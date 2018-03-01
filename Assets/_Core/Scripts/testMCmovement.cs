using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas, Erik Qvarnström och Timmy Alvelöv. 
public class testMCmovement : MonoBehaviour
{
    [SerializeField]
    float speed = 6.0f, rotationSpeed = 6.0f, jumpSpeed = 8.0f, gravity = 20.0f, airtime;
    float moveOnX;

    int jumps = 2, currentjump = 0;

    bool facingRight, isGrounded;

    Animator animator;

    Vector3 moveDirection = Vector3.zero;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        facingRight = true;
        airtime = 0;
    }

    void Update()
    {
        GroundCheck();
        animator.SetBool("isGrounded", isGrounded);
        moveOnX = Input.GetAxis("Horizontal");

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

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && isGrounded)
        {
            animator.SetTrigger("wave");
        }
    }

    void FlipPlayer() //Vänd spelaren åt motsatt rotation
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Inverse(transform.rotation);
    }

    void Grounded()
    {
        animator.SetFloat("running", (moveOnX), .01f, Time.deltaTime);
        moveDirection = new Vector3(0, 0, Mathf.Abs(moveOnX));
        moveDirection = new Vector3(moveOnX, moveDirection.y, 0);
        moveDirection *= speed;
        currentjump = 0;
        airtime = 0;
    }

    void Airbourne()
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

    void JumpOrFall()
    {
        moveDirection.y = jumpSpeed;
        currentjump++;

        if (currentjump == 2)
        {
            animator.SetTrigger("doubleJump");
        }
        else
        {
            animator.SetTrigger("jump");
        }
    }

    void GroundCheck() //Kollar om spelaren står på marken
    {
        RaycastHit hit;
        isGrounded = Physics.SphereCast(transform.position + new Vector3(0, .25f, 0), 0.2f, -transform.up, out hit, 0.15f);
    }
    void HeadbumbCheck() //Kollar om spelarens huvud krockar med ett objekt
    {
        RaycastHit hit;

        if (moveDirection.y > 0 && Physics.SphereCast(transform.position + new Vector3(0, 1.8f, 0), 0.2f, transform.up, out hit, 0.1f))
        {
            moveDirection.y = 0;
        }

    }

}