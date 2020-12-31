using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Text messagePanelTxt;

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
}
