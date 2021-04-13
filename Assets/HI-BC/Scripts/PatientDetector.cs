using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientDetector : MonoBehaviour
{

    public GameObject patient;
    public bool isTutorial;
    Vector3 lyingPos;

    private void Start()
    {
        lyingPos = new Vector3(-2.497f, -2f, 0.319f);

        if (isTutorial)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5)) print("patient state is: " + GetPatientState());
    }

    public int GetPatientState() //0 = stand, 1 = sit, 2 = lying
    {
        if (patient == null) return 0;

        if(Vector3.Distance(patient.transform.position, lyingPos) <= 0.2f) return 2;
        
        return 1;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerNetwork>();
        if (player != null)
        {
            if(player.playerType == PlayerType.CTA_PATIENT)
            {
                patient = player.gameObject;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        patient = null;
    }

}
