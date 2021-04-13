using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatientOnboarding : MonoBehaviour
{
    string currentStage; // nav1 = WASD, nav2 = W, nav3 = S, nav4 = A, nav5 = D, act1 = sit, act2 = stand, free = done
    public TextMeshProUGUI navigationPanelText;
    public TextMeshProUGUI actionPanelText;
    public Button nextBtn;
    public Text nextBtnText;
    public GameObject nextBtnGlow;
    public GameObject glowW;
    public GameObject glowS;
    public GameObject glowA;
    public GameObject glowD;
    public GameObject glowSitDown;
    public GameObject glowStandUp;

    public GameObject actionPanel;

    Image sitDownBtn;
    Image standUpBtn;

    // Start is called before the first frame update
    void Start()
    {
        currentStage = "nav1";
        nextBtn = GameObject.Find("ArrowBtn").GetComponent<Button>();
        nextBtn.onClick.AddListener(HandleNextButtonClick);
        sitDownBtn = GameObject.Find("SitDown").GetComponent<Image>();
        standUpBtn = GameObject.Find("StandUp").GetComponent<Image>();
        GameObject.Find("SitDown").GetComponent<Button>().onClick.AddListener(HandleSitDownButtonClick);
        GameObject.Find("StandUp").GetComponent<Button>().onClick.AddListener(HandleStandUpButtonClick);
    }

    void Update()
    {
        if (currentStage == "nav2" && Input.GetKeyDown(KeyCode.W))
        {
            currentStage = "nav3";
            GameObject.Find("NextBtn").SetActive(false);
            navigationPanelText.text = "Great job! Now, press S key to move backward.";
            glowW.SetActive(false);
            glowS.SetActive(true);
        } else if (currentStage == "nav3" && Input.GetKeyDown(KeyCode.S))
        {
            currentStage = "nav4";
            navigationPanelText.text = "Nice job! Now, press A key to rotate right.";
            glowS.SetActive(false);
            glowA.SetActive(true);
        } else if (currentStage == "nav4" && Input.GetKeyDown(KeyCode.A))
        {
            currentStage = "nav5";
            navigationPanelText.text = "Great! Now, press D key to rotate left.";
            glowA.SetActive(false);
            glowD.SetActive(true);
        } else if (currentStage == "nav5" && Input.GetKeyDown(KeyCode.D))
        {
            currentStage = "act1";
            navigationPanelText.text = "Now, go near the examination table and click on the 'Sit Down' button.";
            glowD.SetActive(false);
            glowSitDown.SetActive(true);
            GameObject.Find("NavTitle").GetComponent<TextMeshProUGUI>().text = "Action";
            actionPanel.SetActive(true);
        }
    }

    public void HandleNextButtonClick()
    {
        if (currentStage == "nav1")
        {
            // if nav1, change text
            navigationPanelText.text = "Now, press W key to move forward.";
            nextBtnText.text = "Back";
            nextBtn.transform.localRotation = Quaternion.Euler(0, 180, 0);
            nextBtnGlow.SetActive(false);
            glowW.SetActive(true);
            currentStage = "nav2";
        }
        else
        {
            // is in nav2
            navigationPanelText.text = "Move around by clicking WASD or arrow keys on your keyboard.";
            nextBtnText.text = "Next";
            nextBtn.transform.localRotation = Quaternion.Euler(0, 180, 0);
            nextBtnGlow.SetActive(true);
            glowW.SetActive(false);
            currentStage = "nav1";
        }
    }

    private void HandleStandUpButtonClick()
    {
        currentStage = "free";
        glowStandUp.SetActive(false);
        GameObject.Find("NavTitle").GetComponent<TextMeshProUGUI>().text = "Congratulations!";
        navigationPanelText.text = "That's it for the basic navigation. You are now free to explore around this room. To go back to the main menu, press..";
        actionPanel.SetActive(false);
    }

    private void HandleSitDownButtonClick()
    {
        if (currentStage == "act1" && sitDownBtn.sprite.name != "Group 21")
        {
            currentStage = "act2";
            glowSitDown.SetActive(false);
            glowStandUp.SetActive(true);
            navigationPanelText.text = "Now, try to click on the 'Stand Up' button.";
        }
    }
}
