using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandler : MonoBehaviour
{

    Animator animator;

    [SerializeField] Transform rightHand = null;
    [SerializeField] Transform leftHand = null;
    [SerializeField] Transform lookObj = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //lookObj = 
    }

    private void OnAnimatorIK()
    {
        if (lookObj != null)
        {
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(lookObj.position);
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
    }
}
