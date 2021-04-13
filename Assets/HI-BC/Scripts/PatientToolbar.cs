using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientToolbar : MonoBehaviour
{
    [SerializeField] Sprite CHAT_ON, CHAT_OFF, MIC_ON, MIC_OFF, CONTROL_GUIDE_ON, CONTROL_GUIDE_OFF, PAUSE_ON, PAUSE_OFF, TERMINATE_ON, TERMINATE_OFF;
    private Image chatBtn, micBtn, controlGuideBtn, pauseBtn, terminateBtn;
    [SerializeField] GameObject endSessionPopUp, closeGuidePopUp, chatBox;

    bool isChatOn, isMicOn, isControlGuideOn=true, isPaused, isTerminating;
     
    void Start()
    {
        chatBtn = GameObject.Find("ChatBtn").GetComponent<Image>();
        micBtn = GameObject.Find("MicBtn").GetComponent<Image>();
        controlGuideBtn = GameObject.Find("ControlBtn").GetComponent<Image>();
        pauseBtn = GameObject.Find("PauseBtn").GetComponent<Image>();
        terminateBtn = GameObject.Find("TerminateBtn").GetComponent<Image>();

        GameObject.Find("ChatBtn").GetComponent<Button>().onClick.AddListener(ToggleChat);
        GameObject.Find("MicBtn").GetComponent<Button>().onClick.AddListener(ToggleMic);
        GameObject.Find("ControlBtn").GetComponent<Button>().onClick.AddListener(ToggleControlGuide);
        GameObject.Find("PauseBtn").GetComponent<Button>().onClick.AddListener(TogglePause);
        GameObject.Find("TerminateBtn").GetComponent<Button>().onClick.AddListener(Terminate);

        GameObject.Find("CancelTerminateBtn").GetComponent<Button>().onClick.AddListener(CancelTerminate);
        GameObject.Find("ControlPanelCloseBtn").GetComponent<Button>().onClick.AddListener(CloseControlGuide);
        GameObject.Find("CloseChatBtn").GetComponent<Button>().onClick.AddListener(CloseChatBox);

        ToggleControlGuide();
        ToggleChat();

        isMicOn = true;
        micBtn.sprite = MIC_ON;
    }

    private void CloseChatBox()
    {
        isChatOn = false;
        chatBtn.sprite = CHAT_OFF;
        chatBox.transform.localScale = new Vector3(0, 0, 0);
    }

    private void CloseControlGuide()
    {
        isControlGuideOn = false;
        controlGuideBtn.sprite = CONTROL_GUIDE_OFF;
        closeGuidePopUp.transform.localScale = new Vector3(0, 0, 0);
    }

    private void CancelTerminate()
    {
        isTerminating = false;
        terminateBtn.sprite = TERMINATE_OFF;
        endSessionPopUp.transform.localScale = new Vector3(0, 0, 0);
    }

    private void Terminate()
    {
        if (!isTerminating)
        {
            isTerminating = true;
            terminateBtn.sprite = TERMINATE_ON;
            endSessionPopUp.transform.localScale = new Vector3(1, 1, 1);
        } else
        {
            isTerminating = false;
            terminateBtn.sprite = TERMINATE_OFF;
            endSessionPopUp.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    private void Update()
    {
        if (GameObject.Find("PausePopUp").transform.localScale == new Vector3(1, 1, 1))
        {
            isPaused = true;
            pauseBtn.sprite = PAUSE_ON;
        }
        else
        {
            isPaused = false;
            pauseBtn.sprite = PAUSE_OFF;
        }
    }

    private void TogglePause()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseBtn.sprite = PAUSE_ON;
        }
        else
        {
            isPaused = false;
            pauseBtn.sprite = PAUSE_OFF;
        }
    }

    private void ToggleControlGuide()
    {
        if (!isControlGuideOn)
        {
            isControlGuideOn = true;
            controlGuideBtn.sprite = CONTROL_GUIDE_ON;
            closeGuidePopUp.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            isControlGuideOn = false;
            controlGuideBtn.sprite = CONTROL_GUIDE_OFF;
            closeGuidePopUp.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    private void ToggleMic()
    {
        if (!isMicOn)
        {
            isMicOn = true;
            micBtn.sprite = MIC_ON;
        } else
        {
            isMicOn = false;
            micBtn.sprite = MIC_OFF;
        }
    }

    private void ToggleChat()
    {
        if (!isChatOn)
        {
            isChatOn = true;
            chatBtn.sprite = CHAT_ON;
            chatBox.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        } else
        {
            isChatOn = false;
            chatBtn.sprite = CHAT_OFF;
            chatBox.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
