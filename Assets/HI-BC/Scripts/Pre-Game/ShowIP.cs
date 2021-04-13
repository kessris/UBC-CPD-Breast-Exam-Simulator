using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIP : MonoBehaviour
{
    bool isIpActive = false;
    public GameObject IPfield;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            if (isIpActive)
            {
                IPfield.transform.localScale = new Vector3(0, 0, 0);
                isIpActive = false;
            }
            else if (!isIpActive)
            {
                    IPfield.transform.localScale = new Vector3(1, 1, 1);
                    isIpActive = true;
            }
        }
    }
}

