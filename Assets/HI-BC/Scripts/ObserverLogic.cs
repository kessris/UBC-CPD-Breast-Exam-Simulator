using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class ObserverLogic : MonoBehaviour
{
    public Transform startPos;

    void Start()
    {
        NetworkManager.Instance.InstantiateObserver(0, startPos.position);
    }

}
