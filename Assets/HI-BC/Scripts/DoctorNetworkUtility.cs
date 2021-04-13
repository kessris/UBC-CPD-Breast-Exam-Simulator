using System.Collections;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;

public class DoctorNetworkUtility : DoctorNetworkControllerBehavior
{

    public AudioSource knocking;
    public AudioSource washingHands;
    public Transform rightHandObj;
    public Transform leftHandObj;
    [SerializeField]
    IKControl handControl;


    Animator _animator;
    GameObject sinkWater;

    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null) _animator = transform.Find("model").GetComponent<Animator>();
        sinkWater = GameObject.Find("SinkWater");
        sinkWater.transform.Find("water").gameObject.SetActive(false);
        //handControl = transform.Find("model").GetComponent<IKControl>();

        if(GetComponent<PlayerNetwork>().playerType!=PlayerType.OBSERVER)
            FollowCam.Instance.RotateTowards(180, 3);

    }

    private void Update()
    {
        
        if (!networkObject.IsOwner)
        {
            // Assign the position of this cube to the position sent on the network
            rightHandObj.position = networkObject.RightHandPosition;
            leftHandObj.position = networkObject.LeftHandPosition;
            
            return;
        }
        

        // Since we are the owner, tell the network the updated position
        networkObject.RightHandPosition = rightHandObj.position;
        networkObject.LeftHandPosition = leftHandObj.position;

    }

    IEnumerator WashHands()
    {
        if(Vector3.Distance(transform.position, sinkWater.transform.position) <= 4f)
        {
            sinkWater.transform.Find("water").gameObject.SetActive(true);
            washingHands.Play();
            yield return new WaitForSeconds( 5f);
            washingHands.Stop();
            yield return new WaitForSeconds(2f);
            sinkWater.transform.Find("water").gameObject.SetActive(false);
        }

        yield return null;
    }

    public void SynchDoctorAction(int selection)
    {
        networkObject.SendRpc(RPC_DOCTOR_ACTION, Receivers.All, selection);
    }


    public override void DoctorAction(RpcArgs args)
    {
        int action = args.GetNext<int>();


		MainThreadManager.Run(() =>
		{

            if(action == 1) //knock on door
            {
                _animator.SetTrigger("knock");
                knocking.Play();
            }
            if(action == 2) //wash hands
            {
                _animator.SetTrigger("washHands");
                StartCoroutine(WashHands());
            }

            if(action == 3) //Examine
            {
                handControl.ToggleExamining(true);
            }

            if(action == 4) //Stop examine
            {
                handControl.ToggleExamining(false);
            }
		});
	}

}
