using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Följande script gör så att karaktären kan snabbt "dasha" framåt i den riktning spelaren siktar.
//Skapat av Slavko Stojnic och Moa Lindgren.
public class DashScript : MonoBehaviour
{
    [SerializeField]
    AudioClip dashSound;
    SoundManager soundManager;
    private ParticleSystem[] dashParticles;

    [SerializeField]
    GameObject dashEmitter;

    [SerializeField]
    GameObject rifleBarrel, startObject;
    [SerializeField]
    float lengthOfDash, dashCooldown, moveSpeed, approxValue;
    float endDashX, endDashY, charX, charY;
    Vector3 direction, endDash, startPosition;
    [SerializeField]
    bool dashing;
    bool dashOnCooldown;
    PlayerMovement playerMovement;
    float dist;

    void Start()
    {
        dashParticles = dashEmitter.GetComponentsInChildren<ParticleSystem>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
        foreach (ParticleSystem dashParticle in dashParticles)
        {
            var emission = dashParticle.emission;
            emission.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !dashOnCooldown)
        {
            dashOnCooldown = true;
            soundManager.PlaySingle(dashSound,1); // play Dash sound
            startPosition = startObject.transform.position;
            direction = rifleBarrel.transform.forward;
            Dash();
            dashing = true;
            playerMovement.ZeroGravity(dashing);
            foreach (ParticleSystem dashParticle in dashParticles)
            {
                var emission = dashParticle.emission;
                emission.enabled = true;
            }
        }

        if(dashing)
        {
            dist = Vector3.Distance(transform.position, endDash);
            //endDashX = endDash.x;
            //endDashY = endDash.y;
            //charX = transform.position.x;
            //charY = transform.position.y;

            //Följande gör att spelaren dashar:
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, endDash, step);

            //Kollar om spelaren är nära raycastens slutposition (endDash). Vilket leder till att spelaren slutar dasha:
            if (dist <= approxValue)
            {
                dashing = false;
                playerMovement.ZeroGravity(dashing);
                foreach (ParticleSystem dashParticle in dashParticles)
                {
                    var emission = dashParticle.emission;
                    emission.enabled = false;
                }
            }
        }
    }

    void Dash()
    {
        Ray ray = new Ray(startPosition, direction);
        RaycastHit raycastHit;
        Debug.DrawRay(ray.origin, ray.direction * lengthOfDash);

        //Om spelaren försöker dasha in mot ett objekt:
        if (Physics.Raycast(ray, out raycastHit, lengthOfDash))
        {
            endDash = new Vector3(raycastHit.point.x, raycastHit.point.y, transform.position.z) ;
        }
        else
        {
            endDash = ray.origin + ray.direction * lengthOfDash;
        }
        StartCoroutine(DashCooldown());
    }
    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
    }
}