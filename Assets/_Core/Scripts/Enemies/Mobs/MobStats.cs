using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Av Andreas de Freitas Timmy Alvelöv

// Håller koll på hälsa, fart, osv. för mobs
public class MobStats : MonoBehaviour
{
    Score score;
    [SerializeField]
    protected float speed, maxHealth, fireRate, aggroRange, distanceInterval, timeBetweenBurst, shotsPerBurst, spread, health, deathAnimDuration;
    [SerializeField]
    protected int color, numberOfBulletsPerShot, scoreValue;
    [SerializeField]
    protected GameObject[] bulletSpawners, raycastOrigin;
    [SerializeField]
    protected GameObject destination, bullet, patrolPoints;
    public GameObject PatrolPoints
    {
        set
        {
            patrolPoints = value;
            updatePatrolPoints();
        }
    }
    protected GameObject currentBullet;
    protected Transform playerTarget, player;
    protected List<Transform> patrolPointsList = new List<Transform>();
    protected bool onCooldown, dead;
    protected float timeLeft, burstTimer, burstCounter, playerDistance;
    protected int patrolCounter;
    protected Quaternion startRot;
    protected Animator animator;


    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("ShootHere").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected virtual void Start()
    {
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();
        if (GetComponent<Animator>() != null)
            animator = gameObject.GetComponent<Animator>();
        else
        {
            animator = null;
        }
        patrolCounter = 0;
        health = maxHealth;
        timeLeft = fireRate;
        onCooldown = false;
        updatePatrolPoints();
        startRot = transform.rotation;
        if (deathAnimDuration == 0)
        {
            deathAnimDuration = 2;
        }
    }
    protected void updatePatrolPoints() //Kollar barnen på ett gameobject och lägger till dem i en lista.
    {
        if (patrolPoints != null)
        {
            for (int i = 0; i < patrolPoints.transform.childCount; i++)
            {
                patrolPointsList.Add(patrolPoints.transform.GetChild(i));
            }
            if (patrolPointsList.Count > 0)
                destination = patrolPointsList[0].gameObject;
        }

    }

    public virtual void TakeDamage(float damage, int color) //Om mob:en blir träffad av en kula som korresponderar med mob:ens färg så tar den skada.
    {
        if (color == this.color && health > 0)
        {
            health -= damage;
            if (!dead && health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(Flicker());
            }
        }
    }

    public void GainHealth(float life) //Ökar mob:ens hälsa
    {
        health += life;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void Die() //Mob:en dör.
    {
        score.AddScore(scoreValue);
        dead = true;
        if (animator != null)
            animator.SetTrigger("deathTrigger");
        StartCoroutine(GetDestroyed());
    }
    IEnumerator GetDestroyed()
    {
        SkinnedMeshRenderer mesh = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        int numberOfBlinks = 3;
        for (int i = 0; i < numberOfBlinks; i++)
        {
            mesh.enabled = !mesh.enabled;
            yield return new WaitForSeconds((deathAnimDuration / 96) / numberOfBlinks);
            mesh.enabled = !mesh.enabled;
            yield return new WaitForSeconds((deathAnimDuration / 48) / numberOfBlinks);
            yield return new WaitForSeconds((deathAnimDuration / 96) / numberOfBlinks);
        }
        Destroy(gameObject);
    }

    public void ChangeDestination(GameObject newDestination, GameObject lastDestination) //Ger en mob sitt nästa mål, om input är null går den till nästa mål i sin lista.
    {
        if (lastDestination == destination) //Ser till att vi inte krockar in i fel patrullställe
        {
            if (newDestination != null) //Sätter ny destination
                destination = newDestination;
            else //Går till nästa plats i listan
            {
                if (patrolPointsList.Count != 1)
                {
                    patrolCounter++;
                    if (patrolCounter > patrolPointsList.Count - 1)
                    {
                        patrolCounter = 0;
                    }
                    destination = patrolPointsList[patrolCounter].gameObject;

                }
            }
        }

    }
    protected void Patrol() //Går mot nästa patrullplats
    {
        if (destination != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, speed / 2 * Time.deltaTime);
        }

    }

    protected virtual void Shoot() //Ser till att rätt antal skott skjuts samt räknar ut dess offset.
    {
        float startRot;
        onCooldown = true;
        if (numberOfBulletsPerShot % 2 == 0) //Om vi har ett jämnt antal kulor
        {
            startRot = ((numberOfBulletsPerShot / 2) - 1) * spread;
            startRot += spread / 2;
        }
        else //Om vi har ett udda antal kulor
        {
            int n = numberOfBulletsPerShot / 2;
            startRot = n * spread;
        }
        for (int i = 0; i < numberOfBulletsPerShot; i++)
        {
            ShootABullet(startRot - spread * i);
        }
        burstCounter--;
        if (burstCounter <= 0) //Räknar om vi är klara med bursten eller inte.
        {
            burstCounter = shotsPerBurst;
            burstTimer = timeBetweenBurst;
        }

        if (onCooldown)
        {
            timeLeft = fireRate;
        }
    }

    protected virtual void ShootABullet(float RotationOffset) //Sköter instansieringen av en kula och ger den en rotationsoffset.
    {
        for (int i = 0; i < bulletSpawners.Length; i++)
        {
            currentBullet = Instantiate(bullet); //Skapar en kula

            currentBullet.transform.position = bulletSpawners[i].transform.position; //Sätter positionen till mynningen på vapnet
            currentBullet.transform.rotation = bulletSpawners[i].transform.rotation; //Sätter rotationen så att skottet åker dit vapnet siktar
            currentBullet.transform.Rotate(RotationOffset, 0, 0); //Ändrar offseten för skottet om så önskas
        }

    }
    protected virtual float GetPlayerDistance(Transform position) //Ger tillbaka avståndet till spelaren med endast x -och yaxlarna i beaktning
    {
        return ((playerTarget.transform.position - position.position).magnitude);
    }
    protected bool CanSeePlayer() //Ser om det finns en collder mellan spelaren och fienden
    {
        for (int i = 0; i < raycastOrigin.Length; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(raycastOrigin[i].transform.position, new Vector3(playerTarget.position.x - raycastOrigin[i].transform.position.x, playerTarget.position.y - raycastOrigin[i].transform.position.y, playerTarget.position.z - raycastOrigin[i].transform.position.z), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(raycastOrigin[i].transform.position, new Vector3(playerTarget.position.x - transform.position.x, playerTarget.position.y - transform.position.y, playerTarget.position.z - transform.position.z), Color.blue);
                if (hit.transform.gameObject.tag == "Player")
                {
                    return true;
                }
            }


        }
        return false;
    }
    protected void LookAtPlayer(Transform Obj) //Kollar mot spelaren på x-y planet
    {
        Obj.transform.LookAt(new Vector3(playerTarget.position.x, playerTarget.position.y, Obj.position.z));
    }
    protected void SetToPlayerPlane(Transform Obj)
    {
        Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y, player.transform.position.z);
    }
    IEnumerator Flicker()
    {
        Material[] mats = transform.GetChild(0).GetComponent<Renderer>().materials;

        for (int i = 0; i < 2; i++)
        {
            mats[0].SetColor("_EmissionColor", Color.white);
            transform.GetChild(0).GetComponent<Renderer>().materials = mats;
            yield return new WaitForSeconds(0.1f);
            mats[0].SetColor("_EmissionColor", Color.black);
            transform.GetChild(0).GetComponent<Renderer>().materials = mats;
            yield return new WaitForSeconds(0.1f);

        }

    }

}
