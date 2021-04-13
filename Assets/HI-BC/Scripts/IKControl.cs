using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = false;
    
    public float rightRotationWeight = 0f;
    public float rightPositionalWeight = 1.0f;
    public float lookWeight = 0.5f;

    public Transform rightHandObj = null;
    public Transform leftHandObj = null;
    public Transform lookObj = null;

    public float handSpeed = 0.5f;

    public float maxX = 1.25f;
    public float minX = -1f;
    public float maxY = 3f;
    public float minY = 0;

    public float maxLeftX, minLeftX, maxLeftY, minLeftY;

    public Vector3 rightHandDirection;
    public Vector3 leftHandDirection;

    bool isLooking;
    float h, v;

    Vector3 rHandOriginalPos;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null) animator = transform.Find("model").GetComponent<Animator>();

        rightHandDirection.z = 0.4f;
        leftHandDirection.z = 0.4f;

        rHandOriginalPos = rightHandObj.position;
    }

    private void Update()
    {
        if (transform.root.tag != "LocalPlayer") return;
        if (!isLooking) return;


        h = Input.GetAxisRaw("Mouse X") * handSpeed;
        v = Input.GetAxisRaw("Mouse Y") * handSpeed;

        //print($"vertical {v}, hor: {h}");
        rightHandDirection.x += h; //left to right
        rightHandDirection.y += v; //height

        leftHandDirection.x += h;
        leftHandDirection.y += v;


        rightHandDirection.x = Mathf.Clamp(rightHandDirection.x, minX, maxX);
        rightHandDirection.y = Mathf.Clamp(rightHandDirection.y, minY, maxY);

        leftHandDirection.x = Mathf.Clamp(leftHandDirection.x, minLeftX, maxLeftX);
        leftHandDirection.y = Mathf.Clamp(leftHandDirection.y, minLeftY, maxLeftY);


        //Vector3 finalPos = transform.position + rightHandObj.TransformDirection(rightHandDirection);

        if (Input.GetMouseButton(1)) rightHandObj.position = transform.position + rightHandObj.TransformDirection(rightHandDirection);
        if (Input.GetMouseButton(0)) leftHandObj.position = transform.position + leftHandObj.TransformDirection(leftHandDirection);


        //create a ray cast and set it to the mouses cursor position in game
        /*
        if (Input.GetMouseButtonDown(0))
        {
            print("called");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Player");
            if (Physics.Raycast(ray, out hit, 1000f, mask))
            {
                //Debug.DrawLine(ray.origin, hit.point); //draw invisible ray cast/vector
                print($"Object hit: {hit.collider.name}");
                rightHandObj.position = hit.collider.transform.position;
                leftHandObj.position = hit.collider.transform.position;

                var patient = hit.collider.GetComponent<PatientUtility>();
                if (patient != null)
                {
                    patient.Examine();
                }

            }
        }*/
        

        if (Input.GetKeyDown(KeyCode.H))
        {
            
        }

    }


    public void SetHandPosition(Vector3 position)
    {
        rightHandObj.position = position;
        //leftHandObj.position = position;
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (!isLooking) return;

        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(lookWeight);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {

                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightPositionalWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightRotationWeight);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);

                    //animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    //animator.SetIKPosition(AvatarIKGoal.LeftHand, rightHandObj.position);
                }

                if (leftHandObj != null)
                {

                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, rightPositionalWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, rightRotationWeight);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);

                    //animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    //animator.SetIKPosition(AvatarIKGoal.LeftHand, rightHandObj.position);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }


    public void ToggleExamining(bool state)
    {
        isLooking = state;
        //if (state == false) rightHandObj.position = leftHandObj.position;
    }
}
