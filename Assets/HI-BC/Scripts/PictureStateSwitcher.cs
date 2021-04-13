using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine.UI;
using System.Collections;

public enum Scene2D
{
    Dressed,
    Sitting,
    Standing,
    LyingDown,
    SittingRaiseRight,
    SittingRaiseLeft,
    SittingRaiseBoth,
    SittingTouchTender,
    StandingRaiseRight,
    StandingRaiseLeft,
    StandingRaiseBoth,
    StandingTouchTender,
    LyingDownRight,
    LyingDownLeft,
    LyingDownBoth,
    TripodPosition,
    RaiseRight45,
    RaiseLeft45
}

public class PictureStateSwitcher : SimplePictureBehavior
{

    public Image _backgroundImage;

    public Sprite startingScene, sittingNeutral, sittingRaiseRight, sittingRaiseLeft, sittingRaiseBoth, sittingTouchTender;

    public Sprite standing, standingRaiseRight, standingRaiseLeft, standingRaiseBoth, standingTouchTender;

    public Sprite lyingDown, lyingDownRaiseRight, lyingDownRaiseLeft, lyingDownRaiseBoth;

    public Sprite tripodSprite, raiseRight45Deg, raiseLeft45Deg;


    public Image leftImg, rightImg, bothImg, undressImg, tenderImg; //image reference to the button icons
    public Image tripodImg, raiseLeft45Img, raiseRight45Img;

    //these are buttion image -> sprites
    public Sprite raiseLeftBtn, raiseLeftBtn2, raiseRightBtn, raiseRightBtn2, raiseBothBtn, raiseBothBtn2, undressBtn, undressBtn2, tenderImgBtn, tenderImgBtn2;
    public Sprite tripodBtn, tripodBtn2, raiseRight45Btn, raiseRight45Btn2, raiseLeft45Btn, raiseLeft45Btn2;
    
    //track if button was pressed/unpressed
    bool isLeftActive, isRightActive, isBothActive, isTenderActive, isUndressActive, isTripodActive, isRaiseRight45Active, isRaiseLeft45Active;


    public Canvas myCanvas;
    public Transform handCursorTransform;
    public Image handCursorImg;


    PlayerState patientState;


    private void Start()
    {
        //satisfactionBar = GameObject.Find("SatisfactionBar").GetComponent<Image>();
        patientState.Sit();
    }



    /*
    public void ChangePic(int choice) //should be renamed to ChangePosition(int)
    {


        tenderImg.gameObject.GetComponent<Button>().interactable = true;
        tripodImg.gameObject.GetComponent<Button>().interactable = true;
        raiseRight45Img.gameObject.GetComponent<Button>().interactable = true;
        raiseLeft45Img.gameObject.GetComponent<Button>().interactable = true;
        undressImg.gameObject.GetComponent<Button>().interactable = true;
        bothImg.gameObject.GetComponent<Button>().interactable = true;


        ResetAllButtonsExcept("");

        switch ((Scene2D)choice)
        {

            case Scene2D.Sitting:

                patientState.Sit();

                if (isUndressActive)
                {
                    _backgroundImage.sprite = sittingNeutral;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Sitting);
                }
                else
                {
                    _backgroundImage.sprite = startingScene;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Dressed);
                }
                
                break;

            case Scene2D.Standing:

                _backgroundImage.sprite = standing;
                patientState.Stand();
                networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Standing);
                break;

            case Scene2D.LyingDown:

                _backgroundImage.sprite = lyingDown;
                patientState.Lie();
                networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.LyingDown);


                tenderImg.gameObject.GetComponent<Button>().interactable = false;
                tripodImg.gameObject.GetComponent<Button>().interactable = false;
                raiseRight45Img.gameObject.GetComponent<Button>().interactable = false;
                raiseLeft45Img.gameObject.GetComponent<Button>().interactable = false;
                undressImg.gameObject.GetComponent<Button>().interactable = false;
                bothImg.gameObject.GetComponent<Button>().interactable = false;

                break;
        }

        //networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, choice);
    }



    public void ModifyArmPosition(string arm)
    {

        ResetAllButtonsExcept(arm);

        if (arm == "right")
        {
            if (isRightActive)
            {
                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = sittingNeutral;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Sitting);
                }
                else if (patientState.isStanding)
                {
                    _backgroundImage.sprite = standing;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Standing);
                }
                else if (patientState.isLying)
                {
                    _backgroundImage.sprite = lyingDown;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.LyingDown);
                }
            }
            else
            {

                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = sittingRaiseRight;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.SittingRaiseRight);
                }
                else if (patientState.isStanding)
                {
                    _backgroundImage.sprite = standingRaiseRight;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.StandingRaiseRight);
                }
                else if (patientState.isLying)
                {
                    _backgroundImage.sprite = lyingDownRaiseRight;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.LyingDownRight);
                }

            }


            isRightActive = !isRightActive;
            rightImg.sprite = (isRightActive) ? raiseRightBtn2: raiseRightBtn;


        }else if(arm == "left")
        {

            if (isLeftActive)
            {
                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = sittingNeutral;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Sitting);
                }
                else if (patientState.isStanding)
                {
                    _backgroundImage.sprite = standing;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Standing);
                }
                else if (patientState.isLying)
                {
                    _backgroundImage.sprite = lyingDown;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.LyingDown);
                }
            }
            else
            {

                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = sittingRaiseLeft;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.SittingRaiseLeft);
                }
                else if (patientState.isStanding)
                {
                    _backgroundImage.sprite = standingRaiseLeft;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.StandingRaiseLeft);
                }
                else if (patientState.isLying)
                {
                    _backgroundImage.sprite = lyingDownRaiseLeft;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.LyingDownLeft);
                }

            }


            isLeftActive = !isLeftActive;
            leftImg.sprite = (isLeftActive) ? raiseLeftBtn2 : raiseLeftBtn;


        }
        else if (arm == "both")
        {

            if (isBothActive)
            {
                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = sittingNeutral;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Sitting);
                }
                else if (patientState.isStanding)
                {
                    _backgroundImage.sprite = standing;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Standing);
                }
                else if (patientState.isLying)
                {
                    _backgroundImage.sprite = lyingDown;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.LyingDown);
                }
            }
            else
            {

                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = sittingRaiseBoth;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.SittingRaiseBoth);
                }
                else if (patientState.isStanding)
                {
                    _backgroundImage.sprite = standingRaiseBoth;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.StandingRaiseBoth);
                }
                else if (patientState.isLying)
                {
                    _backgroundImage.sprite = lyingDownRaiseBoth;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.LyingDownBoth);
                }

            }


            isBothActive = !isBothActive;
            bothImg.sprite = (isBothActive) ? raiseBothBtn2 : raiseBothBtn;


        }
        else if(arm == "tender")
        {
            if (isTenderActive)
            {
                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = sittingNeutral;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Sitting);
                }
                else if (patientState.isStanding)
                {
                    _backgroundImage.sprite = standing;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Standing);
                }
                else if (patientState.isLying)
                {
                    _backgroundImage.sprite = lyingDown;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.LyingDown);
                }
            }
            else
            {

                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = sittingTouchTender;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.SittingTouchTender);
                }
                else if (patientState.isStanding)
                {
                    _backgroundImage.sprite = standingTouchTender;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.StandingTouchTender);
                }


            }

            isTenderActive = !isTenderActive;
            tenderImg.sprite = (isTenderActive) ? tenderImgBtn2 : tenderImgBtn;
        }
        else if (arm == "undress")
        {

            if (isUndressActive)
            {
                _backgroundImage.sprite = startingScene;
                networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Dressed);
            }
            else
            {
                _backgroundImage.sprite = sittingNeutral;
                networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Sitting);
                patientState.Sit();
            }

            ChangePic((int)Scene2D.Sitting);    

            isUndressActive = !isUndressActive;
            undressImg.sprite = (isUndressActive) ? undressBtn2 : undressBtn;
        }else if(arm == "tripod")
        {

            if (isTripodActive)
            {
                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = sittingNeutral;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Sitting);
                }
                else if (patientState.isStanding)
                {
                    _backgroundImage.sprite = standing;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Standing);
                }
                else if (patientState.isLying)
                {
                    _backgroundImage.sprite = lyingDown;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.LyingDown);
                }


            }
            else
            {

                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = tripodSprite;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.TripodPosition);
                }
                

            }

            isTripodActive = !isTripodActive;
            tripodImg.sprite = (isTripodActive) ? tripodBtn2 : tripodBtn;

        }
        else if(arm == "left45")
        {

            if (isRaiseLeft45Active)
            {
                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = sittingNeutral;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Sitting);
                }
                else if (patientState.isStanding)
                {
                    _backgroundImage.sprite = standing;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Standing);
                }
                else if (patientState.isLying)
                {
                    _backgroundImage.sprite = lyingDown;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.LyingDown);
                }


            }
            else
            {

                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = raiseLeft45Deg; 
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.RaiseLeft45);
                }


            }

            isRaiseLeft45Active = !isRaiseLeft45Active;
            raiseLeft45Img.sprite = (isRaiseLeft45Active) ? raiseLeft45Btn2 : raiseLeft45Btn;

        }
        else if(arm == "right45")
        {
            if (isRaiseRight45Active)
            {
                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = sittingNeutral;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Sitting);
                }
                else if (patientState.isStanding)
                {
                    _backgroundImage.sprite = standing;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.Standing);
                }
                else if (patientState.isLying)
                {
                    _backgroundImage.sprite = lyingDown;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.LyingDown);
                }


            }
            else
            {

                if (patientState.isSitting)
                {
                    _backgroundImage.sprite = raiseLeft45Deg;
                    networkObject.SendRpc(RPC_CHANGE_PICTURE, Receivers.All, (int)Scene2D.RaiseRight45);
                }


            }

            isRaiseRight45Active = !isRaiseRight45Active;
            raiseRight45Img.sprite = (isRaiseRight45Active) ? raiseRight45Btn2 : raiseRight45Btn;
        }

    }


    //modifies the 2d image of the doctors hand
    public void ModifyDoctorHand(bool isVisible, Vector2 position)
    {
        networkObject.SendRpc(RPC_MODIFY_CURSOR, Receivers.All, isVisible, position);
    }
    */

    public override void ChangePicture(RpcArgs args)
    {
        int selection = args.GetNext<int>();

		MainThreadManager.Run(() =>
		{
            switch ((Scene2D)selection)
            {
                case Scene2D.Dressed:
                    _backgroundImage.sprite = startingScene;
                    break;

                case Scene2D.Sitting:
                    _backgroundImage.sprite = sittingNeutral;
                    break;

                case Scene2D.Standing:
                    _backgroundImage.sprite = standing;
                    break;

                case Scene2D.LyingDown:
                    _backgroundImage.sprite = lyingDown;
                    break;

                case Scene2D.SittingRaiseRight:
                    _backgroundImage.sprite = sittingRaiseRight;
                    break;

                case Scene2D.SittingRaiseLeft:
                    _backgroundImage.sprite = sittingRaiseLeft;
                    break;

                case Scene2D.SittingRaiseBoth:
                    _backgroundImage.sprite = sittingRaiseBoth;
                    break;
                case Scene2D.SittingTouchTender:
                    _backgroundImage.sprite = sittingTouchTender;
                    break;

                case Scene2D.StandingRaiseRight:
                    _backgroundImage.sprite = standingRaiseRight;
                    break;


                case Scene2D.StandingRaiseLeft:
                    _backgroundImage.sprite = standingRaiseLeft;
                    break;

                case Scene2D.StandingRaiseBoth:
                    _backgroundImage.sprite = standingRaiseBoth;
                    break;

                case Scene2D.StandingTouchTender:
                    _backgroundImage.sprite = standingTouchTender;
                    break;

                case Scene2D.LyingDownRight:
                    _backgroundImage.sprite = lyingDownRaiseRight;
                    break;

                case Scene2D.LyingDownLeft:
                    _backgroundImage.sprite = lyingDownRaiseLeft;
                    break;

                case Scene2D.LyingDownBoth:
                    _backgroundImage.sprite = lyingDownRaiseBoth;
                    break;

                case Scene2D.TripodPosition:
                    _backgroundImage.sprite = tripodSprite;
                    break;

                case Scene2D.RaiseLeft45:
                    _backgroundImage.sprite = raiseLeft45Deg;
                    break;
                case Scene2D.RaiseRight45:
                    _backgroundImage.sprite = raiseRight45Deg;
                    break;

            }

        });
	}


    public override void ModifyCursor(RpcArgs args)
    {
        bool isVisible = args.GetNext<bool>();
        Vector2 pos = args.GetNext<Vector2>();

        MainThreadManager.Run(() =>
        {

            if (isVisible)
            {
                handCursorImg.enabled = true;
                handCursorTransform.position = myCanvas.transform.TransformPoint(pos);
            }
            else
            {
                handCursorImg.enabled = false;
            }

        });
    }

    public GameObject patientWorldspaceMoodBar;
    public Image moodImg;
    public Text moodBarText;
    public Sprite angry, anxious, confused, embarassed, calm;

    public override void ModifyMoodBar(RpcArgs args)
    {
        float fillValue = args.GetNext<float>();

        MainThreadManager.Run(() =>
        {
            patientWorldspaceMoodBar.SetActive(true);
            patientWorldspaceMoodBar.GetComponent<Canvas>().enabled = true;

            switch (fillValue)
            {

                case 1:
                    moodImg.sprite = angry;
                    moodBarText.text = "Angry";
                    break;
                case 2:
                    moodImg.sprite = anxious;
                    moodBarText.text = "Anxious";
                    break;
                case 3:
                    moodImg.sprite = confused;
                    moodBarText.text = "Confused";
                    break;
                case 4:
                    moodImg.sprite = embarassed;
                    moodBarText.text = "Embarassed";
                    break;
                case 5:
                    moodImg.sprite = calm;
                    moodBarText.text = "Calm";
                    break;
            }

        });
    }

    public void ModifyMoodBar(float fillAmount)
    {
        networkObject.SendRpc(RPC_MODIFY_MOOD_BAR, Receivers.All, fillAmount);
    }



    //public Image leftImg, rightImg, bothImg, undressImg, tenderImg; //image reference to the button icons
    //public Image tripodImg, raiseLeft45Img, raiseRight45Img;
    private void ResetAllButtonsExcept(string name)
    {
        leftImg.sprite = raiseLeftBtn;
        rightImg.sprite = raiseRightBtn;
        undressImg.sprite = undressBtn;
        tenderImg.sprite = tenderImgBtn;
        tripodImg.sprite = tripodBtn;
        raiseRight45Img.sprite = raiseRight45Btn;
        raiseLeft45Img.sprite = raiseLeft45Btn;
        bothImg.sprite = raiseBothBtn;  


        switch (name)
        {
            case "left":
                isRightActive = isBothActive = isTenderActive = isUndressActive = isTripodActive = isRaiseRight45Active = isRaiseLeft45Active = false;
                break;
            case "right":
                isLeftActive = isBothActive = isTenderActive = isUndressActive = isTripodActive = isRaiseRight45Active = isRaiseLeft45Active = false;
                break;

            case "undress":
                isLeftActive = isRightActive = isBothActive = isTenderActive = isTripodActive = isRaiseRight45Active = isRaiseLeft45Active = false;
                break;

            case "tripod":
                isLeftActive = isRightActive = isBothActive = isTenderActive = isUndressActive = isRaiseRight45Active = isRaiseLeft45Active = false;
                break;

            case "left45":
                isLeftActive = isRightActive = isBothActive = isTenderActive = isUndressActive = isTripodActive = isRaiseRight45Active = false;
                break;

            case "right45":
                isLeftActive = isRightActive = isBothActive = isTenderActive = isUndressActive = isTripodActive = isRaiseLeft45Active = false;
                break;

            case "both":

                isRightActive  = isTenderActive = isUndressActive = isTripodActive = isRaiseRight45Active = isRaiseLeft45Active = false;
                break;
        }
    }

}

public struct PlayerState
{
    public bool isStanding, isSitting, isLying;

    public void Stand()
    {
        SetAllFalse();
        isStanding = true;
    }

    public void Sit()
    {
        SetAllFalse();
        isSitting = true;
    }

    public void Lie()
    {
        SetAllFalse();
        isLying = true;
    }


    public void SetAllFalse()
    {
        isStanding = false;
        isSitting = false;
        isLying = false;
    }

}