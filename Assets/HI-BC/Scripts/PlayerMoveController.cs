using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{

    public float speed = 5f;

    public bool isExamining;

    float horizontal, vertical;
    Vector3 move;

    Animator playerAnimationController;


    void Start()
    {
        playerAnimationController = GetComponent<Animator>();
        if (playerAnimationController == null) playerAnimationController = transform.Find("model").GetComponent<Animator>();
        
    }

    
    void Update()
    {

        //horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (vertical != 0)
        {
            playerAnimationController.SetInteger("speed", 1);
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, FollowCam.Instance.rotY, transform.localEulerAngles.z);
        }
        else
        {
            playerAnimationController.SetInteger("speed", 0);
        }

        float roty = FollowCam.Instance.rotY/360f;
        if(!isExamining) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, FollowCam.Instance.rotY, transform.localEulerAngles.z);


        move = transform.right * 0 + transform.forward * vertical;

        transform.position += (move * speed * Time.deltaTime);


    }
}
