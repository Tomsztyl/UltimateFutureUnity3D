using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour
{
    [SerializeField] private Text comandText;
    [SerializeField] private Text comandTextView;
    [SerializeField] private List<string> comandConsole = new List<string>();


    //FreeCamera
    [SerializeField] private GameObject freeCamera;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private bool isActiveFreeCamera = false;

    //TimeConsole
    [SerializeField] private DateTime dataTime = DateTime.Now;

    private void Start()
    {
        playerController =this.gameObject.transform.root.GetComponent<PlayerController>();
    }

    public void ExeciuteComand()
    {
        bool comandExist = false;
        for (int i =0;i<comandConsole.Count;i++)
        {
            if (comandText.text==comandConsole[i])
            {
                comandExist = true;
                ManagerComand(i);
            }
        }
        if (!comandExist)
        {
            string textVisual = "[Console Manager]" + "[" + dataTime + "]" + "Warning! I not recognize this comand! Write !Help";
            comandTextView.text += "\n" + textVisual;
            Debug.LogWarning(textVisual);
        }
    }

    private void ManagerComand(int idComand)
    {
        switch (idComand)
        {
            case 0:
                SwitchCamera(comandConsole[idComand]);
                break;
            case 1:
                SwitchCamera(comandConsole[idComand]);
                break;
            case 2:
                SwitchCamera(comandConsole[idComand]);
                break;
            case 3:
                PrintAllEnableComand();
                break;
            case 4:
                comandTextView.text = "";
                break;
            case 5:
                QuitGame();
                break;
            default:
                string textVisual = "[Console Manager]"+"["+dataTime+"]"+"Warning! I not recognize this comand!";
                comandTextView.text += "\n" + textVisual;
                Debug.LogWarning(textVisual);
                break;
        }
    }
    private void SwitchCamera(string cameraController)
    {
        if (cameraController== "!ChangeFirstCamera")
        {
            FreeCameraManager();
            CameraControllerPlayer cameraControllerPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraControllerPlayer>();
            cameraControllerPlayer.firstCamera = true;
            cameraControllerPlayer.thirdCamera = false;
            string textVisual = "[Console Manager] Warning! I switch Camera" + cameraController;
            comandTextView.text += "\n"+textVisual;
            Debug.LogWarning(textVisual);
        }
        else if (cameraController== "!ChangeThirdCamera")
        {
            FreeCameraManager();
            CameraControllerPlayer cameraControllerPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraControllerPlayer>();
            cameraControllerPlayer.firstCamera = false;
            cameraControllerPlayer.thirdCamera = true;
            string textVisual = "[Console Manager]" + "[" + dataTime + "]" + "Warning! I switch Camera" + cameraController;
            comandTextView.text += "\n" + textVisual;
            Debug.LogWarning(textVisual);
        }
        else if (cameraController== "!FreeCamera")
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            CameraControllerPlayer cameraControllerPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraControllerPlayer>();
            playerController.enabled = false;
            cameraControllerPlayer.firstCamera = false;
            cameraControllerPlayer.thirdCamera = false;
            Instantiate(freeCamera, playerTransform.transform.position,Quaternion.identity);
            isActiveFreeCamera = true;
            string textVisual = "[Console Manager]" + "[" + dataTime + "]" + "Warning! I switch Camera" + cameraController;
            comandTextView.text += "\n" + textVisual;
            Debug.LogWarning(textVisual);
        }
        else
        {
            string textVisual = "[Console Manager]" + "[" + dataTime + "]" + "I don't Recognize this comand";
            Debug.LogError(textVisual);
        }
    }
    private void FreeCameraManager()
    {
        if (isActiveFreeCamera)
        {
            FreeCam freeCam = GameObject.FindGameObjectWithTag("CameraFree").GetComponent<FreeCam>();
            isActiveFreeCamera = false;
            freeCam.Freecamera = isActiveFreeCamera;
            playerController.enabled = true;
        }
    }
    private void PrintAllEnableComand()
    {
        for (int i=0;i<comandConsole.Count;i++)
        {
            string textVisual = "[Console Manager]" + "[" + dataTime + "]" + "!Help Comand: " +comandConsole[i];
            comandTextView.text += "\n" + textVisual;
        }
    }
    private void QuitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }
}

