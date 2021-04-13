using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class GameLogic2 : MonoBehaviour
{
    void Start()
    {
        if (CrossSceneInformation.role == "doctor")
            NetworkManager.Instance.InstantiatePlayer(4, new Vector3(-1,-2.5f,-2));
        else if (CrossSceneInformation.role == "patient")
            NetworkManager.Instance.InstantiatePlayer(3, new Vector3(-1,-2,2));
    }
}
