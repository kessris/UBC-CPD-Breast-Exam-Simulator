using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatientTutorial : MonoBehaviour
{
    public TMP_Text heading;
    public TMP_Text content;

    public TMP_Text heading2;
    public TMP_Text content2;

    public TMP_Text heading3;
    public TMP_Text content3;

    public GameObject NextButton, BackButton;

    public GameObject Loc2, Loc3;

    public GameObject Loc3NextBtn;

    public GameObject next, minimize, exit, home, chat, mic, nav, pause, terminate, sit, actionPanel, undress, undressOptions, tripod, liedown, lyingActions, standup, moodbarButtons, moodbarEmoji, moodbarNums;

    private int currentState = 1; // specifies the tutorial stage

    void Start()
    {
        NextButton.GetComponent<Button>().onClick.AddListener(handleNextButton);
        BackButton.GetComponent<Button>().onClick.AddListener(handleBackButton);
    }

    private void Update()
    {
        if (currentState == 1)
        {
            BackButton.SetActive(false);
            chat.SetActive(false);
            next.SetActive(true);
            content.text = "Welcome to the patient's interface tutorial. Please follow the blinking green square to familiarize yourself with the buttons.";
            heading.text = "Welcome!";
        }
        else if (currentState == 2)
        {
            // chat
            next.SetActive(false);
            BackButton.SetActive(true);
            chat.SetActive(true);
            mic.SetActive(false);
            content.text = "The chat box will open/close when you click on the Chat Box icon. You can communicate here if the voice feature does not work.";
            heading.text = "Chat Box";
        }
        else if (currentState == 3)
        {
            // mic
            chat.SetActive(false);
            mic.SetActive(true);
            nav.SetActive(false);
            content.text = "You can do a real-time voice chat when you click on the Mic icon.";
            heading.text = "Voice Chat";
        }
        else if (currentState == 4)
        {
            // nav
            mic.SetActive(false);
            nav.SetActive(true);
            pause.SetActive(false);
            content.text = "If you forget how to rotate your view, you can open the Turn View help window.";
            heading.text = "Turn View";
        }
        else if (currentState == 5)
        {
            // pause
            nav.SetActive(false);
            pause.SetActive(true);
            content.text = "Clicking on the Pause icon will pause the role-play momentarily for a quick chat.";
            heading.text = "Time Out";
            terminate.SetActive(false);
        }
        else if (currentState == 6)
        {
            // terminate
            pause.SetActive(false);
            terminate.SetActive(true);
            sit.SetActive(false);
            content.text = "When you click on the End Session icon, you can end the session from all players' sides.";
            heading.text = "End Session";
        }
        else if (currentState == 7)
        {
            // sit
            terminate.SetActive(false);
            sit.SetActive(true);
            actionPanel.SetActive(false);
            content.text = "Click on the Sit Down button to sit on the examination table.";
            heading.text = "Action Panel";
        }
        else if (currentState == 8)
        {
            // actionpanels
            sit.SetActive(false);
            actionPanel.SetActive(true);
            undress.SetActive(false);
            content.text = "These are the actions you can perform while you are sitting down.";
            heading.text = "Action Panel";
        }
        else if (currentState == 9)
        {
            // undress
            actionPanel.SetActive(false);
            undress.SetActive(true);
            content.text = "Let's start with the Change Clothes button.";
            heading.text = "Action Panel";

            actionPanel.SetActive(false);
        }
        else if (currentState == 10)
        {
            // free actions
            undress.SetActive(false);
            actionPanel.SetActive(true);
            liedown.SetActive(false);
            content.text = "Please try all the action buttons until you feel comfortable.";
            heading.text = "Action Panel";
        }
        else if (currentState == 11)
        {
            // lie down
            actionPanel.SetActive(false);
            liedown.SetActive(true);
            lyingActions.SetActive(false);
            content.text = "Now, let's try to lie down.";
            heading.text = "Action Panel";
        }
        else if (currentState == 12)
        {
            // lie down actions
            liedown.SetActive(false);
            lyingActions.SetActive(true);
            standup.SetActive(false);
            content.text = "Great job! Please try these action buttons while you are lying down.";
            heading.text = "Action Panel";
        }
        else if (currentState == 13)
        {
            // lie down actions
            transform.localScale = new Vector3(1, 1, 1);
            Loc2.transform.localScale = new Vector3(0, 0, 0);
            lyingActions.SetActive(false);
            standup.SetActive(true);
            moodbarEmoji.SetActive(false);
            content.text = "Now, try clicking on the Stand Up button to stand.";
            heading.text = "Action Panel";
        }
        else if (currentState == 14)
        {
            // moodbar emoji
            transform.localScale = new Vector3(0, 0, 0);
            Loc2.transform.localScale = new Vector3(1, 1, 1);

            standup.SetActive(false);
            moodbarEmoji.SetActive(true);
            moodbarNums.SetActive(false);
            content2.text = "You can use the mood bar to express your emotion. Once clicked, both the patient and the doctor will see the emoji pop up from the patient's head.";
            heading2.text = "Mood Bar";
        }
        else if (currentState == 15)
        {
            // mood num
            Loc3.transform.localScale = new Vector3(0, 0, 0);
            Loc2.transform.localScale = new Vector3(1, 1, 1);
            moodbarEmoji.SetActive(false);
            moodbarNums.SetActive(true);
            minimize.SetActive(false);
            content2.text = "The counters at the top records how many times each emoji was clicked.";
            heading2.text = "Mood Bar";
        }
        else if (currentState == 16)
        {
            // minimize
            Loc2.transform.localScale = new Vector3(0, 0, 0);
            Loc3.transform.localScale = new Vector3(1, 1, 1);
            moodbarNums.SetActive(false);
            minimize.SetActive(true);
            exit.SetActive(false);
            content3.text = "Clicking the Minimize button will minimize the program.";
            heading3.text = "Minimize Application";
        }
        else if (currentState == 17)
        {
            // exit
            minimize.SetActive(false);
            exit.SetActive(true);
            home.SetActive(false);
            NextButton.SetActive(true);
            Loc3NextBtn.SetActive(true);
            content3.text = "Clicking the Exit button will terminate and close the program.";
            heading3.text = "Exit Application";
        }
        /**
        else if (currentState == 18)
        {
            // go home.SetActive(false);
            exit.SetActive(false);
            home.SetActive(true);
            NextButton.SetActive(true);
            content3.text = "Clicking the Home button will terminate the session and go back to the Homescreen.";
            heading3.text = "Homescreen";
            Loc3NextBtn.SetActive(true);
        }
    **/
        else if (currentState == 18)
        {
            // Done
            content3.text = "You have successfully finished the tutorial. To exit the tutorial, please click on the Home button at the top.";
            heading3.text = "Congratulations!";
            NextButton.SetActive(false);
            Loc3NextBtn.SetActive(false);
        }
    }

    public void handleNextButton()
    {
        currentState += 1;
    }

    public void handleBackButton()
    {
        currentState -= 1;
    }
}
