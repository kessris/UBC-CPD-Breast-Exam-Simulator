using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=LbDQHv9z-F0
//INSTRUCTIONS:
//Camerabase object contains just FollowCam script. 
//Maincamera = child of camerabase, contains camera + followcamcollision script. make sure to set main camera z to like -1 at the very least.

public class FollowCamCollision : MonoBehaviour
{
    public float minDistance = 1.0f, maxDistance = 4.0f, smooth = 10.0f;
    public float zoomSpeed = 0.1f;
    public float maxZoomOffset = 15f;
    Vector3 dollyDir;
    public float distance;
    public float maxHitPercentDistance = 0.89f;


    float _maxDistance, _minDistance;

    private void Start()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;

        _maxDistance = maxDistance;
        _minDistance = minDistance;

    }


    private void Update()
    {

        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
            maxDistance = Mathf.Clamp(maxDistance - zoomSpeed, minDistance, maxDistance);
        else if (d < 0f)// scroll down
            maxDistance = Mathf.Clamp(maxDistance + zoomSpeed, _maxDistance, _maxDistance + maxZoomOffset);


        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;


        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
            if (hit.collider.tag != "Player")
            {

                distance = Mathf.Clamp(hit.distance * maxHitPercentDistance, minDistance, maxDistance); //hit distance multipled by 90%? for a good reason to prevent clipping through floor/terrain
            }
        }
        else
        {
            distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}
