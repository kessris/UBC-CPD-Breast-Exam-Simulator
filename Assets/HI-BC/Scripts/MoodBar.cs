using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodBar : MonoBehaviour
{

	public Image angryImg, anxiousImg, confusedImg, embarassImg, calmImg;
	public Sprite angry, angryInactive, anxious, anxiousInactive, confuse, confuseInactive, embarass, embarassInactive, calm, calmInactive;
	public PlayerNetwork pNetwork;
	bool isAngry, isAnxious, isConfuse, isEmbarss, isCalm;
	float moodValue = 1.0f;



	public void Start()
	{
		//pictureNetwork = GameObject.Find("MoodBarController").GetComponent<PictureStateSwitcher>();
		pNetwork = transform.root.GetComponent<PlayerNetwork>();
	}




	public void SetMood(int mood)
    {

		angryImg.sprite = angryInactive;
		anxiousImg.sprite = anxiousInactive;
		confusedImg.sprite = confuseInactive;
		embarassImg.sprite = embarassInactive;
		calmImg.sprite = calmInactive;


		switch (mood)
        {
			case 1: //angry

				angryImg.sprite = angry;
				pNetwork.ModifyMoodBar(1);

				break;
			case 2: //anxious
				anxiousImg.sprite = anxious;
				pNetwork.ModifyMoodBar(2);
				break;

			case 3: //confused
				confusedImg.sprite = confuse;
				pNetwork.ModifyMoodBar(3);
				break;

			case 4: //embarass
				embarassImg.sprite = embarass;
				pNetwork.ModifyMoodBar(4);
				break;

			case 5: //calm
				calmImg.sprite = calm;
				pNetwork.ModifyMoodBar(5);
				break;

		}
	}

	/*
	public void IncreaseMoodBarValue()
    {
		moodValue += 0.2f;
		if (moodValue > 1f)
		{
			moodValue = 1f;
		}
		moodBar.fillAmount = moodValue;


		pictureNetwork.ModifyMoodBar(moodValue);
	}

	public void DecreaseMoodBarValue()
    {
		moodValue -= .2f;
		if (moodValue < 0f) moodValue = 0f;
		moodBar.fillAmount = moodValue;


		pictureNetwork.ModifyMoodBar(moodValue);
	}


	
	private void SetMoodBarColor()
    {
        if (moodValue >= 1.0f)
        {
			moodBar.color = hundred;
        }
		else if (moodValue >= 0.8f)
		{
			moodBar.color = eighty;
		}
		else if(moodValue >= 0.6f)
        {
			moodBar.color = sixty;
		}
		else if(moodValue >= 0.4f)
        {
			moodBar.color = fourty;
		}
		else if(moodValue >= 0.2f)
        {
			moodBar.color = twenty;
		}
        else
        {
			moodBar.color = Color.white;
        }

		moodValueText.text = $"{(int)(moodValue * 100)}%";
	}*/

}
