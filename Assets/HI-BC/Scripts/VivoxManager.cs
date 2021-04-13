using System;
using System.Collections;
using UnityEngine;
using VivoxUnity;
using UnityEngine.UI;

public class VivoxManager : MonoBehaviour
{
    public string userName = "UserNaruto1";
    string channelName = "Test";

    VivoxVoiceManager _vivox;
    Client _client = new Client();

    Button muteBtn;
    public GameObject speakingIndicator;
    public bool isObserver = false;

    bool isMuted = false;

    private void Awake()
    {
        _vivox = VivoxVoiceManager.Instance;
        _client.Uninitialize();
        _client.Initialize();

        _vivox.OnUserLoggedInEvent += LoggedIn;
        //DontDestroyOnLoad(this);

        if (CrossSceneInformation.mode == "observer")
            isObserver = true;

        if(!isObserver) GameObject.Find("MicBtn").GetComponent<Button>().onClick.AddListener(ToggleMute);
        speakingIndicator = GameObject.Find("SpeakingIndicator");
        speakingIndicator.SetActive(false);


        if (isObserver)
        {
            _vivox.AudioInputDevices.Muted = true;
            _vivox.AudioOutputDevices.Muted = false;
        }

        StartCoroutine(SetupUser());

        // join different channel based on the room
        if (CrossSceneInformation.room == "1")
            channelName = "Room1";
        else if (CrossSceneInformation.room == "2")
            channelName = "Room2";
    }

    private void OnApplicationQuit()
    {

        print("<color=orange>VivoxVoice Quitting Application - Cleaning up</color>");

        if (_vivox.LoginState == LoginState.LoggedIn)
        {
            _vivox.Logout();
        }

        _client.Uninitialize();
    }

    public void Login()
    {
        if (_vivox.LoginState == LoginState.LoggedIn) return;

        if (!string.IsNullOrEmpty(userName))
        {
            //print($"Login(): {userName}");
            _vivox.Login(userName);
        }
    }

    private void LoggedIn()
    {
        userName = _vivox.LoginSession.LoginSessionId.DisplayName;
        //print($"Logged in with user: {userName}");
    }

    public void Logout()
    {
        if (_vivox.LoginState == LoginState.LoggedIn)
        {
            print("Logging out of vivox");
            _vivox.Logout();
        }
    }

    public void JoinChannel()
    {
        if (!string.IsNullOrEmpty(channelName))
        {
            print($"Joining channel: {channelName}");
            _vivox.JoinChannel(channelName, ChannelType.NonPositional, VivoxVoiceManager.ChatCapability.AudioOnly);
            //assign channelName.text = inputChannelName.text;
        }
    }

    public void LoginUserToVivox()
    {
        StartCoroutine(SetupUser());
    }

    private IEnumerator SetupUser(/*IParticipant participant*/)
    {
        Login();
        yield return new WaitUntil(() => _vivox.LoginState == LoginState.LoggedIn);
        JoinChannel();
        //SetupParticipantPositionalVoice(participant);
    }



    public void ToggleMute()
    {
        if (!isMuted)
        {
            _vivox.AudioInputDevices.Muted = true;
            _vivox.AudioOutputDevices.Muted = false;
            isMuted = true;
            //GameObject.Find("MuteText").GetComponent<Text>().text = "Microphone muted.";
        }
        else
        {
            _vivox.AudioInputDevices.Muted = false;
            _vivox.AudioOutputDevices.Muted = false;
            isMuted = false;
            //GameObject.Find("MuteText").GetComponent<Text>().text = "Microphone not muted.";
        }
        

    }


}
