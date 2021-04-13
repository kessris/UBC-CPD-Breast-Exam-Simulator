using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class ObserverNetwork : ObserverBehavior
{
    public Behaviour[] disabledBehaviours;

    private void Start()
    {
        if(networkObject.IsOwner)
        {
            FollowCam.Instance.CameraFollowObj = transform.Find("playerFollowObj").gameObject;
        }
        else
        {
            foreach (var b in disabledBehaviours)
            {
                b.enabled = false;
            }

            GetComponent<Collider>().enabled = false;
        }
    }
}
