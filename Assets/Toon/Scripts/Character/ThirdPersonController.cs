using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
// using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [Header("References")]

    public Transform cameraTransform;
    public Animator animator;
    public Rig aimrig;
    [SerializeField] private FloatingJoystick floatingJoystick;

    // PlayerInput playerInput;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;

    private float horizontal = 0;
    private float vertical = 0;
    private float turnSmoothVelocity;





    // //InputAction
    // private InputAction moveaction;
    // private InputAction lookaction;
    // private InputAction Aimaction;
    // private InputAction jumpaction;

    private CharacterController characterController;

    [Header("Aim Settings")]
    // public bool aim = false; // Determines whether the character should aim
    public float aimRotationSpeed = 10f; // Speed of rotation while aiming
    [HideInInspector] public float rigWeight;

    private void Start()
    {
        if (!GameManager.instance.IsAndroid)
        {
            Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen
            Cursor.visible = false; // Hides the cursor
        }

        // playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();


        // moveaction = playerInput.actions["Move"];
        // lookaction = playerInput.actions["Look"];
        // jumpaction = playerInput.actions["Jump"];
        // Aimaction = playerInput.actions["Aim"];
    }

    private void Update()
    {
        HandleMovement();
        HandleAim();
    }

    private void HandleMovement()
    {


        // Get joystick input
        if (GameManager.instance.IsAndroid)
        {

           

            horizontal = floatingJoystick.Horizontal;
            vertical = floatingJoystick.Vertical;



            if (horizontal == 0 && vertical == 0f)
            {
                print("1");
                animator.SetFloat("Movement", 0f);
            }

            else
            {
                print("3");
                animator.SetFloat("Movement", .25f);
            }
        }
        else
        {

horizontal= Input.GetAxis("Horizontal");
        vertical  = Input.GetAxis("Vertical");
             
            if (horizontal == 0 && vertical == 0)
            {
                animator.SetFloat("Movement", 0);
            }
            else
            {
                if (!GameManager.instance.IsAiming)
                {
                    animator.SetFloat("Movement", 0.25f);
                }
                else
                {

                    if (vertical > 0)
                    {
                        animator.SetFloat("Movement", 0.25f);
                    }
                    else if (vertical < 0)
                    {
                        animator.SetFloat("Movement", 1f);
                    }
                    else if (horizontal < 0)
                    {
                        animator.SetFloat("Movement", 0.5f);
                    }
                    else if (horizontal > 0)
                    {
                        animator.SetFloat("Movement", 0.75f);
                    }
                }
            }


        }

        // Movement logic when not aiming

        if (!GameManager.instance.IsAiming)
        {
            // Regular movement (non-aiming mode)
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            // Only move if there's input
            if (direction.magnitude >= 0.1f)
            {
                // Calculate the target angle the player should face
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                // Rotate the player
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                // Move the player
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
            }
        }
        else
        {
            // Movement when aiming
            Vector3 camForward = cameraTransform.forward; // Camera's forward direction
            Vector3 camRight = cameraTransform.right; // Camera's right direction

            // Flatten the camera directions to ignore vertical movement
            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            // Get the movement direction relative to the camera's orientation
            Vector3 direction = camForward * vertical + camRight * horizontal;

            // Move the player if there's input
            if (direction.magnitude >= 0.1f)
            {
                // Rotate the character to face the same direction as the camera
                transform.rotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f);

                // Move the player in the direction relative to the camera
                characterController.Move(direction.normalized * speed * Time.deltaTime);
            }
        }


    }

    private void HandleAim()
    {

        if (GameManager.instance.IsAiming )
        {
            // GameManager.instance.AimScript.aim();

            // Rotate the player to match the camera's Y rotation
            Quaternion targetRotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, aimRotationSpeed * Time.deltaTime);


        }
        


        

        animator.SetBool("IsAiming", GameManager.instance.IsAiming);

        aimrig.weight = Mathf.Lerp(aimrig.weight, rigWeight, Time.deltaTime * 20);
    }
}



