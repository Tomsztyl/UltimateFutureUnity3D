using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerPlayer : MonoBehaviour
{
    //Variable Select Camera
    [SerializeField] public bool thirdCamera = false;
    [SerializeField] public bool firstCamera = false;

    [SerializeField] private GameObject thirdCameraPref;
    [SerializeField] private GameObject firstCameraPref;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField][Range(0.01f,1.0f)]
    private float SmoothFactory=0.5f;
    [SerializeField] private bool lookAtPlayer = false;
    [SerializeField] private bool rotateAroundPlayer = true;
    [SerializeField] private bool rotateAbovePlayer = true;
    [SerializeField] private float rotationSpeed = 5.0f;


    private void LateUpdate()
    {
        SelectCamera();
        RotateCameraAround();
        RotateCameraAbove();
    }

    private void SelectCamera()
    {
        if (thirdCamera==false&&firstCamera==false)
        {
            thirdCameraPref.SetActive(false);
            firstCameraPref.SetActive(false);
        }
        else if (thirdCamera==true)
        {
            thirdCameraPref.SetActive(true);
            firstCamera =DeActiveCameraSelect(firstCameraPref);
        }
        else if (firstCamera==true)
        {
            firstCameraPref.SetActive(true);
            thirdCamera= DeActiveCameraSelect(thirdCameraPref);
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
    private void RotateCameraAround()
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
    private void RotateCameraAbove()
    {

        if (rotateAbovePlayer)
        {

            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.left);

            cameraOffset = camTurnAngle * cameraOffset;
        }

        Vector3 newPos = playerTransform.position + cameraOffset;
        thirdCameraPref.transform.position = Vector3.Slerp(thirdCameraPref.transform.position, newPos, SmoothFactory);

        if (lookAtPlayer)
            thirdCameraPref.transform.LookAt(playerTransform);
    }
}
