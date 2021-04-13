using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;

public class Door : DoorBehavior
{

    public float degrees = 90f;
    Quaternion originalRot;
    public Transform door;
    public GameObject text;

    private bool isWithinRange = false;
    private bool isDoorOpen = false;


    private void Start()
    {
        originalRot = transform.rotation; //test
    }

    private void Update()
    {
        if (isWithinRange)
        {
            text.SetActive(true);
            if (!isDoorOpen && Input.GetKeyDown("space"))
            {
                door.rotation = originalRot * Quaternion.AngleAxis(degrees, Vector3.up);
                networkObject.SendRpc(RPC_MANIPULATE_DOOR, Receivers.All, true); // open door
                isDoorOpen = true;
            } else if (isDoorOpen && Input.GetKeyDown("space"))
            {
                door.rotation = originalRot;
                networkObject.SendRpc(RPC_MANIPULATE_DOOR, Receivers.All, false); // close door
                isDoorOpen = false;
            }
        } else
        {
            text.SetActive(false);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        isWithinRange = false;
    }

    private void OnTriggerStay(Collider other)
    {
        isWithinRange = true;
    }
    

    public override void manipulateDoor(RpcArgs args)
    {
        MainThreadManager.Run(() =>
        {
            bool shouldOpen = args.GetNext<bool>();
            if (shouldOpen)
            {
                door.rotation = originalRot * Quaternion.AngleAxis(degrees, Vector3.up);
            } else
            {
                door.rotation = originalRot;
            }

        });
    }
}
