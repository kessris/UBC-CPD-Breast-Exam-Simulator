using UnityEngine;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
//https://github.com/BeardedManStudios/ForgeNetworkingRemastered/wiki/RpcArgs-and-RpcInfo-Structs

public enum Actions
{
	IMG_Doctor_RaiseBoth,
	CTA_Patient_Sit,
	CTA_Patient_Stand,
	CTA_Patient_Sitting_RaiseRight,
	CTA_Patient_Sitting_RaiseLeft,
	CTA_Patient_Sitting_RaiseBoth,
	CTA_Patient_Sitting_Tripod,
	CTA_Patient_Sitting_TouchTender,
	CTA_Patient_Lying,
	CTA_Patient_Lying_RaiseRight,
	CTA_Patient_Lying_RaiseLeft,
	CTA_Patient_Lying_RaiseBoth,
	CTA_PATIENT_GOWN_OPEN,
	CTA_PATIENT_GOWN_RIGHT,
	CTA_PATIENT_GOWN_LEFT,
	CTA_PATIENT_GOWN_NORMAL,
	CTA_PATIENT_GOWN_OFF
}

public enum PlayerType
{
	IMG_DOCTOR,
	CTA_PATIENT,
	OBSERVER
}

public class PlayerNetwork : PlayerBehavior
{

	public Behaviour[] disabledBehaviours; //disabled for remote clients (such as movement scripts)

	public PlayerType playerType;

	//Patient mood bar built in to this class
	public GameObject patientWorldspaceMoodBar;
	public Image moodImg;
	public Text moodBarText;
	public Sprite angry, anxious, confused, embarassed, calm;

	private Animator playerAnimationController;

	[DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    float lyingMoodbarPosition;
    float sittingMoodbarPosition;
    float standingMoodbarPosition;
	float moodBarOriginalScale;
    public RectTransform moodbar;

	[SerializeField]
	GameObject gownNormal, gownLeft, gownRight, gownOpen, casualClothes;
	int _offendedCounter, _anxiousCounter, _confusedCounter, _embarassedCounter, _calmCounter;

	float moodBarCurrentScale = 1;
	float counterOriginalY;

	GameObject confirmQuitBtn, confirmHomeBtn;

	private void Start()
    {

		if (networkObject.IsOwner)   //setup localplayer
        {
			FollowCam.Instance.CameraFollowObj = transform.Find("playerFollowObj").gameObject;
			gameObject.tag = "LocalPlayer";

			if (playerType == PlayerType.CTA_PATIENT)
			{
				GameObject.Find("MoodBar").GetComponent<MoodBar>().pNetwork = this;

			}

		}
        else
        {	//if not local player, disable movement scripts 
			foreach(var b in disabledBehaviours)
            {
				b.enabled = false;
            }


			if (playerType == PlayerType.OBSERVER)
			{
				GetComponent<Collider>().enabled = false;

				var pNetwork = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerNetwork>();

				if (pNetwork != null)
				{
					if (pNetwork.playerType == PlayerType.CTA_PATIENT)
					{
						pNetwork.gameObject.GetComponent<PatientButtonSetup>().NotifyObserverMode();
					}
					else if (pNetwork.playerType == PlayerType.IMG_DOCTOR)
					{
						pNetwork.gameObject.GetComponent<DoctorButtonSetup>().NotifyObserverMode();
					}
				}

			}

		}

		//Called for all players
		GameObject.Find("MinimizeBtn").GetComponent<Button>().onClick.AddListener(MinimizeWindow);
		GameObject.Find("ExitBtn").GetComponent<Button>().onClick.AddListener(ExitWindowWarning);
		GameObject.Find("HomeBtn").GetComponent<Button>().onClick.AddListener(GoToHomeWarning);

		playerAnimationController = GetComponent<Animator>();
		if (playerAnimationController == null) playerAnimationController = transform.Find("model").GetComponent<Animator>();

		if (moodbar != null)
		{
			standingMoodbarPosition = moodbar.localPosition.y;
			sittingMoodbarPosition = moodbar.localPosition.y - 40;
			lyingMoodbarPosition = moodbar.localPosition.y - 100;
			moodBarOriginalScale = moodbar.localScale.x;

			var pNetwork = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerNetwork>();
			if (pNetwork != null)
			{
				if (pNetwork.playerType != PlayerType.OBSERVER)
				{
					counterOriginalY = GameObject.Find("offendedCounter").GetComponent<RectTransform>().localPosition.y;
				}
			}
		}

		if (networkObject.IsOwner)
        {
			confirmQuitBtn = GameObject.Find("ConfirmQuit");
			confirmHomeBtn = GameObject.Find("ConfirmHome");
			//confirmQuitBtn.SetActive(false);
			//confirmHomeBtn.SetActive(false);
        }

		StartCoroutine(CheckPatientAnimationStatus());
		

		//print("This object " + gameObject.name + ", network id is: " + networkObject.NetworkId);
	}


    private void MinimizeWindow()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

    public void ExitWindow()
    {
        confirmQuitBtn.SetActive(false);
        GameObject.Find("ClosingMsg").transform.localScale = new Vector3(1, 1, 1);
        networkObject.Destroy();
        Application.Quit();
    }

	public void GoToHome()
    {
        networkObject.Destroy();
        GameObject.Find("NetworkManager(Clone)").GetComponent<NetworkManager>().Disconnect();

        var go = new GameObject("Sacrificial Lamb");
        DontDestroyOnLoad(go);

        foreach (var root in go.scene.GetRootGameObjects())
            Destroy(root);

        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

	public void ExitWindowWarning()
    {
        if (confirmQuitBtn.transform.localScale == new Vector3(1, 1, 1))
            confirmQuitBtn.transform.localScale = new Vector3(0, 0, 0);
        else
            confirmQuitBtn.transform.localScale = new Vector3(1, 1, 1);
		//confirmQuitBtn.SetActive(!confirmQuitBtn.activeSelf);	
    }

	public void GoToHomeWarning()
	{
		confirmHomeBtn.SetActive(!confirmHomeBtn.activeSelf);
	}


    private void OnApplicationQuit()
    {
		networkObject.Destroy();
    }

	IEnumerator CheckPatientAnimationStatus() //only works if a patient is in PatientDetector collider.
	{

		yield return new WaitForSeconds(2f);

		var patientDetector = GameObject.Find("PatientDetector");

		if (patientDetector != null)
		{
			PatientDetector detector = patientDetector.GetComponent<PatientDetector>();
            if (detector != null)
            {
				var patient = detector.patient;

				int selection = detector.GetPatientState();
				if(selection == 1)
                {
					patient.GetComponent<Animator>().SetTrigger("sitTrigger");
				}
				else if(selection == 2)
                {
					patient.GetComponent<Animator>().SetTrigger("lieTrigger");
				}

				
            }
			
		}
	}


	float maxIdleTime = 720;
	float timer = 0;
	Vector3 prevPos, nextPos;

    private void Update()
	{
		// If this is not owned by the current network client then it needs to
		// assign it to the position and rotation specified
		if (!networkObject.IsOwner)
		{
			prevPos = transform.position;

			// Assign the position of this cube to the position sent on the network
			transform.position = networkObject.position;

			// Assign the rotation of this cube to the rotation sent on the network
			transform.rotation = networkObject.rotation;

			playerAnimationController.SetInteger("speed", networkObject.speed);

			nextPos = networkObject.position;

			if(prevPos == nextPos)
            {
				timer += Time.deltaTime;
				if(timer >= maxIdleTime)
                {
					networkObject.Destroy();
					Application.Quit();
				}
			}
            else
            {
				timer = 0;
            }

			// Stop the function here and don't run any more code in this function
			return;
		}


		// Since we are the owner, tell the network the updated position
		networkObject.position = transform.position;

		// Since we are the owner, tell the network the updated rotation
		networkObject.rotation = transform.rotation;

		networkObject.speed = playerAnimationController.GetInteger("speed");

        // Note: Forge Networking takes care of only sending the delta, so there
        // is no need for you to do that manually

		if(Input.GetKeyDown(KeyCode.F12))
        {
			//Debugging print
			Debug.LogError($"Are we the server: {NetworkManager.Instance.IsServer}");
        }

	}



	public void SitDown()
	{
		networkObject.SendRpc(RPC_EXAMINE, Receivers.All, (int)Actions.CTA_Patient_Sit);
	}


	public void StandUp()
    {
		networkObject.SendRpc(RPC_EXAMINE, Receivers.All, (int)Actions.CTA_Patient_Stand);
	}

	public void SynchAnimation(Actions action)
    {
		networkObject.SendRpc(RPC_EXAMINE, Receivers.All, (int)action);
    }


	
	public override void Examine(RpcArgs args)
	{


		int selection = args.GetNext<int>();
		bool state = false;

		GameObject localPlayer;
		PlayerNetwork playerInfo;

		MainThreadManager.Run(() =>
		{
            if ((Actions)selection != Actions.CTA_PATIENT_GOWN_RIGHT && (Actions)selection != Actions.CTA_PATIENT_GOWN_LEFT
			&& (Actions)selection != Actions.CTA_PATIENT_GOWN_NORMAL && (Actions)selection != Actions.CTA_PATIENT_GOWN_OPEN)
            {
				playerAnimationController.SetBool("isStanding", false);
				if((Actions)selection != Actions.CTA_Patient_Lying_RaiseLeft && (Actions)selection != Actions.CTA_Patient_Sitting_RaiseLeft) playerAnimationController.SetBool("raiseLeft", false);
				if ((Actions)selection != Actions.CTA_Patient_Lying_RaiseRight && (Actions)selection != Actions.CTA_Patient_Sitting_RaiseRight) playerAnimationController.SetBool("raiseRight", false);
				if ((Actions)selection != Actions.CTA_Patient_Sitting_RaiseBoth) playerAnimationController.SetBool("raiseBoth", false);
				if ((Actions)selection != Actions.CTA_Patient_Sitting_Tripod) playerAnimationController.SetBool("tripodPosition", false);
				if ((Actions)selection != Actions.CTA_Patient_Sitting_TouchTender) playerAnimationController.SetBool("touchTender", false);

            }

			switch ((Actions)selection)
            {

				case Actions.CTA_Patient_Stand:
					playerAnimationController.SetBool("isStanding", true);
                    moodbar.localPosition = new Vector3(35f, standingMoodbarPosition, 0);
                    break;

				case Actions.CTA_Patient_Sit:
					playerAnimationController.SetTrigger("sitTrigger");
                    moodbar.localPosition = new Vector3(35f, sittingMoodbarPosition, 0);

					localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer");


					if (localPlayer != null)
					{
						playerInfo = localPlayer.GetComponent<PlayerNetwork>();

						if (playerInfo.playerType == PlayerType.IMG_DOCTOR)
						{
							localPlayer.GetComponent<DoctorButtonSetup>().SwitchExaminePosition(1);
							
						}

					}
					break;


				case Actions.CTA_Patient_Lying_RaiseLeft:
				case Actions.CTA_Patient_Sitting_RaiseLeft:
					state = playerAnimationController.GetBool("raiseLeft");
					playerAnimationController.SetBool("raiseLeft", !state);
					break;

				case Actions.CTA_Patient_Lying_RaiseRight:
				case Actions.CTA_Patient_Sitting_RaiseRight:
					state = playerAnimationController.GetBool("raiseRight");
					playerAnimationController.SetBool("raiseRight", !state);
					break;


				case Actions.CTA_Patient_Lying:
					playerAnimationController.SetTrigger("lieTrigger");
                    moodbar.localPosition = new Vector2(35f, lyingMoodbarPosition);

					localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer");


                    if (localPlayer != null)
                    {
						playerInfo = localPlayer.GetComponent<PlayerNetwork>();

						if(playerInfo.playerType == PlayerType.IMG_DOCTOR)
                        {

							localPlayer.GetComponent<DoctorButtonSetup>().SwitchExaminePosition(2);
						}
                    }


					break;

				case Actions.CTA_Patient_Lying_RaiseBoth:
				case Actions.CTA_Patient_Sitting_RaiseBoth:
					state = playerAnimationController.GetBool("raiseBoth");
					playerAnimationController.SetBool("raiseBoth", !state);
					break;

				case Actions.CTA_Patient_Sitting_Tripod:
					state = playerAnimationController.GetBool("tripodPosition");
					playerAnimationController.SetBool("tripodPosition", !state);
					break;

				case Actions.CTA_Patient_Sitting_TouchTender:
					state = playerAnimationController.GetBool("touchTender");
					playerAnimationController.SetBool("touchTender", !state);
					break;


				case Actions.CTA_PATIENT_GOWN_RIGHT:
					gownRight.SetActive(true);
					gownNormal.SetActive(false);
					gownLeft.SetActive(false);
					gownOpen.SetActive(false);
					casualClothes.SetActive(false);
					break;

				case Actions.CTA_PATIENT_GOWN_LEFT:
					gownLeft.SetActive(true);
					gownNormal.SetActive(false);
					gownRight.SetActive(false);
					gownOpen.SetActive(false);
					casualClothes.SetActive(false);
					break;

				case Actions.CTA_PATIENT_GOWN_NORMAL:
					gownNormal.SetActive(true);
					gownLeft.SetActive(false);
					gownRight.SetActive(false);
					gownOpen.SetActive(false);
					casualClothes.SetActive(false);
					break;

				case Actions.CTA_PATIENT_GOWN_OPEN:
					gownNormal.SetActive(false);
					gownLeft.SetActive(false);
					gownRight.SetActive(false);
					gownOpen.SetActive(true);
					casualClothes.SetActive(false);
					break;

				case Actions.CTA_PATIENT_GOWN_OFF:
					gownNormal.SetActive(false);
					gownLeft.SetActive(false);
					gownRight.SetActive(false);
					gownOpen.SetActive(false);
					casualClothes.SetActive(false);
					break;
					/*
				case Actions.CTA_Patient_Lying_RaiseLeft:
					state = playerAnimationController.GetBool("raiseLeft");
					playerAnimationController.SetBool("raiseLeft", !state);
					break; */

			}

		

		});
	}

	public void ModifyMoodBar(int fillAmount)
	{
		networkObject.SendRpc(RPC_SYNCH_ACTION, Receivers.All, fillAmount);
	}


	public override void SynchAction(RpcArgs args)
	{
		MainThreadManager.Run(() =>
		{
			int fillValue = args.GetNext<int>();


			patientWorldspaceMoodBar.SetActive(true);
            moodbar.localPosition = new Vector2(35f, moodbar.localPosition.y);

			var doc = GameObject.FindGameObjectWithTag("LocalPlayer");
			if (doc != null)
			{
				if (doc.GetComponent<PlayerNetwork>().playerType == PlayerType.IMG_DOCTOR)
				{

					bool doctorExamineMode = doc.GetComponent<DoctorButtonSetup>().isExamining;
					if (doctorExamineMode)
					{
						moodBarCurrentScale = 0.5f;
						moodbar.localScale = new Vector3(moodBarCurrentScale, moodBarCurrentScale, moodBarCurrentScale);

						var patientAnim = GetComponent<Animator>();
						if (patientAnim.GetCurrentAnimatorStateInfo(0).IsName("Lying Down")
						|| patientAnim.GetCurrentAnimatorStateInfo(0).IsName("Raise Left Arm Lying")
						|| patientAnim.GetCurrentAnimatorStateInfo(0).IsName("Raise Right Arm Lying")
						|| patientAnim.GetCurrentAnimatorStateInfo(0).IsName("Raise Both Arms Lying"))
                        {
							moodbar.localPosition = new Vector3(-10, -120, 60);
						}
                        else
                        {
							moodbar.localPosition = new Vector2(35f, sittingMoodbarPosition - 70f);
						}

							
					}
					else
					{
						moodbar.localScale = new Vector3(moodBarOriginalScale, moodBarOriginalScale, moodBarOriginalScale);
						moodBarCurrentScale = 1;
						moodbar.localPosition = new Vector3(35f, standingMoodbarPosition, 0f);
					}
				}
			}

			StartCoroutine(animateMoodBarImage());

			switch (fillValue)
			{

				case 1:
					moodImg.sprite = angry;
					moodBarText.text = "Offended";
					print("got here");

					var counter = GameObject.Find("offendedCounter").GetComponent<Text>(); //only applies to doctors (img)


					if (counter != null)
					{
						StartCoroutine(animateCounter(counter.GetComponent<RectTransform>()));
						counter.text = $"X {++_offendedCounter}";
					}

					break;
				case 2:
					moodImg.sprite = anxious;
					moodBarText.text = "Anxious";
					var counter2 = GameObject.Find("anxiousCounter").GetComponent<Text>(); //only applies to doctors (img)
					if (counter2 != null)
					{
						counter2.text = $"X {++_anxiousCounter}";
						StartCoroutine(animateCounter(counter2.GetComponent<RectTransform>()));
					}
					break;
				case 3:
					moodImg.sprite = confused;
					moodBarText.text = "Confused";
					var counter3 = GameObject.Find("confusedCounter").GetComponent<Text>(); //only applies to doctors (img)
					if (counter3 != null) {
						counter3.text = $"X {++_confusedCounter}";
						StartCoroutine(animateCounter(counter3.GetComponent<RectTransform>()));
						}
					break;
				case 4:
					moodImg.sprite = embarassed;
					moodBarText.text = "Embarassed";
					var counter4 = GameObject.Find("embarassedCounter").GetComponent<Text>(); //only applies to doctors (img)
					if (counter4 != null)
					{
						counter4.text = $"X {++_embarassedCounter}";
						StartCoroutine(animateCounter(counter4.GetComponent<RectTransform>()));
					}
					break;
				case 5:
					moodImg.sprite = calm;
					moodBarText.text = "Calm";

					var counter5 = GameObject.Find("calmCounter").GetComponent<Text>(); //only applies to doctors (img)
					if (counter5 != null)
                    {
						counter5.text = $"X {++_calmCounter}";
						StartCoroutine(animateCounter(counter5.GetComponent<RectTransform>()));
					}	

					break;
			}

		});

	}

	IEnumerator animateMoodBarImage()
    {
		RectTransform moodBarTransform = moodbar.GetComponent<RectTransform>();
		float scale = .25f;
		while(scale < moodBarCurrentScale)
        {
			scale += 0.03f;
			moodBarTransform.localScale = new Vector3(scale, scale, scale);
			yield return new WaitForSeconds(0.01f);
        }

		moodBarTransform.localScale = new Vector3(moodBarCurrentScale, moodBarCurrentScale, moodBarCurrentScale);
	}

	
	IEnumerator animateCounter(RectTransform counter)
    {
		int c = 0;
		int animationFrames = 12;
		float step = 1.25f;

		Vector3 counterOriginalPos = counter.localPosition;

		while (c++ < animationFrames)
        {
			counter.localPosition = new Vector3(counterOriginalPos.x, counter.localPosition.y + step, counterOriginalPos.z);
			yield return new WaitForSeconds(0.01f);
        }

		counter.localPosition = new Vector3(counterOriginalPos.x, counterOriginalY, counterOriginalPos.z);
	}
}
