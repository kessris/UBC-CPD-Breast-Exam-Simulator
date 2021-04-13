using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOffline : MonoBehaviour
{
    public float degrees = 90f;
    Quaternion originalRot;

    Transform target;

    bool isOpen;

    private void Start()
    {
        originalRot = transform.rotation;
    }


    private void Update()
    {
        if (target == null) return;

        if(Vector3.Distance(transform.position, target.position) <= 1.5f)
        {
            transform.rotation = originalRot * Quaternion.AngleAxis(degrees, Vector3.up);
        }
        else
        {
            transform.rotation = originalRot;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LocalPlayer") target = other.gameObject.transform;
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }


}
