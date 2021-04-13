using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowsTooltip : MonoBehaviour
{
    GameObject Minimize;
    GameObject Exit;

    public GameObject MinBtn, ExitBtn;
    public Sprite min_ON, min_OFF, exit_ON, exit_OFF;

    // Start is called before the first frame update
    void Start()
    {
        Minimize = GameObject.Find("MinimizeTooltip");
        Exit = GameObject.Find("ExitTooltip");
    }

    public void ShowMinimizeTooltip()
    {
       Minimize.transform.localScale = new Vector3(1, 1, 1);
       //MinBtn.GetComponent<Image>().sprite = min_ON;
    }

    public void HideMinimizeToolTip()
    {
       Minimize.transform.localScale = new Vector3(0, 0, 0);
       //MinBtn.GetComponent<Image>().sprite = min_OFF;
    }

    public void ShowExitTooltip()
    {
        Exit.transform.localScale = new Vector3(1, 1, 1);
        //ExitBtn.GetComponent<Image>().sprite = exit_ON;
    }

    public void HideExitTooltip()
    {
        Exit.transform.localScale = new Vector3(0, 0, 0);
        //ExitBtn.GetComponent<Image>().sprite = exit_OFF;
    }
}
