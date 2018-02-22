using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Movement : MonoBehaviour
{
    private float speed = 6.0F;
    private float jumpSpeed = 12.0F;
    private float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private float turn = 1f;

    private Vector3 targetAngles;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

    }
    void Update()
    {

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            moveDirection.x = 0.0f;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;


            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.A))
        {
            //transform.rotation = Quaternion.Slerp(new Vector3(0f,0f,-90f), transform.position,0);

           transform.rotation = new Quaternion(0, -60, 0, 1);

            /* targetAngles = transform.eulerAngles + 180f * Vector3.up;

             transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, turn); 

    }*/



        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

}
