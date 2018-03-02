﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Av Andreas de Freitas, Erik Qvarnström och Timmy Alvelöv. 
public class IKHandler : MonoBehaviour
{
    [SerializeField]
    Transform rightHand = null, leftHand = null, shoulder = null, elbow_L = null, elbow_R;

    Animator animator;

    Vector3 lookObj = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update() //Uppdaterar muspekarens position som avataren ska titta på
    {
        SettingAimPosition();
    }

    void SettingAimPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lookP = hit.point;
            lookP.z = transform.position.z;
            lookObj = lookP;
        }
    }

    void OnAnimatorIK() //Använder inverted kinematics för att få armarna att följa med vapnet
    {
        if (lookObj != null)
        {
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(lookObj);
            shoulder.LookAt(lookObj);
        }

        if (rightHand != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
        }

        if (leftHand != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
        }

        if(elbow_L != null && elbow_R != null)
        {
            animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
            animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);

            animator.SetIKHintPosition(AvatarIKHint.LeftElbow, elbow_L.position);
            animator.SetIKHintPosition(AvatarIKHint.RightElbow, elbow_R.position);

            animator.SetIKHintPosition(AvatarIKHint.LeftElbow, elbow_L.position);
            animator.SetIKHintPosition(AvatarIKHint.RightElbow, elbow_R.position);
        }
    }
}
