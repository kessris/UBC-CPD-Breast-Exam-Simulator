using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PatientButtonSetup : MonoBehaviour
{

    Transform sittingLocation;
    Transform lyingLocation;
    Transform standUpLocation;

    public RectTransform moodbar;
    
    Animator anim;

    float lyingMoodbarPosition;
    float sittingMoodbarPosition;
    float standingMoodbarPosition;

    PlayerNetwork playerNetwork;

    bool isSitting = false;
    bool isStanding = true;
    bool isLyingDown = false;

    Image sit, stand, lie, right, left, both, right45, left45, undress, tripod, tender;

    [SerializeField] Sprite SIT_ON, SIT_OFF, STAND_ON, STAND_OFF, LIE_ON, LIE_OFF, RIGHT_ON, RIGHT_OFF, LEFT_ON, LEFT_OFF, BOTH_ON, BOTH_OFF,
        RIGHT_45_ON, RIGHT_45_OFF, LEFT_45_ON, LEFT_45_OFF, UNDRESS_ON, UNDRESS_OFF, TRIPOD_ON, TRIPOD_OFF, TENDER_ON, TENDER_OFF, CHANGE_TO_GOWN_ON, CHANGE_TO_GOWN_OFF,
        OPEN_RIGHT_SIDE_ON, OPEN_RIGHT_SIDE_OFF, OPEN_LEFT_SIDE_ON, OPEN_LEFT_SIDE_OFF, OPEN_BOTH_ON, OPEN_BOTH_OFF, REMOVE_GOWN_ON, REMOVE_GOWN_OFF;

    Color invalid;
    Color valid;

    [SerializeField]
    GameObject gownNormal, gownLeft, gownRight, gownOpen, casualClothes;
    [SerializeField]
    Text _moodBarText;
    [SerializeField]
    Image _moodBarImg;
    int gownSelection = 1;
    bool isUndressPanelOpen = false;
    Image changeToGown, openRightSide, openLeftSide, openBoth, removeGown;

    Button leftArmBtn, rightArmBtn, bothArmsBtn, undressBtn, tripodBtn, tenderBtn, left45Btn, right45Btn;

    GameObject Chat;

    private string action = "none";
    private string position = "none";

    GameObject notificationlabel;

    private void Start()
    {
        anim = GetComponent<Animator>();

        Chat = GameObject.Find("Chat");

        standingMoodbarPosition = moodbar.localPosition.y;
        sittingMoodbarPosition = moodbar.localPosition.y - 50;
        lyingMoodbarPosition = moodbar.localPosition.y - 100;

        Button sitBtn = GameObject.Find("SitDown").GetComponent<Button>();
        Button standBtn = GameObject.Find("StandUp").GetComponent<Button>();
        Button lieBtn = GameObject.Find("LieDown").GetComponent<Button>();

        //leftArmBtn = GameObject.Find("RaiseLeftArm").GetComponent<Button>();
        //rightArmBtn = GameObject.Find("RaiseRightArm").GetComponent<Button>();
        bothArmsBtn = GameObject.Find("RaiseBothArms").GetComponent<Button>();

        left45Btn = GameObject.Find("LeftUp45deg").GetComponent<Button>();
        right45Btn = GameObject.Find("RightUp45Deg").GetComponent<Button>();
        undressBtn = GameObject.Find("Undress").GetComponent<Button>();

        tripodBtn = GameObject.Find("Tripod").GetComponent<Button>();
        tenderBtn = GameObject.Find("Point").GetComponent<Button>();

        changeToGown = GameObject.Find("ChangeToGown").GetComponent<Image>();
        openRightSide = GameObject.Find("OpenRightSide").GetComponent<Image>();
        openLeftSide = GameObject.Find("OpenLeftSide").GetComponent<Image>();
        openBoth = GameObject.Find("OpenBothSide").GetComponent<Image>();
        removeGown = GameObject.Find("RemoveGown").GetComponent<Image>();


        ColorUtility.TryParseHtmlString("#666666", out invalid);
        ColorUtility.TryParseHtmlString("#FFFFFF", out valid);
        
        sitBtn.onClick.AddListener(Sit);
        standBtn.onClick.AddListener(StandUp);
        lieBtn.onClick.AddListener(LieDown);
        //leftArmBtn.onClick.AddListener(RaiseLeft);
        //rightArmBtn.onClick.AddListener(RaiseRight);
        bothArmsBtn.onClick.AddListener(RaiseBoth);
        left45Btn.onClick.AddListener(RaiseLeft45);
        right45Btn.onClick.AddListener(RaiseRight45);
        undressBtn.onClick.AddListener(Undress);
        tripodBtn.onClick.AddListener(Tripod);
        tenderBtn.onClick.AddListener(Tender);

        GameObject.Find("ChangeToGown").GetComponent<Button>().onClick.AddListener(ChangeToGown);
        GameObject.Find("OpenRightSide").GetComponent<Button>().onClick.AddListener(OpenRightSide);
        GameObject.Find("OpenLeftSide").GetComponent<Button>().onClick.AddListener(OpenLeftSide);
        GameObject.Find("OpenBothSide").GetComponent<Button>().onClick.AddListener(OpenBothSide);
        GameObject.Find("RemoveGown").GetComponent<Button>().onClick.AddListener(RemoveGown);

        sit = GameObject.Find("SitDown").GetComponent<Image>();
        stand = GameObject.Find("StandUp").GetComponent<Image>(); 
        lie = GameObject.Find("LieDown").GetComponent<Image>();
        //right = GameObject.Find("RaiseRightArm").GetComponent<Image>();
        //left = GameObject.Find("RaiseLeftArm").GetComponent<Image>();
        both = GameObject.Find("RaiseBothArms").GetComponent<Image>();
        right45 = GameObject.Find("RightUp45Deg").GetComponent<Image>();
        left45 = GameObject.Find("LeftUp45deg").GetComponent<Image>();
        undress = GameObject.Find("Undress").GetComponent<Image>();
        tripod = GameObject.Find("Tripod").GetComponent<Image>();
        tender = GameObject.Find("Point").GetComponent<Image>();

        sittingLocation = GameObject.Find("SittingLocation").transform;
        lyingLocation = GameObject.Find("LyingLocation").transform;
        standUpLocation = GameObject.Find("StandingLocation").transform;

        playerNetwork = GetComponent<PlayerNetwork>();
        
        StandUp();

        notificationlabel = GameObject.Find("NotificationLabel");
        notificationlabel.SetActive(false);
    }

    private void Update()
    {
        // FOR TRANSCRIPT FUNCTIONALITY
        
        if (both.sprite == BOTH_ON)
            action = "both arms up";
        else if (right45.sprite == RIGHT_45_ON)
            action = "right arm up 45 degrees";
        else if (left45.sprite == LEFT_45_ON)
            action = "left arm up 45 degrees";
        else if (tripod.sprite == TRIPOD_ON)
            action = "tripod";
        else if (tender.sprite == TENDER_ON)
            action = "point at tender area";
        else
            action = "none";

        if (isStanding)
            position = "standing";
        else if (isSitting)
            position = "sitting";
        else if (isLyingDown)
            position = "lying down";
        
        if (Chat.GetComponent<ChatManager>().action != action || Chat.GetComponent<ChatManager>().position != position)
            Chat.GetComponent<ChatManager>().UpdateActionPosition(action, position);

      
    }


    private void RemoveGown()
    {
        changeToGown.sprite = CHANGE_TO_GOWN_OFF;
        openRightSide.sprite = OPEN_RIGHT_SIDE_OFF;
        openLeftSide.sprite = OPEN_LEFT_SIDE_OFF;
        openBoth.sprite = OPEN_BOTH_OFF;
        removeGown.sprite = REMOVE_GOWN_ON;

        gownSelection = 5;

        gownNormal.SetActive(false);
        gownLeft.SetActive(false);
        gownRight.SetActive(false);
        gownOpen.SetActive(false);

        playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_OFF);

        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
            return;
        }

    }


    private void OpenBothSide()
    {
        changeToGown.sprite = CHANGE_TO_GOWN_OFF;
        openRightSide.sprite = OPEN_RIGHT_SIDE_OFF;
        openLeftSide.sprite = OPEN_LEFT_SIDE_OFF;
        openBoth.sprite = OPEN_BOTH_ON;
        removeGown.sprite = REMOVE_GOWN_OFF;

        gownSelection = 4;

        gownNormal.SetActive(false);
        gownLeft.SetActive(false);
        gownRight.SetActive(false);
        gownOpen.SetActive(true);
        playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_OPEN);

        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
            return;
        }
    }

    private void OpenLeftSide()
    {
        changeToGown.sprite = CHANGE_TO_GOWN_OFF;
        openRightSide.sprite = OPEN_RIGHT_SIDE_OFF;
        openLeftSide.sprite = OPEN_LEFT_SIDE_ON;
        openBoth.sprite = OPEN_BOTH_OFF;
        removeGown.sprite = REMOVE_GOWN_OFF;

        gownSelection = 3;

        gownNormal.SetActive(false);
        gownLeft.SetActive(false);
        gownRight.SetActive(false);
        gownOpen.SetActive(false);
        gownRight.SetActive(true);
        playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_RIGHT);

        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
            return;
        }
    }

    private void OpenRightSide()
    {
        changeToGown.sprite = CHANGE_TO_GOWN_OFF;
        openRightSide.sprite = OPEN_RIGHT_SIDE_ON;
        openLeftSide.sprite = OPEN_LEFT_SIDE_OFF;
        openBoth.sprite = OPEN_BOTH_OFF;
        removeGown.sprite = REMOVE_GOWN_OFF;

        gownSelection = 2;

        gownNormal.SetActive(false);
        gownLeft.SetActive(false);
        gownRight.SetActive(false);
        gownOpen.SetActive(false);
        gownLeft.SetActive(true);
        playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_LEFT);

        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
            return;
        }
    }

    private void ChangeToGown()
    {
        changeToGown.sprite = CHANGE_TO_GOWN_ON;
        openRightSide.sprite = OPEN_RIGHT_SIDE_OFF;
        openLeftSide.sprite = OPEN_LEFT_SIDE_OFF;
        openBoth.sprite = OPEN_BOTH_OFF;
        removeGown.sprite = REMOVE_GOWN_OFF;

        gownSelection = 1;

        casualClothes.SetActive(false);
        gownLeft.SetActive(false);
        gownRight.SetActive(false);
        gownOpen.SetActive(false);
        gownNormal.SetActive(true);
        playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_NORMAL);

        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
            return;
        }
    }

    private void Tender()
    {
        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
        }

        if (!isSitting) return;

        ToggleOffEveryButtonsExcept("tender");

        playerNetwork.SynchAnimation(Actions.CTA_Patient_Sitting_TouchTender);
    }

    private void Tripod()
    {
        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
        }

        if (!isSitting) return;

        ToggleOffEveryButtonsExcept("tripod");

        playerNetwork.SynchAnimation(Actions.CTA_Patient_Sitting_Tripod);
    }

    int toggle;
    private void Undress()
    {
        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
            return;
        }

        // show pop up panel
        undress.sprite = UNDRESS_ON;
        isUndressPanelOpen = true;
        GameObject.Find("UndressPopUp").transform.localScale = new Vector3(1, 1, 1);
        /**
        if (gownSelection == 1)
        {
            gownNormal.SetActive(true);
            playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_NORMAL);
            gownSelection++;
        }
        else if (gownSelection == 2)
        {
            gownLeft.SetActive(true);
            playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_LEFT);
            gownSelection++;
        }
        else if (gownSelection == 3)
        {
            gownRight.SetActive(true);
            playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_RIGHT);
            gownSelection++;
        }
        else if (gownSelection == 4)
        {
            gownOpen.SetActive(true);
            playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_OPEN);
            gownSelection = 1;
        }


        //ToggleOffEveryButtonsExcept("undress");

        gownNormal.SetActive(false);
        gownLeft.SetActive(false);
        gownRight.SetActive(false);
        gownOpen.SetActive(false);

        if (gownSelection == 1)
        {
            gownNormal.SetActive(true);
            playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_NORMAL);
            gownSelection++;
        }
        else if (gownSelection == 2)
        {
            gownLeft.SetActive(true);
            playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_LEFT);
            gownSelection++;
        }
        else if (gownSelection == 3)
        {
            gownRight.SetActive(true);
            playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_RIGHT);
            gownSelection++;
        }else if(gownSelection == 4)
        {
            gownOpen.SetActive(true);
            playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_OPEN);
            gownSelection = 1;
        }
        **/
    }

    private void RaiseRight45()
    {
        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;

        }
        if (isStanding) return;

        ToggleOffEveryButtonsExcept("right45");

        if (isLyingDown)
        {
            playerNetwork.SynchAnimation(Actions.CTA_Patient_Lying_RaiseRight);
        }
        else
        {

            playerNetwork.SynchAnimation(Actions.CTA_Patient_Sitting_RaiseRight);
        }

    }

    private void RaiseBoth()
    {
        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
        }

        if (!isSitting && !isLyingDown) return;

        ToggleOffEveryButtonsExcept("both");

        playerNetwork.SynchAnimation(Actions.CTA_Patient_Sitting_RaiseBoth);
    }

    private void RaiseLeft45()
    {
        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
        }

        if (isStanding) return;
        ToggleOffEveryButtonsExcept("left45");

        if (isLyingDown)
        {
            playerNetwork.SynchAnimation(Actions.CTA_Patient_Lying_RaiseLeft);
        }
        else
        {
            playerNetwork.SynchAnimation(Actions.CTA_Patient_Sitting_RaiseLeft);
        }

    }

    private void RaiseRight()
    {
        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
        }

        if (isStanding) return;

        ToggleOffEveryButtonsExcept("right");

        if (isSitting)
        {
            playerNetwork.SynchAnimation(Actions.CTA_Patient_Sitting_RaiseRight);
        }
        else if (isLyingDown)
        {
            playerNetwork.SynchAnimation(Actions.CTA_Patient_Lying_RaiseRight);
        }

    }

    private void RaiseLeft()
    {
        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
        }

        if (isStanding) return;

        ToggleOffEveryButtonsExcept("left");

        if(isSitting)
        {
            playerNetwork.SynchAnimation(Actions.CTA_Patient_Sitting_RaiseLeft);
        }
        else if (isLyingDown)
        {
            playerNetwork.SynchAnimation(Actions.CTA_Patient_Lying_RaiseLeft);
        }


    }


    private void LieDown()
    {
        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
        }

        if (isLyingDown) return;

        moodbar.localPosition = new Vector2(35f, lyingMoodbarPosition);

        isSitting = false; isLyingDown = true; isStanding = false;
        sit.sprite = SIT_OFF; stand.sprite = STAND_OFF; lie.sprite = LIE_ON;
        ToggleOffEveryButtonsExcept("lie");

        //leftArmBtn.interactable = true;
       // rightArmBtn.interactable = true;
        undressBtn.interactable = true;
        bothArmsBtn.interactable = false;
        tripodBtn.interactable = false;
        tenderBtn.interactable = false;
        left45Btn.interactable = true;
        right45Btn.interactable = true;

        TogglePlayerMovement(false);
        transform.position = lyingLocation.position;
        transform.rotation = lyingLocation.rotation;


        playerNetwork.SynchAnimation(Actions.CTA_Patient_Lying);
    }


    public void Sit()
    {
        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
        }

        if (Vector3.Distance(transform.position, sittingLocation.position) > 2f || isSitting) return;

        moodbar.localPosition = new Vector2(35f, sittingMoodbarPosition);

        isSitting = true; isLyingDown = false; isStanding = false;
        sit.sprite = SIT_ON; stand.sprite = STAND_OFF; lie.sprite = LIE_OFF;
        ToggleOffEveryButtonsExcept("stand");

        //leftArmBtn.interactable = false;
        //rightArmBtn.interactable = false;
        bothArmsBtn.interactable = true;
        tripodBtn.interactable = true;
        tenderBtn.interactable = true;
        undressBtn.interactable = true;
        left45Btn.interactable = true;
        right45Btn.interactable = true;

        TogglePlayerMovement(false);
        transform.position = sittingLocation.position;
        transform.rotation = sittingLocation.rotation;

        playerNetwork.SynchAnimation(Actions.CTA_Patient_Sit);
    }

    void StandUp()
    {
        if (isUndressPanelOpen)
        {
            GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
            isUndressPanelOpen = false;
            undress.sprite = UNDRESS_OFF;
        }

        moodbar.localPosition = new Vector2(35f, standingMoodbarPosition);

        changeToGown.sprite = CHANGE_TO_GOWN_OFF;
        openRightSide.sprite = OPEN_RIGHT_SIDE_OFF;
        openLeftSide.sprite = OPEN_LEFT_SIDE_OFF;
        openBoth.sprite = OPEN_BOTH_OFF;

        GameObject.Find("UndressPopUp").transform.localScale = new Vector3(0, 0, 0);
        isUndressPanelOpen = false;
        undress.sprite = UNDRESS_OFF;

        isSitting = false; isLyingDown = false; isStanding = true;
        sit.sprite = SIT_OFF; stand.sprite = STAND_ON; lie.sprite = LIE_OFF;

        ToggleOffEveryButtonsExcept("");
        //leftArmBtn.interactable = false;
        //rightArmBtn.interactable = false;
        bothArmsBtn.interactable = false;
        tripodBtn.interactable = false;
        tenderBtn.interactable = false;
        undressBtn.interactable = false;
        left45Btn.interactable = false;
        right45Btn.interactable = false;


        //playerNetwork.SynchAnimation(Actions.CTA_PATIENT_GOWN_NORMAL);
        playerNetwork.SynchAnimation(Actions.CTA_Patient_Stand);
        //casualClothes.SetActive(true);

        transform.position = standUpLocation.position;
        transform.rotation = standUpLocation.rotation;
      
        TogglePlayerMovement(true);
    }


    void TogglePlayerMovement(bool state)
    {

        GetComponent<PlayerMoveController>().enabled = state;
        GetComponent<Collider>().enabled = state;
        GetComponent<Rigidbody>().useGravity = state;
    }

    IEnumerator NotifyLabel(string msg)
    {
        notificationlabel.SetActive(true);
        notificationlabel.transform.GetChild(0).GetComponent<Text>().text = msg;
        yield return new WaitForSeconds(3f);
        notificationlabel.SetActive(false);
    }

    public void NotifyObserverMode()
    {
        print("hello dudes");
        StartCoroutine(NotifyLabel("An observer has entered the room!"));
    }

    void ToggleOffEveryButtonsExcept(String btn)
    {

        if (btn != "right45") right45.sprite = RIGHT_45_OFF;
        if (btn != "left45") left45.sprite = LEFT_45_OFF;
        if(btn != "both") both.sprite = BOTH_OFF;
        //undress.sprite = UNDRESS_OFF;
        if (btn != "tripod") tripod.sprite = TRIPOD_OFF;
        if (btn != "tender") tender.sprite = TENDER_OFF;

        /*
        if (btn == "right")
        {

            right.sprite = (right.sprite == RIGHT_ON) ? RIGHT_OFF : RIGHT_ON;
        }
        if (btn == "left")
        {
            left.sprite = (left.sprite == LEFT_ON) ? LEFT_OFF : LEFT_ON;
        }*/
        if (btn == "both")
        {
            both.sprite = (both.sprite == BOTH_ON) ? BOTH_OFF : BOTH_ON;
        }
        if (btn == "right45")
        {
            right45.sprite = (right45.sprite == RIGHT_45_ON) ? RIGHT_45_OFF : RIGHT_45_ON;
        }
        if (btn == "left45")
        {
            left45.sprite = (left45.sprite == LEFT_45_ON) ? LEFT_45_OFF : LEFT_45_ON;
        }
        if (btn == "undress")
        {
            undress.sprite = UNDRESS_ON;
        }
        if (btn == "tripod")
        {
            tripod.sprite = (tripod.sprite == TRIPOD_ON) ? TRIPOD_OFF : TRIPOD_ON;
        }
        if (btn == "tender")
        {
            tender.sprite = (tender.sprite == TENDER_ON) ? TENDER_OFF : TENDER_ON;
        }
    }
}
