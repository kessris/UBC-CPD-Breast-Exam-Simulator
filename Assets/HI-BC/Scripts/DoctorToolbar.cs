using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctorToolbar : MonoBehaviour
{
    [SerializeField] Sprite CHAT_ON, CHAT_OFF, MIC_ON, MIC_OFF, CONTROL_GUIDE_ON, CONTROL_GUIDE_OFF, PAUSE_ON, PAUSE_OFF;
    private Image chatBtn, micBtn, controlGuideBtn, pauseBtn;
    [SerializeField] GameObject closeGuidePopUp, chatBox;

    bool isChatOn, isMicOn, isControlGuideOn = true, isPaused;

    void Start()
    {
        chatBtn = GameObject.Find("ChatBtn").GetComponent<Image>();
        micBtn = GameObject.Find("MicBtn").GetComponent<Image>();
        controlGuideBtn = GameObject.Find("ControlBtn").GetComponent<Image>();
        pauseBtn = GameObject.Find("PauseBtn").GetComponent<Image>();

        GameObject.Find("ChatBtn").GetComponent<Button>().onClick.AddListener(ToggleChat);
        GameObject.Find("MicBtn").GetComponent<Button>().onClick.AddListener(ToggleMic);
        GameObject.Find("ControlBtn").GetComponent<Button>().onClick.AddListener(ToggleControlGuide);
        GameObject.Find("PauseBtn").GetComponent<Button>().onClick.AddListener(TogglePause);

        GameObject.Find("ControlPanelCloseBtn").GetComponent<Button>().onClick.AddListener(CloseControlGuide);
        GameObject.Find("CloseChatBtn").GetComponent<Button>().onClick.AddListener(CloseChatBox);

        ToggleChat();
        ToggleControlGuide();

        isMicOn = true;
        micBtn.sprite = MIC_ON;
    }

    private void Update()
    {
        if (GameObject.Find("PausePopUp").transform.localScale == new Vector3(1,1,1))
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

    private void CloseChatBox()
    {
        isChatOn = false;
        chatBtn.sprite = CHAT_OFF;
        chatBox.transform.localScale = new Vector3(0, 0, 0);
    }

    public void CloseControlGuide()
    {
        isControlGuideOn = false;
        controlGuideBtn.sprite = CONTROL_GUIDE_OFF;
        closeGuidePopUp.transform.localScale = new Vector3(0, 0, 0);
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
        }
        else
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
        }
        else
        {
            isChatOn = false;
            chatBtn.sprite = CHAT_OFF;
            chatBox.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
