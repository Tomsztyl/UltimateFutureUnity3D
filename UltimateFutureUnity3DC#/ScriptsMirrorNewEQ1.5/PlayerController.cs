﻿using Mirror;
using Mirror.Examples.NetworkRoom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GunKind
{
    None,
    Pistol,
    Rifle
}

public class PlayerController : NetworkBehaviour
{
    private Animator anim;

    //Default Const Speed Player
    [Tooltip("This is Variables Const basic Speed Player")]
    [SerializeField] private const  float basicSpeed = 10f;
    [Tooltip("This is Variable Const basic Speed Rotation Player")]
    [SerializeField] private const float basicRotationSpeed = 100f;


    //Default Settings Move Player
    [Header("Default Settings Move Player")]
    [SerializeField] private float speedDef = 10f;
    [SerializeField] private float rotationSpeedDef = 100f;
    [SerializeField] private float speedAnimationDef = 0f;
    [SerializeField] private float rotationAnimationDef = 0f;
    [SerializeField] private bool isWalkingDef = false;

    //Default Settings Move Player Aim Gun
    [Header("Default Settings Move Player Aim Gun")]
    [SerializeField] private KeyCode aimGun = KeyCode.Mouse1;
    [SerializeField] private float speedGunAim = 4f;
    [SerializeField] private float rotationSpeedGunAim = 40.0f;
    private BoneAimingController boneAimingController;
    private CameraControllerPlayer cameraControllerPlayer;


    //Default Setting Sprint Player
    [Header("Default Setting Sprint Player")]
    [SerializeField] private float speedSprintToAdd = 3f;
    [SerializeField] private float rotationSpeedSprintingToAdd = 10f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    //Default Setting Junp Player
    [Header("Default Setting Junp Player")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float jumpAceleration = 100f;
    [SerializeField] private bool isGround = false;
    private CapsuleCollider capsuleCollider; 
    private Rigidbody rigidbody;


    //Variables To check is gun
    [Header("Variables To check Kind Gun")]
    [Tooltip("This enum pointer check kind gun in hand player")]
    public GunKind GunKind=GunKind.None;

    [Header("This is a Mechanic Camera")]
    public Transform m_Cam;
    public Vector3 m_CamForward;
    public Vector3 m_Move;



    void Start()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
        boneAimingController = GetComponent<BoneAimingController>();
        cameraControllerPlayer = GetComponent<CameraControllerPlayer>();
        SetBasicSpeed();

    }
    private void SetBasicSpeed()
    {
        speedDef = basicSpeed;
        rotationSpeedDef = basicRotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveSelectGunCharacter();
        PlayerJump();
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }
    }

    private void PlayerMoveSelectGunCharacter()
    {
        if (GunKind == GunKind.None)
        {
            anim.SetBool("isGun", false);
            StopAimingTrigger();
            PlayerCheckIsSprint("speed");
            PlayerMove(speedDef, rotationSpeedDef, "speed");
            PlayerMoveAnimation(speedAnimationDef, rotationAnimationDef, isWalkingDef, "isRunBack", "isLeftTurn", "isRightTurn");
        }
        else if (GunKind == GunKind.Rifle)
        {
            anim.SetBool("isGun", true);
            PlayerAimGunStanding();
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
            //Start Aiming
            StartAimingTrigger();
        }
        if (Input.GetKeyUp(aimGun))
        {
            //Stop Aiming
            StopAimingTrigger();
        }
    }
    private void StartAimingTrigger()
    {
        anim.SetBool("isAiming", true);
        boneAimingController.SetStartIsAimingServer();
        cameraControllerPlayer.ChangePivotCameraRifle();
        GetComponent<FightController>().StartAiming();
    }
    private void StopAimingTrigger()
    {
        anim.SetBool("isAiming", false);
        boneAimingController.SetStopIsAimingServer();
        cameraControllerPlayer.ChangePivotCameraRifleDefault();
        GetComponent<FightController>().StopAiming();
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
    private void PlayerJump()
    {
        if (Input.GetKeyDown(jumpKey) && isGround)
        {
           // Debug.LogWarning("Jump");
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
        if (isGround)
        {
            anim.SetBool("isJumping", false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            isGround = false;
        }
    }
}
