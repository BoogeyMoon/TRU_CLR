using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMCmovement : MonoBehaviour
{
    [SerializeField]
    float speed = 6.0f, rotationSpeed = 6.0f, jumpSpeed = 8.0f, gravity = 20.0f;
    Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    int jumps = 2;
    int currentjump = 0;
    float moveOnX;
    bool facingRight;
    Animator animator;
    bool isGrounded;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        facingRight = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GroundCheck();
        animator.SetBool("isGrounded", isGrounded);
        moveOnX = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            //moveOnX = Input.GetAxis("Horizontal");
            animator.SetFloat("running", Mathf.Abs(moveOnX), .01f, Time.deltaTime);
            moveDirection = new Vector3(0, 0, Mathf.Abs(moveOnX));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            currentjump = 0;
        }
        else
        {
            //if(currentjump == 0)
            //{
            //    currentjump++;
            //}
            //moveOnX = Input.GetAxis("Horizontal");
            moveDirection = new Vector3(moveOnX, moveDirection.y, 0);
            moveDirection.x *= speed;
        }

        if (Input.GetButtonDown("Jump") && currentjump < jumps)
        {
            moveDirection.y = jumpSpeed;
            //animator.SetTrigger("jump");
            currentjump++;
            if(currentjump == 2)
            {
                animator.SetTrigger("doubleJump");
            }
            else
            {
                animator.SetTrigger("jump");
            }
            
        }
        if (moveOnX < 0.0f && facingRight == false)
        {
            FlipPlayer();
        }

        if (moveOnX > 0.0f && facingRight == true)
        {
            FlipPlayer();
        }

        //this.transform.rotation = Quaternion.LookRotation(moveDirection);

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        //memes
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && isGrounded)
        {
            animator.SetTrigger("wave");
        }

    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        //Vector3 localScale = gameObject.transform.localScale;
        //localScale.x *= -1;
        //transform.localScale = localScale;

        this.transform.rotation = Quaternion.Inverse(transform.rotation);
    }

    void GroundCheck()
    {
        Ray ray = new Ray(this.transform.position + new Vector3(0,.25f,0), -this.transform.up);
        isGrounded = Physics.Raycast(ray, .25f);
    }

}