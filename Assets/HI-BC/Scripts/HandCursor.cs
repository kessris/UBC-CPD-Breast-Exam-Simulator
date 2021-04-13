using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCursor : MonoBehaviour
{

    public Sprite examineSprite, stopExamineSprite;

    public Image examineBtnImage;

    public PictureStateSwitcher pictureStateNetwork;

    Canvas myCanvas;

    bool isExamining;
    bool toggle;

    private void Start()
    {
        myCanvas = GetComponentInParent<Canvas>();
    }

    Vector2 pos;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            isExamining = !isExamining;

            if (!isExamining)
            {
                //examineBtnImage.sprite = stopExamineSprite;
                //pictureStateNetwork.ModifyDoctorHand(true, pos);
            }

        }

        if (!isExamining) return;

        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);

    }

    public void Examine()
    {
        if (!toggle)
        {
            toggle = true;
            isExamining = true;
            GetComponent<Image>().enabled = true;

        }
        else
        {
            isExamining = false;
            toggle = false;
            GetComponent<Image>().enabled = false;
            examineBtnImage.sprite = examineSprite;

            //pictureStateNetwork.ModifyDoctorHand(false, Vector2.zero);
        }
        
    }


}
