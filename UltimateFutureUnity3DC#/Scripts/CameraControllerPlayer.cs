using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerPlayer : MonoBehaviour
{
    //Variable Select Camera
    [SerializeField] private bool thirdCamera = false;
    [SerializeField] private bool firstCamera = false;

    [SerializeField] private GameObject thirdCameraPref;
    [SerializeField] private GameObject firstCameraPref;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField][Range(0.01f,1.0f)]
    private float SmoothFactory=0.5f;
    [SerializeField] private bool lookAtPlayer = false;
    [SerializeField] private bool rotateAroundPlayer = true;
    [SerializeField] private float rotationSpeed = 5.0f;

    //[SerializeField] private float speedHorizontalCamera = 2.0f;
    //[SerializeField] private float speedVerticalCamera = 2.0f;
    //[SerializeField] private float yawCamera = 0.0f;
    //[SerializeField] private float pitchCamera = 0.0f;


    private void Start()
    {
        //cameraOffset = thirdCameraPref.transform.position - playerTransform.position;
    }
    private void LateUpdate()
    {
        if (rotateAroundPlayer)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);

            cameraOffset = camTurnAngle * cameraOffset;
        }

        Vector3 newPos = playerTransform.position + cameraOffset;
        thirdCameraPref.transform.position = Vector3.Slerp(thirdCameraPref.transform.position, newPos, SmoothFactory);

        if (lookAtPlayer)
            thirdCameraPref.transform.LookAt(playerTransform);
    }

    private void Update()
    {
        SelectCamera();
    }

    private void SelectCamera()
    {
        if (thirdCamera==true)
        {
            thirdCameraPref.SetActive(true);
            firstCamera =DeActiveCameraSelect(firstCameraPref);
            RotateCameraWithMouse(thirdCameraPref);
        }
        else if (firstCamera==true)
        {
            firstCameraPref.SetActive(true);
            thirdCamera= DeActiveCameraSelect(thirdCameraPref);
            RotateCameraWithMouse(firstCameraPref);
        }
        else
        {
            return;
        }
    }
    private bool DeActiveCameraSelect(GameObject deActiveCameraObj)
    {
        deActiveCameraObj.SetActive(false);
        return false;
    }
    private void RotateCameraWithMouse(GameObject selectCameraRotate)
    {
        //yawCamera += speedHorizontalCamera * Input.GetAxis("Mouse X");
        //pitchCamera -= speedVerticalCamera * Input.GetAxis("Mouse Y");

       // selectCameraRotate.transform.eulerAngles = new Vector3(pitchCamera, yawCamera, 0.0f);
        //PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //if (playerController.GetRotationAnimationDef()!=0)
        //{
        //    //Debug.Log("Rotation is set: "+ new Vector3(0.0f,selectCameraRotate.transform.eulerAngles.y,selectCameraRotate.transform.eulerAngles.y)); 
        //    selectCameraRotate.transform.eulerAngles=new Vector3(0.0f, 0.0f, 0.0f);
        //}
        //selectCameraRotate.transform.LookAt(transform.position);
        //selectCameraRotate.transform.LookAt(transform.position, Vector3.forward);
    }
}
