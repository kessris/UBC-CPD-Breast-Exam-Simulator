using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
//https://gist.github.com/theWill/7053a02c31b0858dbff3b71b99effc1f
public class MulitplayerEventsController /*: MultiplayerEventsBehavior*/
{
    /*
    protected override void NetworkStart()
    {
        if (!NetworkManager.Instance.IsServer) return;

        base.NetworkStart();

        //Server & Client Events
        NetworkManager.Instance.Networker.bindSuccessful += messageOnBindSucess;
        NetworkManager.Instance.Networker.bindFailure += messageOnBindFail;
        NetworkManager.Instance.Networker.disconnected += messageOnDisconnect;

        //Server Events
        NetworkManager.Instance.Networker.playerConnected += messageOnPlayerConnected;
        NetworkManager.Instance.Networker.playerGuidAssigned += messageOnPlayerGuidAssigned;
        NetworkManager.Instance.Networker.playerAccepted += messageOnPlayerAccepted;
        NetworkManager.Instance.Networker.playerRejected += messageOnPlayerRejected;
        NetworkManager.Instance.Networker.playerTimeout += messageOnPlayerTimeout;
        NetworkManager.Instance.Networker.playerDisconnected += messageOnPlayerDisconnected;

        

        //Client Events
        NetworkManager.Instance.Networker.serverAccepted += messageOnServerAccepted;
        //((UDPClient)NetworkManager.Instance.Networker).connectAttemptFailed += ConnectionFailed;

        //Scene Events
        NetworkManager.Instance.playerLoadedScene += messageOnPlayerLoadedScene;
        NetworkManager.Instance.networkSceneChanging += messageOnNetworkSceneChanging;
        NetworkManager.Instance.networkSceneLoaded += messageOnNetworkSceneLoaded;
        
        DontDestroyOnLoad(this.gameObject);
    }

    //######### Server & Client Events ##########
    private void messageOnBindSucess(NetWorker sender)
    {
        string message = "Multiplayer Event: Bind Successful";
        Debug.Log(message);
    }

    private void messageOnBindFail(NetWorker sender)
    {
        string message = "Multiplayer Event: Bind Failure";
        Debug.Log(message);
    }

    private void messageOnDisconnect(NetWorker sender)
    {
        string message = "Multiplayer Event: disconnected";
        Debug.Log(message);
    }

    //######### Server Events ##########
    private void messageOnPlayerConnected(NetworkingPlayer player, NetWorker sender)
    {
        string message = "Server Multiplayer Event: playerConnected: " + player.NetworkId;
        Debug.Log(message);
    }

    private void messageOnPlayerGuidAssigned(NetworkingPlayer player, NetWorker sender)
    {
        string message = "Server Multiplayer Event: player Guid Assigned: " + player.NetworkId +
            " " + player.InstanceGuid;
        Debug.Log(message);
    }

    private void messageOnPlayerAccepted(NetworkingPlayer player, NetWorker sender)
    {
        string message = "Server Multiplayer Event: player Accepeted: " + player.NetworkId;
        Debug.Log(message);
    }

    private void messageOnPlayerRejected(NetworkingPlayer player, NetWorker sender)
    {
        string message = "Server Multiplayer Event: player rejected: " + player.NetworkId;
        Debug.Log(message);
    }

    private void messageOnPlayerTimeout(NetworkingPlayer player, NetWorker sender)
    {
        string message = "Server Multiplayer Event: player timeout: " + player.NetworkId;
        Debug.Log(message);
    }

    private void messageOnPlayerDisconnected(NetworkingPlayer player, NetWorker sender)
    {
        string message = "Server Multiplayer Event: player disconnected: " + player.NetworkId;
        Debug.Log(message);


        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach(var p in players)
        {
            PlayerNetwork pInfo = p.GetComponent<PlayerNetwork>();
            if (pInfo != null)
            {
                uint networkID = pInfo.networkObject.NetworkId;
                if(player.NetworkId == networkID)
                {
                    pInfo.networkObject.Destroy();
                    Debug.Log($"Destroying network object: {p.name}");
                    //Destroy();
                }
            }
        }

    }

    //######### Client Events ##########
    private void messageOnServerAccepted(NetWorker sender)
    {
        string message = "Client Multiplayer Event: server accepted";
        Debug.Log(message);
    }

    private void ConnectionFailed(NetWorker sender)
    {
        string message = "Client Multiplayer Event: connection failed";
        Debug.Log(message);
    }

    //######### Scene Events ##########
    private void messageOnPlayerLoadedScene(NetworkingPlayer player, NetWorker sender)
    {
        string message = "Scene Multiplayer Event: Player Loaded Scene: " + player.NetworkId;
        Debug.Log(message);
    }

    private void messageOnNetworkSceneChanging(int arg0, LoadSceneMode arg1)
    {
        string message = "Scene Multiplayer Event: Network Scene Changing";
        Debug.Log(message);
    }

    private void messageOnNetworkSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        string message = "Scene Multiplayer Event: Network Scene Loaded";
        Debug.Log(message);
    }

    private void transmitMessage(string message)
    {
        Debug.Log(message);
    }
*/
}