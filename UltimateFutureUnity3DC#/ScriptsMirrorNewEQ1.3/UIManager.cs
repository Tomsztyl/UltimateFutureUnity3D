using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : NetworkBehaviour
{
    [Header("This is Message Panel")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Text messagePanelTxt;

    [Header("This is a properties to Console")]
    //Variable Controller Console
    [SerializeField] private GameObject console;
    [SerializeField] private KeyCode keyControlConsole = KeyCode.BackQuote;

    [Header("This is a controls Equipment")]
    //Variable SelectBoxEq
    [SerializeField] private GameObject toolBox;
    [SerializeField] private KeyCode keyControlToolBox = KeyCode.E;
    bool activeToolBox = false;


    private void Update()
    {
        EnableConsole();

    }
    private void LateUpdate()
    {
        ChangeToolBoxActive();
    }

    public void ManagerMessagePanel(string textMessagePanel, bool isInColider)
    {
        if (isInColider==true)
        {
            messagePanel.SetActive(true);
            messagePanelTxt.text = textMessagePanel;
        }
        else
        {
            messagePanel.SetActive(false);
            messagePanelTxt.text = textMessagePanel;
        }
    }
    private void EnableConsole()
    {
        if (Input.GetKeyDown(keyControlConsole))
        {
            if (console.active == false)
            {
                Screen.lockCursor = false;
                Cursor.visible = true;
                console.SetActive(true);
            }
            else
            {
                Screen.lockCursor = true;
                Cursor.visible = false;
                console.SetActive(false);
            }
        }
    }
    private void ChangeToolBoxActive()
    {

        if (Input.GetKeyDown(keyControlToolBox) && activeToolBox == false)
        {
            toolBox.SetActive(true);
            activeToolBox = true;
        }
        else if (Input.GetKeyDown(keyControlToolBox)&&activeToolBox==true)
        {
            toolBox.SetActive(false);
            activeToolBox = false;
        }
    }
}
