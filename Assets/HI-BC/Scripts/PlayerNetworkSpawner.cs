using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkSpawner : MonoBehaviour
{

    #region Singleton
    public static PlayerNetworkSpawner Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
        DontDestroyOnLoad(this);
    }

    #endregion
    [SerializeField]
    public int playerPrefabChoice = 0;


    public void ChangePrefabChoice(int val)
    {
        playerPrefabChoice = val;
    }


}
