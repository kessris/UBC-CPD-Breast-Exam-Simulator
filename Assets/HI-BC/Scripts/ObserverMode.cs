using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverMode : MonoBehaviour
{
    public GameObject[] uiToHide;

    // Start is called before the first frame update
    void Start()
    {
        if (CrossSceneInformation.mode == "observer")
        {
            foreach (GameObject obj in uiToHide)
            {
                obj.transform.localScale = new Vector3(0, 0, 0);
            }

            GameObject.Find("ControlPanel").transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
