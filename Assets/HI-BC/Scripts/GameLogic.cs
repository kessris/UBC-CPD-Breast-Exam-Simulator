using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

//used to instantiate player prefabs over the network
public class GameLogic : MonoBehaviour
{
    [SerializeField]
    Vector3 startPosition; //create another one for patients later.

    [SerializeField]
    int playerPrefabChoice = 0;

    void Start()
    {
        //string gender = GameObject.Find("SelectionScreen").GetComponent<SelectionScreen>().role;

        // Set gender character accordingly by changing the playerPrefabChoice -> number
        print(CrossSceneInformation.gender);
        print(CrossSceneInformation.mode);

        if (CrossSceneInformation.mode == "observer")
        {
            startPosition = new Vector3(-1, -2.5f, -2);
            NetworkManager.Instance.InstantiatePlayer(8, startPosition);
        } else
        {
            if (CrossSceneInformation.role == "doctor" && CrossSceneInformation.gender == "female")
                NetworkManager.Instance.InstantiatePlayer(5, startPosition);
            else if (CrossSceneInformation.role == "doctor" && CrossSceneInformation.gender == "male")
                NetworkManager.Instance.InstantiatePlayer(6, startPosition);
            else if (CrossSceneInformation.role == "patient" && CrossSceneInformation.gender == "female")
                NetworkManager.Instance.InstantiatePlayer(4, startPosition);
            else if (CrossSceneInformation.role == "patient" && CrossSceneInformation.gender == "male")
                NetworkManager.Instance.InstantiatePlayer(7, startPosition);
            else
                NetworkManager.Instance.InstantiatePlayer(playerPrefabChoice, startPosition);
        }

        

        //NetworkManager.Instance.InstantiateChatManager();
    }


    public void ConfirmQuit()
    {
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerNetwork>().ExitWindow();
    }

    public void ConfirmGoHome()
    {
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerNetwork>().GoToHome();
    }

    public void HideQuit()
    {
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerNetwork>().ExitWindowWarning();
    }

    public void HideGoHome()
    {
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerNetwork>().GoToHomeWarning();
    }
    /*
    bool CheckIfMoreThanOnePlayer() //super hackjob :D
    {
        var players = GameObject.FindGameObjectsWithTag("LocalPlayer");
        print(players.Length);
        if (players.Length > 1)
        {
            foreach(var player in players)
            {
                if (!player.GetComponent<PlayerNetwork>().isActualLocalPlayer)
                {
                    //player.SetActive(false);
                    Destroy(player);
                }
            }

            enabled = false; //stop checking after disabling the duplicate local players... ...
            return true;
        }else if (players.Length == 1)
        {
            enabled = false;
        }


        return false;
    }*/

}
