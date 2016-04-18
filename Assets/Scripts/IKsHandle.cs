using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Characters.Black
{
    [RequireComponent(typeof(Black_Mask))]
    public class IKsHandle : MonoBehaviour
    {

        Animator anim;
        Vector3 lookPos;
        Vector3 IK_lookPos;
        Vector3 targetPos;
        Black_Mask p1;

        [SerializeField]
        float lerpRate = 15;
        [SerializeField]
        float updateLookPosThreshold = 2;
        [SerializeField]
        float lookWeight = 1;
        [SerializeField]
        float bodyWeight = .9f;
        [SerializeField]
        float headWeight = 1; //specific to the character in sample assets, will need to be modified to work with others
        [SerializeField]
        float clampWeight = 1;
        [SerializeField]
        float rightHandWeight = 1;
        [SerializeField]
        float leftHandWeight = 1;

        public Transform rightHandTarget;
        public Transform rightElbowTarget;
        public Transform leftHandTarget;
        public Transform leftElbowTarget;

        // Use this for initialization
        void Start()
        {
            this.anim = GetComponent<Animator>();
            p1 = GetComponent<Black_Mask>();
        }

        // Update is called once per frame
        void OnAnimatorIK()
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);

            //anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
            //anim.SetIKPosition(AvatarIKGoal.LeftHand, rightHandTarget.position);

            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);

            anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, rightHandTarget.rotation);

            //this.lookPos = p1.lookPos;
            lookPos.z = transform.position.z;

            float distanceFromPlayer = Vector3.Distance(lookPos, transform.position);

            if(distanceFromPlayer > updateLookPosThreshold)
            {
                targetPos = lookPos;
            }

            IK_lookPos = Vector3.Lerp(IK_lookPos, targetPos, Time.deltaTime * lerpRate);

            anim.SetLookAtWeight(lookWeight, bodyWeight, headWeight, headWeight, clampWeight);
            anim.SetLookAtPosition(IK_lookPos);
        }
    }

}
