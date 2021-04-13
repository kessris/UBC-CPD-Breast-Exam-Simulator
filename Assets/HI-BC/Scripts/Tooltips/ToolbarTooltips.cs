using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarTooltips : MonoBehaviour
{
    GameObject Chat;
    GameObject Mic;
    GameObject Control;
    GameObject Pause;
    GameObject Terminate;

    // Start is called before the first frame update
    void Start()
    {
        Chat = GameObject.Find("ChatTooltip");
        Mic = GameObject.Find("MicTooltip");
        Control= GameObject.Find("ControlTooltip");
        Pause = GameObject.Find("PauseTooltip");
        Terminate = GameObject.Find("TerminateTooltip");
    }

    public void ShowChatTooltip()
    {
        Chat.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideChatTooltip()
    {
        Chat.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowMicTooltip()
    {
        Mic.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideMicTooltip()
    {
        Mic.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowControlTooltip()
    {
        Control.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideControlTooltip()
    {
        Control.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowPauseTooltip()
    {
        Pause.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HidePauseTooltip()
    {
        Pause.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowTerminateTooltip()
    {
        Terminate.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideTerminateTooltip()
    {
        Terminate.transform.localScale = new Vector3(0, 0, 0);
    }
}
