using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.UI;

public class PauseTerminateSwitcher : PauseScreenBehavior
{
    public GameObject pauseScreen;
    public GameObject terminatedNotifierScreen;

    bool isPaused;

    private void Start()
    {
        isPaused = false;

    }

    public override void pauseOrResume(RpcArgs args)
    {
        bool shouldPause = args.GetNext<bool>();
        MainThreadManager.Run(() =>
        {
            if (shouldPause)
            {
                pauseScreen.transform.localScale = new Vector3(1,1,1);
            }
            else if (!shouldPause)
            {
                pauseScreen.transform.localScale = new Vector3(0, 0, 0);
            }
        });
    }

    public void triggerPauseButton()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.transform.localScale = new Vector3(0, 0, 0);
            networkObject.SendRpc(RPC_PAUSE_OR_RESUME, Receivers.All, isPaused);
        }
        else if (!isPaused)
        {
            isPaused = true;
            pauseScreen.transform.localScale = new Vector3(1, 1, 1);
            networkObject.SendRpc(RPC_PAUSE_OR_RESUME, Receivers.All, isPaused);
        }
    }

    public void showSummary()
    {
        GameObject.Find("offendedCounter2").GetComponent<Text>().text = GameObject.Find("offendedCounter").GetComponent<Text>().text;
        GameObject.Find("anxiousCounter2").GetComponent<Text>().text = GameObject.Find("anxiousCounter").GetComponent<Text>().text;
        GameObject.Find("confusedCounter2").GetComponent<Text>().text = GameObject.Find("confusedCounter").GetComponent<Text>().text;
        GameObject.Find("embarassedCounter2").GetComponent<Text>().text = GameObject.Find("embarassedCounter").GetComponent<Text>().text;
        GameObject.Find("calmCounter2").GetComponent<Text>().text = GameObject.Find("calmCounter").GetComponent<Text>().text;

        GameObject.Find("SummaryPage").transform.localScale = new Vector3(1, 1, 1);
    }

    public void showTerminatePopUp()
    {
        GameObject.Find("EndSessionPopUp").transform.localScale = new Vector3(0, 0, 0);
        terminatedNotifierScreen.transform.localScale = new Vector3(1, 1, 1);
        networkObject.SendRpc(RPC_SHOW_TERMINATE_NOTIFICATION, Receivers.All);
    }

    public override void showTerminateNotification(RpcArgs args)
    {
        MainThreadManager.Run(() =>
        {
            terminatedNotifierScreen.transform.localScale = new Vector3(1, 1, 1);
        });
    }
}
