using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{

    private CharacterController characterController;
    public WeaponController wc;

    public float walkSpeed = 6f; //Move speed
    public float runSpeed = 10f;
    private float speed;
    
    public bool isRun;
    public bool isJump;
    public bool isWalk;

    public float jumpForce = 3f;
    public Vector3 velocity; //y axis
    public float gravity = -40f;
    public bool isGround;

    public float slopeForce = 6.0f;// force added when walking on slop
    public float slopeForceRayLength = 2.0f;
    
    private Transform groundCheck;
    private float groundDistance = 0.1f;
    public LayerMask groundMask;
    
    
    public Vector3 moveDirection;// move dir
    private Camera fpsCamera;
    [Header("Key bind setting")]
    [Tooltip("Running")]public KeyCode runInputName;//bind key to run :shift
    [Tooltip("Running")] public string jumpName = "Jump";

    [Header("Sound")] 
    [SerializeField]private AudioSource audioSource;
    public AudioClip walkingSound;
    public AudioClip runningSound;
    

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        runInputName = KeyCode.LeftShift;
        fpsCamera = GetComponentInChildren<Camera>();
        groundCheck = GameObject.Find("Player/CheckGround").GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();

        sit = new Vector3(0, 1.4f, 0);
        stand = new Vector3(0, 1.6f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        Move();
    }

    public Vector3 sit;
    public Vector3 stand;
    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float j = Input.GetAxis("Jump");
        isRun=Input.GetKey(runInputName);
        isWalk = (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0) ? true : false;
        if (isRun&&(h != 0 || v != 0))
        {
            speed = runSpeed;
            fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, 95, 0.7f);
             
        }
        else if (isWalk && !wc.isAiming)
        {
            speed = walkSpeed;
            fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, 90, 0.1f);
        } 
        
        //sit
        if (Input.GetButton("Fire1"))
        {
            fpsCamera.transform.localPosition = Vector3.Lerp(stand,sit,1000);
            speed = walkSpeed/2;
        }
        else
        {
            fpsCamera.transform.localPosition = Vector3.Lerp(sit,stand,Time.deltaTime*1000);
            speed = walkSpeed;
        }

        moveDirection =( transform.right * h+ transform.forward * v).normalized ;
        characterController.Move(moveDirection*speed *Time.deltaTime);

        if (isGround==false)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        characterController.Move(velocity*Time.deltaTime);
        Jump();
        if (OnSlop())
        {
            //give a downforce
            characterController.Move(Vector3.down*characterController.height/2*slopeForce*Time.deltaTime);
        }
        PlayerFootSound();
    }

    private void Jump()
    {
        isJump = Input.GetButtonDown(jumpName);
        if (isJump && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

    }

    private void CheckGround()
    {
         isGround =  Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);
         if (isGround && velocity.y<=0)
         {
             velocity.y = -2f;
         }
    }
//Suspended in mid-air when going downhill, give player a downforce
    private bool OnSlop()
    {
        if (isJump) {return false;}
        // use physic ray the check if slop or plane
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit,characterController.height/2*slopeForceRayLength);
        if (hit.normal!=Vector3.up)
        {
            return true;
        }
        return false;
    }

    private void PlayerFootSound()
    {
        if (isGround && moveDirection.sqrMagnitude > 0.9f)
        {
            audioSource.clip = isRun ? runningSound : walkingSound;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
        
    }
}
