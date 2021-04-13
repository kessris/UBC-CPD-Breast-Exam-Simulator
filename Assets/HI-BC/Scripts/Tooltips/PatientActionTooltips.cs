using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientActionTooltips : MonoBehaviour
{
    GameObject sit;
    GameObject stand;
    GameObject lie;
    GameObject tripod;
    GameObject point;
    GameObject bothArms;
    GameObject left;
    GameObject right;
    GameObject undress;

    // Start is called before the first frame update
    void Start()
    {
        sit = GameObject.Find("SitTooltip");
        lie = GameObject.Find("LieTooltip");
        stand = GameObject.Find("StandTooltip");
        tripod = GameObject.Find("TripodTooltip");
        point = GameObject.Find("PointTooltip");
        bothArms = GameObject.Find("BothArmsTooltip");
        left = GameObject.Find("LeftTooltip");
        right = GameObject.Find("RightTooltip");
        undress = GameObject.Find("UndressTooltip");
    }

    public void ShowSitTooltip()
    {
        sit.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideSitTooltip()
    {
        sit.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowStandTooltip()
    {
        stand.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideStandTooltip()
    {
        stand.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowLieTooltip()
    {
        lie.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideLieTooltip()
    {
        lie.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowTripodTooltip()
    {
        tripod.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideTripodTooltip()
    {
        tripod.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowPointTooltip()
    {
        point.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HidePointTooltip()
    {
        point.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowBothArmsTooltip()
    {
        bothArms.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideBothArmsTooltip()
    {
        bothArms.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowLeftTooltip()
    {
        left.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideLeftTooltip()
    {
        left.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowRightTooltip()
    {
        right.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideRightTooltip()
    {
        right.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowUndressTooltip()
    {
        undress.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideUndressTooltip()
    {
        undress.transform.localScale = new Vector3(0, 0, 0);
    }

}
