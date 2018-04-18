using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Av Andreas de Freitas Timmy Alvelöv

// Håller koll på hälsa, fart, osv. för mobs
public class MobStats : MonoBehaviour
{
    Score score;
    [SerializeField]
    AudioClip [] damageCyan, damageYellow, damageMagenta;
    SoundManager soundManager;

    //score popup memes :)
    GameObject scoreCanvas;
    protected Vector3 deadMob;


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
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
    }
    protected virtual void Start()
    {
        scoreCanvas = Resources.Load("ScorePopupCanvas") as GameObject;
        

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
            deathAnimDuration = 0.5f;
        }
        if (bulletSpawners.Length != 0 && raycastOrigin.Length == 0)
        {
            raycastOrigin = new GameObject[1];
            raycastOrigin[0] = bulletSpawners[0];
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
            switch (this.color)
            {
                case 0:
                    if (damageCyan != null) { soundManager.RandomizeSfx(damageCyan, 3, true); }
                    break;
                case 1:
                    if (damageYellow != null) { soundManager.RandomizeSfx(damageYellow, 3, true); }
                    break;
                case 2:
                    if (damageMagenta != null) { soundManager.RandomizeSfx(damageMagenta, 3, false); }
                    break;
            }
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

    public virtual void TakeDamage(float damage, int color, Transform obj) //Om mob:en blir träffad av en kula som korresponderar med mob:ens färg så tar den skada.
    {
        if (color == this.color && health > 0)
        {
            health -= damage;
 
            if (!dead && health <= 0)
            {
                Die(obj);
            }
            else
            {
                StartCoroutine(Flicker(obj));
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

    protected void Die() //Mob:en dör.
    {
        score.AddScore(scoreValue);

        if (scoreValue != 0)
        {
            FloatingScore();
        }
        dead = true;
        if (animator != null)
            animator.SetTrigger("deathTrigger");
        StartCoroutine(GetDestroyed());
    }

    protected void FloatingScore() //Skapar en popup textruta som visar hur mycket score som laggts till ens total. 
    {
        deadMob = this.transform.position; 
        GameObject scoreCanvasInstance = Instantiate(scoreCanvas);
        scoreCanvasInstance.transform.position = deadMob;
        scoreCanvasInstance.GetComponentInChildren<Text>().text = scoreValue.ToString();
        Animator anim = scoreCanvasInstance.GetComponentInChildren<Animator>();
        AnimatorClipInfo[] clipinfo = anim.GetCurrentAnimatorClipInfo(0);
        Destroy(scoreCanvasInstance, clipinfo[0].clip.length + 10);
        
    }


    protected void Die(Transform obj) //Mob:en dör men renderern sitter på en annan plats än scriptet
    {
        score.AddScore(scoreValue);
        dead = true;
        if (animator != null)
            animator.SetTrigger("deathTrigger");
        StartCoroutine(GetDestroyed());
    }

    IEnumerator GetDestroyed() //Blinkande effekt
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            int numberOfBlinks = 3;
            for (int i = 0; i < numberOfBlinks; i++)
            {
                rend.enabled = !rend.enabled;
                yield return new WaitForSeconds((deathAnimDuration / numberOfBlinks) / 4);
                rend.enabled = !rend.enabled;
                yield return new WaitForSeconds(((deathAnimDuration * 3) / numberOfBlinks) / 4);
            }
        }
        Destroy(gameObject);
    }

    IEnumerator GetDestroyed(Transform obj) //Blinkande effekt men renderern sitter på en annan plats än scriptet
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            int numberOfBlinks = 3;
            for (int i = 0; i < numberOfBlinks; i++)
            {
                rend.enabled = !rend.enabled;
                yield return new WaitForSeconds((deathAnimDuration / numberOfBlinks) / 4);
                rend.enabled = !rend.enabled;
                yield return new WaitForSeconds(((deathAnimDuration * 3) / numberOfBlinks) / 4);
            }
        }
        Destroy(obj.gameObject);
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
    protected void SetToPlayerPlane(Transform Obj) //Sätter objeket den tar till till samma z-värde som spelaren
    {
        Obj.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y, player.transform.position.z);
    }
    IEnumerator Flicker() //Gör att renderern blinkar vitt
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            Material[] mats = rend.materials;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j].SetColor("_EmissionColor", Color.white);
                }
                rend.materials = mats;
                yield return new WaitForSeconds(0.1f);
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j].SetColor("_EmissionColor", Color.black);
                }
                rend.materials = mats;
                yield return new WaitForSeconds(0.1f);

            }
        }
    }

    IEnumerator Flicker(Transform obj) //Gör att renderern på det intagna objektet blinkar vitt
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            Material[] mats = rend.materials;
            
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j].SetColor("_EmissionColor", Color.white);
                }
                rend.materials = mats;
                yield return new WaitForSeconds(0.1f);
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j].SetColor("_EmissionColor", Color.black);
                }
                rend.materials = mats;
                yield return new WaitForSeconds(0.1f);

            }
        }
    }

}
