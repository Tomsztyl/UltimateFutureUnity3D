using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerPlayer : MonoBehaviour
{
    //Variable Select Camera
    [SerializeField] public bool thirdCamera = false;
    [SerializeField] public bool firstCamera = false;
    private AuthentionController authentionController;
    private GameObject cameraChoose;

    //KeyToSelectCamera
    [SerializeField] private KeyCode keySwitchThirdCamera = KeyCode.F1;
    [SerializeField] private KeyCode keySwitchFirstCamera = KeyCode.F2;

    [SerializeField] private GameObject thirdCameraPref;
    [SerializeField] private GameObject firstCameraPref;

    //Varaible to Aim Rifle Select Camera
    [Tooltip("Set Pivot For The Camera")]
    [SerializeField] private Vector3 pivotTranform;
    [SerializeField] private Vector3 pivotTranformRifle=new Vector3(1.68f,2f,1.18f);
    [SerializeField] private Transform pivotCameras;


    private void Start()
    {
        authentionController = GameObject.FindObjectOfType<AuthentionController>().GetComponent<AuthentionController>();
        //Default ThirdCamera
        cameraChoose = thirdCameraPref;
        pivotCameras = thirdCameraPref.transform.parent;
        pivotTranform = thirdCameraPref.transform.parent.localPosition;
    }

    private void Update()
    {
        CheckChooseCamera();
    }

    private void LateUpdate()
    {
        SelectCamera();
    }

    private void CheckChooseCamera()
    {
        if (Input.GetKeyDown(keySwitchThirdCamera))
        {
            firstCamera = false;
            thirdCamera = true;
        }
        else if (Input.GetKeyDown(keySwitchFirstCamera))
        {
            thirdCamera = false;
            firstCamera = true;
        }
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
            cameraChoose = thirdCameraPref;
            thirdCameraPref.SetActive(true);
            firstCamera =DeActiveCameraSelect(firstCameraPref);
        }
        else if (firstCamera==true)
        {
            cameraChoose = firstCameraPref;
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
    public void SetThirdCamera(GameObject camera)
    {
        thirdCameraPref = camera;
    }
    public GameObject GetChooseCamera()
    {
        return cameraChoose;
    }
    public void ChangePivotCameraRifle()
    {
        pivotCameras.transform.localPosition = pivotTranformRifle;
    }
    public void ChangePivotCameraRifleDefault()
    {
        pivotCameras.transform.localPosition = pivotTranform;
    }
}
