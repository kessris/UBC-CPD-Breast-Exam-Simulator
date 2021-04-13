using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorActionTooltips : MonoBehaviour
{
    GameObject door;
    GameObject arms;
    GameObject wash;
    GameObject examine;
    GameObject step_inside_outside;

    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.Find("DoorTooltip");
        arms = GameObject.Find("ArmsTooltip");
        wash = GameObject.Find("WashHandsTooltip");
        examine = GameObject.Find("ExamineTooltip");
        step_inside_outside = GameObject.Find("StepInsideOutsideTooltip");
    }

    public void ShowStepTooltip()
    {
        step_inside_outside.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideStepToolTip()
    {
        step_inside_outside.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowDoorTooltip()
    {
        door.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideDoorTooltip()
    {
        door.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowArmsTooltip()
    {
        arms.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideArmsTooltip()
    {
        arms.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowWashTooltip()
    {
        wash.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideWashTooltip()
    {
        wash.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowExamineTooltip()
    {
        examine.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideExamineTooltip()
    {
        examine.transform.localScale = new Vector3(0, 0, 0);
    }

}
