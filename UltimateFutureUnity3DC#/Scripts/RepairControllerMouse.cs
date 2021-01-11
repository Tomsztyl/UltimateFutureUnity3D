using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RepairControllerMouse : MonoBehaviour
{
    [SerializeField] private bool disableMoueSelect = true;
    GameObject lastselect;

    void Start()
    {
        lastselect = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (disableMoueSelect)
            DisableMouseUI();

    }
    private void DisableMouseUI()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }

        

}
