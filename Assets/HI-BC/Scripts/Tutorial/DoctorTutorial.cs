using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoctorTutorial : MonoBehaviour
{
    public TMP_Text heading;
    public TMP_Text content;
    public GameObject NextButton, BackButton;
    public GameObject Loc2NextButton, Loc2BackButton;
    public GameObject Loc3NextButton, Loc3BackButton;
    public GameObject Loc4NextButton, Loc4BackButton;

    public Sprite examineOn;

    public GameObject Loc2, Loc3, Loc4;

    public GameObject next, knockDoor, door, chat, mic, nav, pause, raiseArms, washHands, examine, minimize, home, exit, teleportGuide, examineGuide, moodbarGuide; 

    private int currentState = 1; // specifies the tutorial stage
    
    void Start()
    {
        NextButton.GetComponent<Button>().onClick.AddListener(handleNextButton);
        Loc2NextButton.GetComponent<Button>().onClick.AddListener(handleNextButton);
        Loc3NextButton.GetComponent<Button>().onClick.AddListener(handleNextButton);
        Loc4NextButton.GetComponent<Button>().onClick.AddListener(handleNextButton);
        BackButton.GetComponent<Button>().onClick.AddListener(handleBackButton);
        Loc2BackButton.GetComponent<Button>().onClick.AddListener(handleBackButton);
        Loc3BackButton.GetComponent<Button>().onClick.AddListener(handleBackButton);
        Loc4BackButton.GetComponent<Button>().onClick.AddListener(handleBackButton);
    }
    
    void Update()
    {
        if (currentState == 1)
        {
            BackButton.SetActive(false);
            knockDoor.SetActive(false);
            next.SetActive(true);
            content.text = "Welcome to the doctor's interface tutorial. Please follow the blinking green square to familiarize yourself with the buttons.";
            heading.text = "Welcome!";
        }
        else if (currentState == 2)
        {
            // knock door
            next.SetActive(false);
            BackButton.SetActive(true);
            knockDoor.SetActive(true);
            door.SetActive(false);
            content.text = "You can click on the 'Knock Door' button to knock the door.";
            heading.text = "Action Panel";
        }
        else if (currentState == 3)
        {
            // inside/outside
            knockDoor.SetActive(false);
            door.SetActive(true);
            chat.SetActive(false);
            content.text = "You can click on the 'Inside/Outside' button to go inside/outside the room.";
            heading.text = "Action Panel";
        }
        else if (currentState == 4)
        {
            // chat
            door.SetActive(false);
            chat.SetActive(true);
            mic.SetActive(false);
            content.text = "The chat box will open/close when you click on the Chat Box icon. You can communicate here if the voice feature does not work.";
            heading.text = "Chat Box";
        }
        else if (currentState == 5)
        {
            // mic
            chat.SetActive(false);
            mic.SetActive(true);
            nav.SetActive(false);
            content.text = "You can do a real-time voice chat when you click on the Mic icon.";
            heading.text = "Voice Chat";
        }
        else if (currentState == 6)
        {
            // nav
            mic.SetActive(false);
            nav.SetActive(true);
            pause.SetActive(false);
            content.text = "If you forget how to rotate your view, you can open the Turn View help window.";
            heading.text = "Turn View";
        }
        else if (currentState == 7)
        {
            // pause
            nav.SetActive(false);
            pause.SetActive(true);
            examine.SetActive(false);
            content.text = "Clicking on the Pause icon will pause the role-play momentarily for a quick chat.";
            heading.text = "Time Out";
            examineGuide.SetActive(false);
            transform.localScale = new Vector3(1, 1, 1);
            Loc2.transform.localScale = new Vector3(0, 0, 0);
        }
        else if (currentState == 8)
        {
            // examine
            pause.SetActive(false);
            examine.SetActive(true);
            washHands.SetActive(false);
            content.text = "Please click on the Examine button to start examining the patient. If you are not by the table, you will automatically be teleported.";
            heading.text = "Action Panel";
            transform.localScale = new Vector3(0, 0, 0);
            Loc2.transform.localScale = new Vector3(1, 1, 1);
            if (GameObject.Find("examineGuide").transform.localScale != new Vector3(0, 0, 0))
            {
                examineGuide.SetActive(true);
                examine.SetActive(false);
            } else
            {
                examineGuide.SetActive(false);
                examine.SetActive(true);
            }
                
        }
        else if (currentState == 9)
        {
            // wash hands
            Loc2.transform.localScale = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            examineGuide.SetActive(false);
            examine.SetActive(false);
            washHands.SetActive(true);
            teleportGuide.SetActive(false);
            content.text = "Please click on the Wash Hands button to teleport to the sink.";
            heading.text = "Action Panel";
            Loc4.transform.localScale = new Vector3(0, 0, 0);
        }
        else if (currentState == 10)
        {
            // teleport back to the table
            washHands.SetActive(false);
            if (GameObject.Find("teleportGuide").transform.localScale != new Vector3(0, 0, 0))
                teleportGuide.SetActive(true);
            else
                teleportGuide.SetActive(false);
            content.text = "You can always press B on the keyboard to move back to the examination table, wherever you are.";
            heading.text = "Navigation";
            transform.localScale = new Vector3(0, 0, 0);
            Loc4.transform.localScale = new Vector3(1, 1, 1);
            moodbarGuide.SetActive(false);
            Loc3.transform.localScale = new Vector3(0, 0, 0);
        }

        else if (currentState == 11)
        {
            // moodbar
            Loc4.transform.localScale = new Vector3(0, 0, 0);
            Loc3.transform.localScale = new Vector3(1, 1, 1);
            teleportGuide.SetActive(false);
            moodbarGuide.SetActive(true);
            minimize.SetActive(false);
            content.text = "The numbers in the moodbar indicate how many times the patient expressed its corresponding emotion.";
            heading.text = "Moodbar";
        }
        else if (currentState == 12)
        {
            // minimize
            Loc3.transform.localScale = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            moodbarGuide.SetActive(false);
            minimize.SetActive(true);
            exit.SetActive(false);
            content.text = "Clicking the Minimize button will minimize the program.";
            heading.text = "Minimize Application";
        }
        else if (currentState == 13)
        {
            // exit
            minimize.SetActive(false);
            exit.SetActive(true);
            home.SetActive(false);
            NextButton.SetActive(true);
            content.text = "Clicking the Exit button will terminate and close the program.";
            heading.text = "Exit Application";
        }
        /**
        else if (currentState == 14)
        {
            // go home.SetActive(false);
            exit.SetActive(false);
            home.SetActive(true);
            NextButton.SetActive(true);
            content.text = "Clicking the Home button will terminate the session and go back to the Homescreen.";
            heading.text = "Homescreen";
        }
    **/
        else if (currentState == 14)
        {
            // Done
            content.text = "You have successfully finished the tutorial. To exit the tutorial, please quit the application using the Exit button.";
            heading.text = "Congratulations!";
            NextButton.SetActive(false);
        }

    }

    private void handleNextButton()
    {
        currentState += 1;
    }

    private void handleBackButton()
    {
        currentState -= 1;
    }

    
}
