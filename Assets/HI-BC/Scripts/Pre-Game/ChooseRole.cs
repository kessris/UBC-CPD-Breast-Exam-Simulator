using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRole : MonoBehaviour
{
    public GameObject Doctor;
    public GameObject Patient;

    private void Start()
    {
        if (CrossSceneInformation.role == "doctor")
            Doctor.SetActive(true);
        else if (CrossSceneInformation.role == "patient")
            Patient.SetActive(true);
    }
}
