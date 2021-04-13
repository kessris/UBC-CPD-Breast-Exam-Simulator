using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExamineUtility : MonoBehaviour
{

    public Transform breastLocation, breastLocationLying, neutral;
    IKControl docIkControl;

    bool isCursorInElement;
    bool isExamining;

    //public Image btnImage;
    //public Sprite onsprite, offsprite;


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            docIkControl = GameObject.FindGameObjectWithTag("LocalPlayer").transform.GetChild(0).GetComponent<IKControl>();
            ExamineBreast();
        }


        
    }


    public void ExamineBreast()
    {
        var patientDetector = GameObject.Find("PatientDetector");

        if (patientDetector != null && docIkControl != null)
        {

            if (patientDetector.GetComponent<PatientDetector>().isTutorial)
            {
                if (!isExamining)
                {
                    docIkControl.SetHandPosition(breastLocation.position);
                    isExamining = true;
                }
                else
                {
                    isExamining = false;
                    docIkControl.SetHandPosition(neutral.position);
                }

                return;
            }

            var patient = patientDetector.GetComponent<PatientDetector>().patient;

            if (patient != null)
            {

                if (!isExamining)
                {
                    var animator = patient.GetComponent<Animator>();
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Lying Down")
                        || animator.GetCurrentAnimatorStateInfo(0).IsName("Raise Left Arm Lying")
                        || animator.GetCurrentAnimatorStateInfo(0).IsName("Raise Right Arm Lying")
                        || animator.GetCurrentAnimatorStateInfo(0).IsName("Raise Both Arms Lying")) //lying down pos
                    {
                        docIkControl.SetHandPosition(breastLocationLying.position);
                    }
                    else
                    {
                        docIkControl.SetHandPosition(breastLocation.position);
                    }

                    isExamining = true;
                }
                else
                {
                    isExamining = false;
                    docIkControl.SetHandPosition(neutral.position);
                }

            }
        }
    }
    


}
