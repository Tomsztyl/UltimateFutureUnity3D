using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KindCharacter
{
    Player,
    NPC,
}
public class HealthController : NetworkBehaviour
{
    [Header("Set Current Healt")]
    [Tooltip("Default Healt is 100")]
    [SerializeField] private float healt=100;
    [SerializeField] private KindCharacter kindCharacter;

}
