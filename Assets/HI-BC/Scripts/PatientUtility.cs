using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientUtility : MonoBehaviour
{

    public Canvas rightBreastUI;
    /*
   private void Start()
   {

       var localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer");
       if(localPlayer != null)
       {
           var playerType = localPlayer.GetComponent<PlayerNetwork>().playerType;
           if(playerType == PlayerType.CTA_PATIENT)
           {
               leftBreastUI.SetActive(false);
               rightBreastUI.SetActive(false);
               enabled = false;
           }
       }

   }


   private void Update()
   {
       if (leftBreastUI != null && rightBreastUI != null)
       {

           if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
           {
               leftBreastUI.SetActive(true);
               rightBreastUI.SetActive(true);
           }
           else
           {
               leftBreastUI.SetActive(false);
               rightBreastUI.SetActive(false);
           }
       }
   }*/

    
    public void ToggleUI(bool isVisible)
    {
        rightBreastUI.enabled = isVisible;
        
    }

    public void Examine()
    {
        rightBreastUI.GetComponent<ExamineUtility>().ExamineBreast();
    }



}
