using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DoctorButtonSetup : MonoBehaviour
{

    Transform sittingLocation;

    Animator anim;

    [SerializeField] Sprite EXAMINE_ON, EXAMINE_OFF, RAISE_BOTH_ON, RAISE_BOTH_OFF, STEP_ON, STEP_OFF;
    public bool isExamining = false;
    bool isArmRaised = false;
    bool isInside;

    DoctorNetworkUtility docNetworkUtility;
    PlayerNetwork playerNetworkUtility;

    GameObject bothArmsBtn;
    GameObject notificationlabel;
    Transform patientDetector;

    GameObject examineGuide, teleportGuide, sinkLocation;

    Button knockBtn;

    private void Start()
    {
        anim = GetComponent<Animator>();

        Button examineBtn = GameObject.Find("Examine").GetComponent<Button>();
        examineBtn.onClick.AddListener(Examine);

        knockBtn = GameObject.Find("Knock").GetComponent<Button>();
        knockBtn.onClick.AddListener(Knock);

        Button washHandsBtn = GameObject.Find("WashHand").GetComponent<Button>();
        washHandsBtn.onClick.AddListener(WashHands);

        Button stepInOut = GameObject.Find("StepInsideOutside").GetComponent<Button>();
        stepInOut.onClick.AddListener(StepInOut);


        docNetworkUtility = GetComponent<DoctorNetworkUtility>();
        playerNetworkUtility = GetComponent<PlayerNetwork>();

        bothArmsBtn = GameObject.Find("RaiseBothArms");
        GameObject.Find("RaiseBothArms").GetComponent<Button>().onClick.AddListener(RaiseBoth);

        notificationlabel = GameObject.Find("NotificationLabel");
        notificationlabel.SetActive(false);

        patientDetector = GameObject.Find("PatientDetector").transform;

        examineGuide = GameObject.Find("examineGuide");
        teleportGuide = GameObject.Find("teleportGuide");
        sinkLocation = GameObject.Find("SinkWater");
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, patientDetector.position) > 4.1f)
        {
            knockBtn.interactable = true;
            isInside = false;
        }
        else
        {
            knockBtn.interactable = false;
            isInside = true;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            TeleportToTable();
        }

        if (isExamining)
            examineGuide.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        else
            examineGuide.transform.localScale = new Vector3(0, 0, 0);

        if (Vector3.Distance(transform.position, sinkLocation.transform.position) < 2.5f)
            teleportGuide.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        else
            teleportGuide.transform.localScale = new Vector3(0, 0, 0);
    }

    private void TeleportToTable()
    {
            transform.position = GameObject.Find("SittingExamPos").transform.position;
            transform.rotation = GameObject.Find("SittingExamPos").transform.rotation;
            FollowCam.Instance.RotateTowards(360, 3);
    }

    private void StepInOut()
    {
        if (isExamining)
        {
            Examine();
        }

        if (isInside)
        {
            transform.position = GameObject.Find("KnockDoorPos").transform.position;
            transform.rotation = GameObject.Find("KnockDoorPos").transform.rotation;
            FollowCam.Instance.RotateTowards(180, 3);
        }
        else
        {
            transform.position = GameObject.Find("SittingExamPos").transform.position;
            transform.rotation = GameObject.Find("SittingExamPos").transform.rotation;
            FollowCam.Instance.RotateTowards(360, 3);

        }

    }

    private void RaiseBoth()
    {
        if (isExamining)
        {
            Examine();
        }

        if (isArmRaised)
        {
            isArmRaised = false;
            bothArmsBtn.GetComponent<Image>().sprite = RAISE_BOTH_OFF;
            playerNetworkUtility.SynchAnimation(Actions.CTA_Patient_Sitting_RaiseBoth);

        } else
        {
            isArmRaised = true;
            bothArmsBtn.GetComponent<Image>().sprite= RAISE_BOTH_ON;
            playerNetworkUtility.SynchAnimation(Actions.CTA_Patient_Sitting_RaiseBoth);

        }
        
    }

    public void SwitchExaminePosition(int selection)
    {
        if (!isExamining) return;

        if(selection == 1)
        {
            FollowCam.Instance.RotateTowards(-367.964f, 1); 
            transform.position = GameObject.Find("SittingExamPos").transform.position;
            transform.rotation = GameObject.Find("SittingExamPos").transform.rotation;
        }
        else if(selection == 2)
        {
            FollowCam.Instance.RotateTowards(-383f, 2);
            transform.position = GameObject.Find("LyingExamPos").transform.position;
            transform.rotation = GameObject.Find("LyingExamPos").transform.rotation;
        }
    }

    private void Examine()
    {
        if (isArmRaised)
        {
            isArmRaised = false;
            bothArmsBtn.GetComponent<Image>().sprite = RAISE_BOTH_OFF;
            playerNetworkUtility.SynchAnimation(Actions.CTA_Patient_Sitting_RaiseBoth);
        }

        if (isExamining)
        {
            isExamining = false;
            GameObject.Find("Examine").GetComponent<Image>().sprite = EXAMINE_OFF;
            docNetworkUtility.SynchDoctorAction(4);
            TogglePlayerMovement(true); //can move again
            GetComponent<PlayerMoveController>().isExamining = false;

            FollowCam.Instance.TogglePerspective(false);
        }
        else
        {
            isExamining = true;
            GameObject.Find("Examine").GetComponent<Image>().sprite = EXAMINE_ON;
            docNetworkUtility.SynchDoctorAction(3);
            TogglePlayerMovement(false);
            GetComponent<PlayerMoveController>().isExamining = true;

            FollowCam.Instance.TogglePerspective(true);

            DoctorToolbar docTool = GameObject.Find("Toolbars").GetComponent<DoctorToolbar>();
            if (docTool != null) docTool.CloseControlGuide();


            var patient = GameObject.Find("PatientDetector").GetComponent<PatientDetector>().patient;//GameObject.Find("Female Patient March 23(Clone)");


            if (patient != null)
            {

                Animator patientAnim = patient.GetComponent<Animator>();

                if (patientAnim.GetCurrentAnimatorStateInfo(0).IsName("Lying Down")
                    || patientAnim.GetCurrentAnimatorStateInfo(0).IsName("Raise Left Arm Lying")
                    || patientAnim.GetCurrentAnimatorStateInfo(0).IsName("Raise Right Arm Lying")
                    || patientAnim.GetCurrentAnimatorStateInfo(0).IsName("Raise Both Arms Lying"))
                {
                    transform.position = GameObject.Find("LyingExamPos").transform.position;
                    transform.rotation = GameObject.Find("LyingExamPos").transform.rotation;
                }
                else
                {
                    transform.position = GameObject.Find("SittingExamPos").transform.position;
                    transform.rotation = GameObject.Find("SittingExamPos").transform.rotation;
                }
            }
            else
            {
                transform.position = GameObject.Find("SittingExamPos").transform.position;
                transform.rotation = GameObject.Find("SittingExamPos").transform.rotation;
            }

        }
            
    }

    //Diegetic ui - deprecated function
    private void TogglePatientExamineWorldspaceUI(bool isVisible)
    {
        var patientUtilitiies = GameObject.FindGameObjectsWithTag("Player");
        foreach(var patient in patientUtilitiies)
        {
            var utility = patient.GetComponent<PatientUtility>();
            if (utility != null)
            {
                utility.ToggleUI(isVisible);
            }
        }

    }


    public void NotifyObserverMode()
    {
        StartCoroutine(NotifyLabel("An observer has entered the room!"));
    }
    private IEnumerator NotifyLabel(string msg)
    {
        notificationlabel.SetActive(true);
        notificationlabel.transform.GetChild(0).GetComponent<Text>().text = msg;
        yield return new WaitForSeconds(3.5f);
        notificationlabel.SetActive(false);
    }

    public void Knock()
    {

        //if (isExamining) return;

        if (isExamining)
        {
            Examine();
        }

        if (isArmRaised)
        {
            isArmRaised = false;
            bothArmsBtn.GetComponent<Image>().sprite = RAISE_BOTH_OFF;
            playerNetworkUtility.SynchAnimation(Actions.CTA_Patient_Sitting_RaiseBoth);
        }

        /*
        if (Vector3.Distance(transform.position, GameObject.Find("KnockDoorPos").transform.position) > 1.5)
        {
            StartCoroutine(NotifyLabel("Please move closer to the door"));
            return;
        }*/

        transform.position = GameObject.Find("KnockDoorPos").transform.position;
        transform.rotation = GameObject.Find("KnockDoorPos").transform.rotation;
        FollowCam.Instance.RotateTowards(180, 3);

        docNetworkUtility.SynchDoctorAction(1);
    }

    public void WashHands()
    {
        if (isExamining)
        {
            Examine();
        }

        if (isArmRaised)
        {
            isArmRaised = false;
            bothArmsBtn.GetComponent<Image>().sprite = RAISE_BOTH_OFF;
            playerNetworkUtility.SynchAnimation(Actions.CTA_Patient_Sitting_RaiseBoth);
        }

        transform.position = GameObject.Find("WashHandPos").transform.position;
        transform.rotation = GameObject.Find("WashHandPos").transform.rotation;
        FollowCam.Instance.RotateTowards(-364, 3);

        docNetworkUtility.SynchDoctorAction(2);
    }

    void TogglePlayerMovement(bool state)
    {

        GetComponent<PlayerMoveController>().enabled = state;
        GetComponent<Collider>().enabled = state;
        GetComponent<Rigidbody>().useGravity = state;
    }
}
