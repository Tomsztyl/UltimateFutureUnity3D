using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    [SerializeField]private CameraControllerPlayer cameraControllerPlayer=null;
    [SerializeField]private bool pointerActive = false;

    private void Start()
    {
        if (cameraControllerPlayer == null)
        {
            cameraControllerPlayer =gameObject.transform.root.GetComponent<CameraControllerPlayer>();
        }

    }
    
    void Update()
    {
        if (cameraControllerPlayer != null)
        {
            pointerActive = true;
            PointerMove();
        }

    }
    private void PointerMove()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.

        Camera camera = cameraControllerPlayer.GetChooseCamera().GetComponent<Camera>();

        this.transform.position = camera.ScreenToWorldPoint(temp);
    }
}
