using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField]private new List<Material> materialsGun = new List<Material>();
    /*Materiald List
     [0]GunTwoBlack
     [1]GunTwoBlue
     [2]GunTwoRad
     [3]GunTwoWhite
     [4]GunTwoYellow
     */

    [SerializeField] private MeshRenderer meshRendererGun;

    public void ChangeMaterialGun(Material materialWeapone)
    {
        //Debug.Log(materialsGun.Count);
        for(int i=0;i<materialsGun.Count;i++)
        {
            if (materialsGun[i] == materialWeapone)
            {
                meshRendererGun = GameObject.Find("SciFiGunLight").GetComponent<MeshRenderer>();
                meshRendererGun.sharedMaterial = materialsGun[i];
            }
        }
    }
}
