using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoctorRaycast : MonoBehaviour
{



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("ray casting!");

            //create a ray cast and set it to the mouses cursor position in game
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("UI");
            if (Physics.Raycast(ray, out hit, 1000f, mask))
            {
                //Debug.DrawLine(ray.origin, hit.point); //draw invisible ray cast/vector
                print($"Object hit: {hit.collider.name}");

            }

        }
    }
}