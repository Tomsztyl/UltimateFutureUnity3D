using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BoxController : NetworkBehaviour
{
    [SerializeField] private PlayerMirrorController playerMirror;
    [SerializeField] private NetworkIdentity networkIdentity;


    [SerializeField] private KeyCode interactKey=KeyCode.F;
    
    private bool isInColider=false;

    [SerializeField] private GameObject startPatricles;
    [SerializeField] private GameObject box001;
    [SerializeField] private GameObject destroy_box;

    //List Who is Instantiate GameObjcet from Box
    [SerializeField] private List<GameObject> objectInstantiateToDestroy = new List<GameObject>();

    //When Box is lover than -20f position y is destroy
    [SerializeField] private float rangeDestroyBoxWhenIsNoTerrain = -20f;

    //Radius Instantiate Object
    [SerializeField] private float chaseDistance = 5f;
    [SerializeField] private float chaseDistanceY=10f;
    [SerializeField] private Vector3 vec3Change;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            networkIdentity = other.GetComponent<NetworkIdentity>();
            playerMirror = NetworkIdentity.spawned[networkIdentity.netId].GetComponent<PlayerMirrorController>();
            isInColider = true;
            ControlMessagePanel("- Press "+interactKey+" to pickup -", isInColider = true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
            networkIdentity = other.GetComponent<NetworkIdentity>();
            playerMirror = NetworkIdentity.spawned[networkIdentity.netId].GetComponent<PlayerMirrorController>();
            isInColider = false;
            ControlMessagePanel("",isInColider = false);
        }
    }
    private void Update()
    {
        vec3Change = new Vector3(transform.position.x, transform.position.y + chaseDistanceY, transform.position.z);
        if (isInColider == true)
        {
            TriggerInterraction(interactKey);
        }
        if (transform.position.y< rangeDestroyBoxWhenIsNoTerrain)
        {
            Destroy(this.gameObject);
        }
    }
    private void ControlMessagePanel(string textMessagePanel,bool isInColider)
    {
            UIManager uIManager = playerMirror.canvasHUD.GetComponent<UIManager>();
            uIManager.ManagerMessagePanel(textMessagePanel, isInColider);
    }
    private void TriggerInterraction(KeyCode interactKey)
    {
         if (Input.GetKeyDown(interactKey))
         {
            Debug.Log("is Triger KEy");
            ChangeAnimation();
            InstantiateRandomItemBox();
            DestroyObject();
         }

    }
    private void DestroyObject()
    {
        //Destroy Object to 5f second
        isInColider = false;
        ControlMessagePanel("", isInColider = false);
        NavMeshObstacle navMeshObstacle = GetComponent<NavMeshObstacle>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        rigidbody.useGravity = false;
        boxCollider.isTrigger = true;
        navMeshObstacle.enabled = false;
        Destroy(this.gameObject);
    }
    private void InstantiateRandomItemBox()
    {
        Debug.Log("List have size: " + objectInstantiateToDestroy.Count);
        //Instantiate count object of 1 to Object list count 
        for (int i=0;i<Random.Range(1, objectInstantiateToDestroy.Count);i++)
        {
            //Debug.Log(objectInstantiateToDestroy.Count - 1);
            int RandomObject= Random.Range(0, objectInstantiateToDestroy.Count);
            //Instantiate(objectInstantiateToDestroy[RandomObject], new Vector3
            //(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
            Instantiate(objectInstantiateToDestroy[RandomObject], objectInstantiateToDestroy[RandomObject].transform.position=
            vec3Change + Random.onUnitSphere * chaseDistance, Quaternion.identity);
        }
    }
    private void ChangeAnimation()
    {
        startPatricles.SetActive(false);
        box001.SetActive(false);
        Instantiate(destroy_box, transform.position, Quaternion.identity);

        //Set Bang Animation
        //Animator anim = GetComponent<Animator>();
        //anim.SetTrigger("isBang");
    }
    private void OnDrawGizmosSelected()
    {
        vec3Change = new Vector3(transform.position.x, transform.position.y + chaseDistanceY, transform.position.z);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(vec3Change, chaseDistance);
    }

}
