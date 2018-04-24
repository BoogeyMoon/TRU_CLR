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

    [SerializeField]
    GameObject dashEmitter;
    ParticleSystem [] dashParticles;

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
    Animator animator;
    public bool DashReady { get; set; }
    MenuScript menu;


    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        soundManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>();
        dashParticles = dashEmitter.GetComponentsInChildren<ParticleSystem>();
        menu = GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<MenuScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashOnCooldown && DashReady && !menu.Paused)
        {
            dashOnCooldown = true;
            DashReady = false;
            soundManager.PlaySingle(dashSound,1); // play Dash sound
            startPosition = startObject.transform.position;
            direction = rifleBarrel.transform.forward;
            Dash();
            dashing = true;
            playerMovement.ZeroGravity(dashing);
            animator.SetBool("isDashing", dashing);
            foreach (ParticleSystem particle in dashParticles) // activate particles for the Dash
                if (!playerMovement.FacingRight)
                    dashParticles[0].Play(true);
                else
                    dashParticles[4].Play(true);
        }

        if(dashing)
        {
            dist = Vector3.Distance(transform.position, endDash);
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
        Ray ray = new Ray(startPosition, direction);
        RaycastHit raycastHit;
        Debug.DrawRay(ray.origin, ray.direction * lengthOfDash);

        //Om spelaren försöker dasha in mot ett objekt:
        if (Physics.Raycast(ray, out raycastHit, lengthOfDash))
        {
            endDash = new Vector3(raycastHit.point.x, raycastHit.point.y, transform.position.z) ; // make sure the z stays the same, just in case
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