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


    void Start()
    {
        controller = GetComponent<CharacterController>();
        facingRight = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        animator.SetBool("isGrounded", controller.isGrounded);

        if (controller.isGrounded)
        {
            moveOnX = Input.GetAxis("Horizontal");
            animator.SetFloat("running", Mathf.Abs(moveOnX), .01f, Time.deltaTime);
            moveDirection = new Vector3(0, 0, Mathf.Abs(moveOnX));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            currentjump = 0;
        }

        if (Input.GetButtonDown("Jump") && currentjump < jumps)
        {
            moveDirection.y = jumpSpeed;
            animator.SetTrigger("jump");
            currentjump++;
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
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        //Vector3 localScale = gameObject.transform.localScale;
        //localScale.x *= -1;
        //transform.localScale = localScale;

        this.transform.rotation = Quaternion.Inverse(this.transform.rotation);
    }
}