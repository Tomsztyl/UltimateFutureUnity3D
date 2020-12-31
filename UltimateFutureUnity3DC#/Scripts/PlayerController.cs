using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static Animator anim;

    //Default Settings Move Player
    [SerializeField] private float speedDef = 5f;
    [SerializeField] private float rotationSpeedDef = 50.0f;
    [SerializeField] private float speedAnimationDef = 0f;
    [SerializeField] private float rotationAnimationDef = 0f;
    [SerializeField] private bool isWalkingDef = false;

    //Default Settings Move Player Aim Gun
    [SerializeField] private KeyCode aimGun = KeyCode.Mouse1;
    [SerializeField] private float speedGunAim = 4f;
    [SerializeField] private float rotationSpeedGunAim = 40.0f;


    //Default Setting Sprint Player
    [SerializeField] private float speedSprintToAdd = 3f;
    [SerializeField] private float rotationSpeedSprintingToAdd = 10f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;


    //Variables To check is gun
    public int IsGun = 0;
    public GameObject HandWithGun;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveSelectGunCharacter();
        PlayerAimGunStanding();
    }
    private void PlayerMoveSelectGunCharacter()
    {
        //Variables A
        //0-is No gun
        //1-in gun 

        if (IsGun == 0)
        {
            anim.SetBool("isGun", false);
            PlayerCheckIsSprint("speed");
            PlayerMove(speedDef, rotationSpeedDef, "speed");
            PlayerMoveAnimation(speedAnimationDef, rotationAnimationDef, isWalkingDef, "isRunBack", "isLeftTurn", "isRightTurn");
        }
        else if (IsGun == 1)
        {
            anim.SetBool("isGun", true);
            HandWithGun.SetActive(true);
            PlayerMove(speedGunAim, rotationSpeedGunAim, "speedGunAim");
            PlayerCheckIsSprint("speedGunAim");
            PlayerMoveAnimation(speedAnimationDef, rotationAnimationDef, isWalkingDef, "isRunBack", "isLeftTurn", "isRightTurn");

        }
        else
        {
            //Default Option
            anim.SetBool("isGun", false);
            PlayerMove(speedDef, rotationSpeedDef, "speed");
            PlayerMoveAnimation(speedAnimationDef, rotationAnimationDef, isWalkingDef, "isRunBack", "isLeftTurn", "isRightTurn");
        }
    }
    private void PlayerMove(float speed, float rotationSpeed, string speedChoiceAnimator)
    {
        float translation = Input.GetAxis("Vertical") * speed;
        speedAnimationDef = translation;
        anim.SetFloat(speedChoiceAnimator, speedAnimationDef);
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        rotationAnimationDef = rotation;
        translation *= Time.deltaTime;

        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if (speedAnimationDef != 0)
        {
            //Is Walking
            anim.SetBool("isWalking", true);
            isWalkingDef = true;
        }
        else
        {
            //Is no Walking
            anim.SetBool("isWalking", false);
            isWalkingDef = false;
        }
    }
    private void PlayerMoveAnimation(float speedAnimation, float rotationAnimation, bool isWalking, string isRunBack, string isLeftTurn, string isRightTurn)
    {

        if (speedAnimation < 0f)
        {
            anim.SetBool(isRunBack, true);
        }
        else
        {
            anim.SetBool(isRunBack, false);
        }



        if (rotationAnimation < 0f && isWalking == false)
        {
            //Animation Left
            anim.SetBool(isLeftTurn, true);
        }
        else
        {
            anim.SetBool(isLeftTurn, false);
        }

        if (rotationAnimation > 0f && isWalking == false)
        {
            //Animation Right
            anim.SetBool(isRightTurn, true);
        }
        else
        {
            anim.SetBool(isRightTurn, false);
        }
    }
    private void PlayerAimGunStanding()
    {
        if (Input.GetKeyDown(aimGun))
        {
            Debug.Log("Input Key :" + aimGun);
        }

        if (Input.GetKeyUp(aimGun))
        {
            Debug.Log("Input Key Up:" + aimGun);
        }
    }
    public float GetRotationAnimationDef()
    {
        return rotationAnimationDef;
    }
    private void PlayerCheckIsSprint(string speedToStringAnimator)
    {
        if (Input.GetKeyDown(sprintKey))
        {
            //Start Sprinting
            if (speedToStringAnimator== "speed")
            {
                speedDef += speedSprintToAdd;
                rotationSpeedDef += rotationSpeedSprintingToAdd;
            }
            else if (speedToStringAnimator == "speedGunAim")
            {
                speedGunAim += speedSprintToAdd;
                rotationSpeedGunAim += rotationSpeedSprintingToAdd;
            }
            
        }
        else if (Input.GetKeyUp(sprintKey))
        {
            //Stop Sprinting
            if (speedToStringAnimator == "speed")
            {
                speedDef -= speedSprintToAdd;
                rotationSpeedDef -= rotationSpeedSprintingToAdd;
            }
            else if (speedToStringAnimator == "speedGunAim")
            {
                speedGunAim -= speedSprintToAdd ;
                rotationSpeedGunAim -= rotationSpeedSprintingToAdd;
            }
        }
    }
}
