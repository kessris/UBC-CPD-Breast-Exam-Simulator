using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCam : MonoBehaviour
{



    private void LateUpdate()
    {
        if (Camera.main == null) return;
        //transform.LookAt(Camera.main.transform, Vector3.forward);
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
