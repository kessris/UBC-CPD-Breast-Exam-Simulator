using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//INSTRUCTIONS:
//Camerabase object contains just FollowCam script. 
//Maincamera = child of camerabase, contains camera + followcamcollision script. make sure to set main camera z to like -1 at the very least.

public class FollowCam : MonoBehaviour
{
    #region Singleton
    public static FollowCam Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
        DontDestroyOnLoad(this);
    }
    #endregion


    public float CameraMoveSpeeed = 120.0f;
    public GameObject CameraFollowObj;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public float rotY = 0.0f, rotX = 0.0f;

    public FollowCamCollision _camCollision;

    private float mouseX, mouseY, finalInputX, finalInputZ;


    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;


        _camCollision = transform.GetChild(0).GetComponent<FollowCamCollision>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if (CameraFollowObj == null) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            if (Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            
        }

        //if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButton(1)) // rotate with right mouse button
        {
            /**
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            finalInputX = mouseX; // + inputX;
            finalInputZ = mouseY; //+inputZ

            rotY += finalInputX * inputSensitivity * Time.deltaTime;
            rotX -= finalInputZ * inputSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);

            transform.rotation = localRotation;
    **/
        }
        else
        {

            var y = Input.GetAxis("Horizontal");

            rotY += y * inputSensitivity * Time.deltaTime;


            var v3 = new Vector3(0, y, 0.0f);
            transform.Rotate(v3 * inputSensitivity * Time.deltaTime);
        }

        //if(Input.GetKeyDown(KeyCode.F2)) _camCollision.maxDistance = 0.1f;
    }

    public void RotateTowardsPatient(float xAngle)
    {
        StartCoroutine(RotateToPatient(xAngle));
    }

    IEnumerator RotateToPatient(float xAngle)
    {

        while (true)
        {

            yield return new WaitForEndOfFrame();
            var v3 = new Vector3(0, 1, 0.0f);
            if (rotY > -5f)
            {
                rotY -= inputSensitivity * Time.deltaTime;
                transform.Rotate(v3 * -inputSensitivity * Time.deltaTime);
            }
            else
            {
                rotY += inputSensitivity * Time.deltaTime;
                transform.Rotate(v3 * inputSensitivity * Time.deltaTime);
            }

            if (rotY >= -5.5f && rotY <= -5f)
            {
                break;
            }
        }

        transform.localEulerAngles = new Vector3(xAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private void LateUpdate()
    {

        if (CameraFollowObj == null) return;
        CameraUpdater();
    }

    void CameraUpdater()
    {
        Transform target = CameraFollowObj.transform; //set target obj to follow

        //move towards the game object that is the target
        float step = CameraMoveSpeeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

    }

    float sittingViewAngle = 17.5f;
    public void TogglePerspective(bool isFirstPerson)
    {
        if (isFirstPerson)
        {
            StartCoroutine(ChangeToFirstPerson());

            var patientDetector = GameObject.Find("PatientDetector");

            if (patientDetector != null)
            {
                var patient = patientDetector.GetComponent<PatientDetector>().patient;

                if (patient != null)
                {
                    var animator = patient.GetComponent<Animator>();
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Lying Down")
                        || animator.GetCurrentAnimatorStateInfo(0).IsName("Raise Left Arm Lying")
                        || animator.GetCurrentAnimatorStateInfo(0).IsName("Raise Right Arm Lying")
                        || animator.GetCurrentAnimatorStateInfo(0).IsName("Raise Both Arms Lying")) //lying down pos
                    {
                        transform.localEulerAngles = new Vector3(32f, -383f, transform.localEulerAngles.z);
                        rotY = -383f;
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(sittingViewAngle, -367.964f, transform.localEulerAngles.z);
                        rotY = -367.964f;
                    }
                }
                else
                {
                    transform.localEulerAngles = new Vector3(sittingViewAngle, -367.964f, transform.localEulerAngles.z);
                    rotY = -367.964f;
                }
            }
            else
            {
                transform.localEulerAngles = new Vector3(sittingViewAngle, -367.964f, transform.localEulerAngles.z);
                rotY = -367.964f;

            }

            

        }
        else
        {
            _camCollision.enabled = true;
            _camCollision.maxDistance = 1.5f;
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
    }

    public void RotateTowards(float angle, int position)
    {
        if(position == 1) //1 = sitting
        {
            transform.localEulerAngles = new Vector3(sittingViewAngle, angle, transform.localEulerAngles.z);
            rotY = angle;
        }
        else if(position ==2) //2 = lying
        {
            transform.localEulerAngles = new Vector3(32f, angle, transform.localEulerAngles.z);
            rotY = angle;
        }
        else
        {
            transform.localEulerAngles = new Vector3(0f, angle, transform.localEulerAngles.z);
            rotY = angle;
        }

    }

    private IEnumerator ChangeToFirstPerson()
    {
        _camCollision.maxDistance = 0.1f;
        yield return new WaitForSeconds(0.8f);
        _camCollision.enabled = false;
    }
}
