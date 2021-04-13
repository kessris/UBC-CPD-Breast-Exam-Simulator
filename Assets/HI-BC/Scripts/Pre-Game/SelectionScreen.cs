using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    public GameObject welcomeScreen, modeScreen, roleScreen, genderScreen, roomScreen;

    string mode = "tutorial";
    public string role = "patient";
    string gender = "male";
    string room = "1";

    bool isIpActive = false;

    public Sprite TUTORIAL_YES, TUTORIAL_NO, TUTORIAL_TXT_YES, TUTORIAL_TXT_NO, ROLEPLAY_YES, ROLEPLAY_NO, ROLEPLAY_TXT_YES, ROLEPLAY_TXT_NO, OBSERVER_YES, OBSERVER_NO,
        OBSERVER_TXT_YES, OBSERVER_TXT_NO, DOCTOR_YES, DOCTOR_NO, DOCTOR_TXT_YES, DOCTOR_TXT_NO, PATIENT_YES, PATIENT_NO, PATIENT_TXT_YES,
        PATIENT_TXT_NO, MALE_YES, MALE_NO, FEMALE_YES, FEMALE_NO, ROOM1_YES, ROOM1_NO, ROOM2_YES, ROOM2_NO,
        MALE_TXT_YES, MALE_TXT_NO, FEMALE_TXT_YES, FEMALE_TXT_NO;

    public GameObject nextBtn, backBtn, IPfield;

    public Image tutorial, roleplay, observer, doctor, patient, male, female;
    public Image tutorial_txt, roleplay_txt, observer_txt, doctor_txt, patient_txt, male_txt, female_txt, room1_txt, room2_txt;

    // Start is called before the first frame update
    void Start()
    {
        nextBtn.GetComponent<Button>().onClick.AddListener(NextScreen);
        backBtn.GetComponent<Button>().onClick.AddListener(PrevScreen);
    }

    public void ChangeMode(string mode)
    {
        this.mode = mode;

    }

    public void ChangeRole(string role)
    {
        this.role = role;
    }

    public void ChangeGender(string gender)
    {
        this.gender = gender;
    }

    public void ChangeRoom(string room)
    {
        this.room = room;
    }

    public void NextScreen()
    {
        if (welcomeScreen.activeSelf)
        {
            // enable next/back buttons
            nextBtn.SetActive(true);
            backBtn.SetActive(true);

            welcomeScreen.SetActive(false);
            modeScreen.SetActive(true);
        }
        else if (modeScreen.activeSelf)
        {
            if (CrossSceneInformation.mode == "observer")
            {
                modeScreen.SetActive(false);
                roomScreen.SetActive(true);
            }
            else
            {
                modeScreen.SetActive(false);
                //roleScreen.SetActive(true);
                genderScreen.SetActive(true);
            }
        }
        else if (roleScreen.activeSelf)
        {
            roleScreen.SetActive(false);
            genderScreen.SetActive(true);
        }
        else if (genderScreen.activeSelf)
        {
            if (CrossSceneInformation.mode == "tutorial")
            {
                GameObject.Find("Canvas").GetComponent<MultiplayerMenu>().Host();
            }
            else
            {
                genderScreen.SetActive(false);
                roomScreen.SetActive(true);
            }

        }
        else if (roomScreen.activeSelf)
        {

        }
    }

    public void PrevScreen()
    {
        if (modeScreen.activeSelf)
        {
            backBtn.SetActive(false);
            modeScreen.SetActive(false);
            welcomeScreen.SetActive(true);
        }
        else if (roleScreen.activeSelf)
        {
            roleScreen.SetActive(false);
            modeScreen.SetActive(true);
        }
        else if (genderScreen.activeSelf)
        {
            genderScreen.SetActive(false);
            //roleScreen.SetActive(true);
            modeScreen.SetActive(true);
        }
        else if (roomScreen.activeSelf)
        {
            if (CrossSceneInformation.mode == "observer")
            {
                modeScreen.SetActive(true);
                roomScreen.SetActive(false);
            }
            else
            {
                genderScreen.SetActive(true);
                roomScreen.SetActive(false);
            }
            nextBtn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (welcomeScreen.activeSelf)
        {
            // disable back/next btns
            backBtn.SetActive(false);
        }
        else if (roomScreen.activeSelf)
        {
            nextBtn.SetActive(false);
            if (Input.GetKey(KeyCode.F1))
            {
                if (!isIpActive)
                {
                    IPfield.transform.localScale = new Vector3(1, 1, 1);
                    isIpActive = true;
                    System.Threading.Thread.Sleep(250);
                } else
                {
                    IPfield.transform.localScale = new Vector3(0, 0, 0);
                    isIpActive = false;
                    System.Threading.Thread.Sleep(250);
                }
                
            }

        }

        CrossSceneInformation.mode = mode;
        CrossSceneInformation.role = role;
        CrossSceneInformation.gender = gender;
        CrossSceneInformation.room = room;

        if (mode == "tutorial")
        {
            tutorial.sprite = TUTORIAL_YES;
            tutorial_txt.sprite = TUTORIAL_TXT_YES;

            roleplay.sprite = ROLEPLAY_NO;
            roleplay_txt.sprite = ROLEPLAY_TXT_NO;

            observer.sprite = OBSERVER_NO;
            observer_txt.sprite = OBSERVER_TXT_NO;
        }
        else if (mode == "roleplay")
        {
            tutorial.sprite = TUTORIAL_NO;
            tutorial_txt.sprite = TUTORIAL_TXT_NO;

            roleplay.sprite = ROLEPLAY_YES;
            roleplay_txt.sprite = ROLEPLAY_TXT_YES;

            observer.sprite = OBSERVER_NO;
            observer_txt.sprite = OBSERVER_TXT_NO;
        }
        else if (mode == "observer")
        {
            tutorial.sprite = TUTORIAL_NO;
            tutorial_txt.sprite = TUTORIAL_TXT_NO;

            roleplay.sprite = ROLEPLAY_NO;
            roleplay_txt.sprite = ROLEPLAY_TXT_NO;

            observer.sprite = OBSERVER_YES;
            observer_txt.sprite = OBSERVER_TXT_YES;
        }

        if (role == "doctor")
        {
            doctor.sprite = DOCTOR_YES;
            patient.sprite = PATIENT_NO;

            doctor_txt.sprite = DOCTOR_TXT_YES;
            patient_txt.sprite = PATIENT_TXT_NO;
        }
        else if (role == "patient")
        {
            doctor.sprite = DOCTOR_NO;
            patient.sprite = PATIENT_YES;

            doctor_txt.sprite = DOCTOR_TXT_NO;
            patient_txt.sprite = PATIENT_TXT_YES;
        }

        if (gender == "male")
        {
            male.sprite = MALE_YES;
            female.sprite = FEMALE_NO;

            male_txt.sprite = MALE_TXT_YES;
            female_txt.sprite = FEMALE_TXT_NO;
        }
        else if (gender == "female")
        {
            male.sprite = MALE_NO;
            female.sprite = FEMALE_YES;

            male_txt.sprite = MALE_TXT_NO;
            female_txt.sprite = FEMALE_TXT_YES;
        }

        if (room == "1")
        {
            room1_txt.sprite = ROOM1_YES;
            room2_txt.sprite = ROOM2_NO;
            if (GameObject.Find("Port Number"))
                GameObject.Find("Port Number").GetComponent<InputField>().text = "15936";
        }
        else if (room == "2")
        {
            room1_txt.sprite = ROOM1_NO;
            room2_txt.sprite = ROOM2_YES;
            if (GameObject.Find("Port Number"))
                GameObject.Find("Port Number").GetComponent<InputField>().text = "15937";
        }
    }
}
