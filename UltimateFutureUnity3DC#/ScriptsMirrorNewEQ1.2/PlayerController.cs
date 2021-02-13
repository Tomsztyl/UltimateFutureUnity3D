using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : NetworkBehaviour
{
    private Animator anim;

    //public override void OnStartLocalPlayer()
    //{
    //    Camera.main.orthographic = false;
    //    Camera.main.transform.SetParent(transform);
    //    Camera.main.transform.localPosition = new Vector3(0f, 3f, -8f);
    //    Camera.main.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
    //}

    //void OnDisable()
    //{
    //    if (isLocalPlayer && Camera.main != null)
    //    {
    //        Camera.main.orthographic = true;
    //        Camera.main.transform.SetParent(null);
    //        Camera.main.transform.localPosition = new Vector3(0f, 70f, 0f);
    //        Camera.main.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
    //    }
    //}

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

    //Default Setting Junp Player
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float distanceToGround;
    [SerializeField] private float jumpAceleration = 100f;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Rigidbody rigidbody;


    //Variables To check is gun
    public int IsGun = 0;
    // Start is called before the first frame update




    void Start()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveSelectGunCharacter();
        PlayerAimGunStanding();
        PlayerJump();
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
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distanceToGround + 0.1));
    }
    private void PlayerJump()
    {
        if (Input.GetKeyDown(jumpKey) && IsGrounded())
        {
            Debug.LogWarning("Jump");
            rigidbody.AddForce(new Vector3(0, jumpAceleration, 0), ForceMode.Impulse);
            anim.SetBool("isJumping", true);
        }
        else
        {
            CheckIsGrounded();
        }


    }
    private void CheckIsGrounded()
    {
        if (IsGrounded())
        {
            anim.SetBool("isJumping", false);
            //Debug.LogWarning("Is Ground");
        }
        else
        {
            //Debug.LogError("No Ground");
        }
    }
}
