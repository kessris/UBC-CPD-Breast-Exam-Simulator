using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverMoveController : MonoBehaviour
{
    public float speed = 1.2f;

    float horizontal, vertical;
    Vector3 move;



    void Update()
    {

        //horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, FollowCam.Instance.rotY, transform.localEulerAngles.z);

        move = transform.right * 0 + transform.forward * vertical;

        transform.position += (move * speed * Time.deltaTime);


    }
}
