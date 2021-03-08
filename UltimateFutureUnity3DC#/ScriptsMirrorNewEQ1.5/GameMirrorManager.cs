using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMirrorManager : NetworkBehaviour
{
    private void Awake()
    {
        if (isServerOnly)
        {
            Debug.Log("true");
        }
        else Debug.Log("Nah");
    }
}
