using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables who check If Game is Active
    [SerializeField] private bool isGameActive = false;
    [SerializeField] private bool isInstantiate = false;
    [SerializeField] private int howManyBoxCreate = 1;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boxPandora;

    private void Update()
    {
        if (isGameActive==true)
        {
            InstantiatePlayerAndBox();
        }
        else
        {
            //Debug.Log("Game is not started");
        }
    }
    private void InstantiatePlayerAndBox()
    {
        if (isInstantiate!=true)
        {
            return;
        }
        else
        {
            Instantiate(player, new Vector3(248.5316f, 0.7826424f, 246.4595f), Quaternion.identity);
            for (int i = 0; i < howManyBoxCreate; i++)
                Instantiate(boxPandora, new Vector3(Random.Range(0f, 248f), Random.Range(1.5f, 3f), Random.Range(0, 246f)), Quaternion.identity);
            Debug.Log("Is Instant: "+ howManyBoxCreate +" box");
            Screen.lockCursor = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isInstantiate = false;
        }

    }
}
