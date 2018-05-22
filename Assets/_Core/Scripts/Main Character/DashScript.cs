using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Följande script gör så att karaktären kan snabbt "dasha" framåt i den riktning spelaren siktar.
//Skapat av Slavko Stojnic och Moa Lindgren.
public class DashScript : MonoBehaviour
{
    [SerializeField]
    AudioClip dashSound;
    AudioManager soundManager;

    [SerializeField]
    GameObject dashEmitter;
    ParticleSystem[] dashParticles;

    [SerializeField]
    Transform[] Dashlimits;

    [SerializeField]
    GameObject rifleBarrel;

    [SerializeField]
    GameObject[] startObject;

    [SerializeField]
    float lengthOfDash, dashCooldown, moveSpeed, approxValue, dontDashLength, sizeOfSpherecast;
    float endDashX, endDashY, charX, charY;
    Vector3 direction, endDash, startPosition;

    [SerializeField]
    bool dashing;
    bool dashOnCooldown;
    PlayerMovement playerMovement;
    float dist, tempDist;
    Animator animator;
    public bool DashReady { get; set; }
    MenuScript menu;
    PlayerStats player;


    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        soundManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        dashParticles = dashEmitter.GetComponentsInChildren<ParticleSystem>();
        menu = GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<MenuScript>();
        player = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashOnCooldown && DashReady && !menu.Paused && !player.Dead)
        {
            dashOnCooldown = true;
            DashReady = false;
            soundManager.Play(dashSound.name); // play Dash sound
            startPosition = startObject[0].transform.position;
            direction = rifleBarrel.transform.forward;
            Dash();
            dashing = true;
            dist = lengthOfDash;
            playerMovement.ZeroGravity(dashing);
            animator.SetBool("isDashing", dashing);
            foreach (ParticleSystem particle in dashParticles) // activate particles for the Dash
                if (!playerMovement.FacingRight)
                    dashParticles[0].Play(true);
                else
                    dashParticles[4].Play(true);
        }

        if (dashing)
        {

            foreach (Transform limit in Dashlimits)
            {
                tempDist = Vector3.Distance(limit.position, endDash);
                if (tempDist < dist)
                {
                    dist = tempDist;
                    //print("Distance has changed to " + dist + " from the limit number " + limit.gameObject.name);
                }
            }

            //Make the MC dash
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, endDash, step);
            //Check if MC reached the destination. Stop the dash.
            if (dist <= approxValue)
            {
                dashing = false;
                animator.SetBool("isDashing", dashing);
                playerMovement.ZeroGravity(dashing);
            }
        }
    }

    void Dash()
    {
        Ray ray;
        RaycastHit raycastHit;
        float shortestHit = Mathf.Infinity;
        foreach (GameObject startObjects in startObject) // checking several raycasts from different points on the character
        {
            startPosition = startObjects.transform.position;
            ray = new Ray(startPosition, direction);
            Debug.DrawRay(ray.origin, ray.direction * lengthOfDash);
            int layerMask = ~(1 << 8 | 1 << 2); // layers to ignore with raycast - player character and the original ignore raycast layer

            if (Physics.Raycast(ray, out raycastHit, lengthOfDash, layerMask))
            {
                if (raycastHit.distance < shortestHit)
                {
                    shortestHit = raycastHit.distance;
                    print(shortestHit);
                    endDash = new Vector3(raycastHit.point.x, raycastHit.point.y, transform.position.z);

                }
                print("Raycast hit: " + raycastHit.transform.gameObject);
            }
            else if(shortestHit == Mathf.Infinity)
            {
                    endDash = rifleBarrel.transform.position + ray.direction * lengthOfDash;
            }
        }

        StartCoroutine(DashCooldown());
    }
    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
    }
}