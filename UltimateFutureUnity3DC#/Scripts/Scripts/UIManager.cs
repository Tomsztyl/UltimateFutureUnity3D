using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Text messagePanelTxt;

    //Variable Controller Console
    [SerializeField] private GameObject console;
    [SerializeField] private KeyCode keyControlConsole = KeyCode.BackQuote;

    private void Update()
    {
        EnableConsole();
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
}
