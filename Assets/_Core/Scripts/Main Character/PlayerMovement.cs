using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas, Erik Qvarnström och Timmy Alvelöv. 

//Ser till att spelaren kan röra sig korrekt och att animationerna följer rörelserna.
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 6.0f, jumpSpeed = 8.0f, gravity = 20.0f;
    float moveOnX, startSpeed, crouchCenterOffsetY = 0.5f, crouchHeightOffset = 0.9f, crouchCenterOriginal = 1f, crouchHeightOriginal = 1.8f, startingGravity, airtime, offsetZ = -0.85f;


    int jumps = 2, currentjump = 0;

    [SerializeField]
    bool facingRight, isGrounded, inAir;
    bool zeroGravity;

    [SerializeField]
    GameObject jumpParticleParent;

    ParticleSystem[] jumpParticleChildren;

    Animator animator;

    Vector3 moveDirection = Vector3.zero;

    PlayerStats playerStats;

    CharacterController controller;
    void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, offsetZ);
    }
    DashScript dash;
    void Start()
    {
        jumpParticleChildren = jumpParticleParent.GetComponentsInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        facingRight = false;
        inAir = false;
        zeroGravity = false;
        airtime = 0;
        startSpeed = speed;
        startingGravity = gravity;
        dash = GetComponent<DashScript>();
    }

    void Update()
    {
        if (!playerStats.Dead)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, offsetZ);
            GroundCheck();
            animator.SetBool("isGrounded", isGrounded);

            moveOnX = Input.GetAxisRaw("Horizontal");

            if (isGrounded) //Ifall spelaren är på marken
            {
                Grounded();
            }
            else //Ifall spelaren är i luften
            {
                Airbourne();
            }

            if (Input.GetButtonDown("Jump") && currentjump < jumps) //Kollar ifall spelaren kan hoppa
            {
                JumpOrFall();
            }

            if (Input.mousePosition.x < Screen.width / 2 && facingRight == false) //Om muspekaren är på högra sidan av skärmen så vänder spelaren åt höger
            {
                FlipPlayer();
            }

            if (Input.mousePosition.x > Screen.width / 2 && facingRight == true) //Om muspekaren är på vänstra sidan av skärmen så vänder spelaren åt vänster
            {
                FlipPlayer();
            }

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    void FlipPlayer() //Vänd spelaren åt motsatt rotation
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Inverse(transform.rotation);
    }

    void Grounded() //Ifall spelaren befinner sig på marken
    {
        moveDirection = new Vector3(0, 0, Mathf.Abs(moveOnX));
        moveDirection = new Vector3(moveOnX, moveDirection.y, 0);
        moveDirection *= speed;
        if (facingRight)
            moveOnX = -moveOnX;
        animator.SetFloat("running", (moveOnX), .01f, Time.deltaTime);
        currentjump = 0;
        airtime = 0;
        dash.DashReady = true;
    }

    void Airbourne() //Ifall spelaren befinner sig i luften
    {
        airtime += Time.deltaTime;

        if (currentjump == 0 && airtime > 0.25f)
        {
            currentjump++;
        }

        HeadbumbCheck();
        moveDirection = new Vector3(moveOnX, moveDirection.y, 0);
        moveDirection.x *= speed;
    }

    void JumpOrFall() //Ifall spelaren hoppar eller faller
    {
        moveDirection.y = jumpSpeed;
        currentjump++;

        if (currentjump == 2)
        {
            animator.SetTrigger("doubleJump");

            foreach (ParticleSystem particle in jumpParticleChildren)
            {
                particle.Play();
            }

        }
        else
        {
            animator.SetTrigger("jump");
        }
    }

    void GroundCheck() //Kollar om spelaren står på marken
    {
        RaycastHit hit;
        isGrounded = Physics.SphereCast(transform.position + new Vector3(0, .3f, 0), 0.2f, -transform.up, out hit, 0.1f);
        if (hit.transform != null)
        {
            if (hit.transform.tag == "Bullet" || hit.transform.tag == "PatrolPoint" || hit.transform.tag == "Interactable" || hit.transform.tag == "Shield")
            {
                isGrounded = false;
            }
        }

    }
    void HeadbumbCheck() //Kollar om spelarens huvud krockar med ett objekt
    {
        RaycastHit hit;

        if (moveDirection.y > 0 && Physics.SphereCast(transform.position + new Vector3(0, 1.85f, 0), 0.2f, transform.up, out hit, 0.1f))
        {
            if (hit.transform.tag != "Bullet" && hit.transform.tag != "PatrolPoint" && hit.transform.tag != "Interactable" && hit.transform.tag != "Shield")
            {
                moveDirection.y = 0;
            }


        }
    }
    public void ZeroGravity(bool dashing) //Ser till att nollställa gravitiationen och gravitationspåverkan på spelaren eller sätta tillbaka gravitationen
    {
        if (dashing)
        {
            moveDirection.y = 0;
            gravity = 0;
        }
        else
            gravity = startingGravity;
    }
}