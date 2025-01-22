using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference movement; //Captura el movimiento base
    [SerializeField] private InputActionReference sprint;   //Captura si el jugador esprinta
    [SerializeField] private InputActionReference jump; //Captura si el jugador salta
    [SerializeField] private InputActionReference crouch;   //Captura si el jugador se agacha
    [SerializeField] private InputActionReference look; //Capturar el movimiento del raton

    [Header("Camera")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float lookSpeed = 1f;  //Sensibilidad de la camara
    [SerializeField] private float lookXLimit = 45f;    //Limite para no oasarse por arriba o por abajo

    [Header("Player consts")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float defaultHeight = 2f;
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float crouchSpeed = 3f;

    [Header("Statics")]
    private Vector3 moveDirection = Vector3.zero;   //Input 3D de las fuerzas
    private Vector2 moveInput = Vector2.zero;   //input 2D de las fuerzas
    private float rotationX = 0;    
    private CharacterController characterController;
    private bool canMove = true;
    private bool crouched = false;

    Animator animator;
    float curSpeedX;
    float curSpeedY;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        movement.action.Enable();
        sprint.action.Enable();
        jump.action.Enable();
        crouch.action.Enable();
        look.action.Enable();

        movement.action.performed += onMove;
        movement.action.started += onMove;
        movement.action.canceled += onMove;

        jump.action.performed += onJump;
        crouch.action.performed += onCrouch;
        crouch.action.canceled += onCrouch;
    }

    private void OnDisable()
    {
        movement.action.Disable();
        sprint.action.Disable();
        jump.action.Disable();
        crouch.action.Disable();
        look.action.Disable();

        movement.action.performed -= onMove;
        movement.action.started -= onMove;
        movement.action.canceled -= onMove;

        jump.action.performed -= onJump;
        crouch.action.performed -= onCrouch;
        crouch.action.canceled -= onCrouch;
    }

    private void Update()
    {
        characterMovement();

        cameraRotation();

        if (characterController.isGrounded)
        {
            gravity = 0f;  // Desactiva la gravedad cuando el personaje está en el suelo
        }
        else
        {
            gravity = 10f;  // Aplica gravedad cuando no está en el suelo
        }

        animator.SetFloat("VelX", curSpeedY);
        animator.SetFloat("VelY", curSpeedX);
    }

    private void FixedUpdate()
    {
        moveDirection.y -= gravity * Time.deltaTime;  // Aplica la gravedad de manera constante
    }

    private void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();  // Actualiza el movimiento de acuerdo con la entrada
    }

    private void characterMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = sprint.action.ReadValue<float>() > 0.5f;  // Sprint activado si el valor es mayor que 0.5
        curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * moveInput.y : 0;
        curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * moveInput.x : 0;

        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = movementDirectionY;

        characterController.Move(moveDirection * Time.deltaTime);  // Aplica el movimiento al personaje
    }

    private void cameraRotation()
    {
        Vector2 lookInput = look.action.ReadValue<Vector2>();  // Captura la entrada del ratón

        rotationX += -lookInput.y * lookSpeed;  // Rotación de la cámara en el eje X (arriba/abajo)
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);  // Limita el rango de movimiento de la cámara
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        transform.rotation *= Quaternion.Euler(0, lookInput.x * lookSpeed, 0);  // Rotación del personaje en el eje Y (izquierda/derecha)
    }

    private void onJump(InputAction.CallbackContext context)
    {
        if (characterController.isGrounded)  // Solo permitir salto si está en el suelo
        {
            moveDirection.y = jumpPower;
        }
    }

    private void onCrouch(InputAction.CallbackContext context)
    {
        if (crouched)
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }
        else
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }

        crouched = !crouched;

    }
}



















//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
//public class PlayerMovement : MonoBehaviour
//{
//    public Camera playerCamera;
//    public float walkSpeed = 6f;
//    public float runSpeed = 12f;
//    public float jumpPower = 7f;
//    public float gravity = 10f;
//    public float lookSpeed = 2f;
//    public float lookXLimit = 45f;
//    public float defaultHeight = 2f;
//    public float crouchHeight = 1f;
//    public float crouchSpeed = 3f;

//    private Vector3 moveDirection = Vector3.zero;
//    private float rotationX = 0;
//    private CharacterController characterController;

//    Animator animator;

//    private bool canMove = true;

//    void Awake()
//    {
//        characterController = GetComponent<CharacterController>();
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;

//        animator = GetComponent<Animator>();
//    }

//    void Update()
//    {
//        Vector3 forward = transform.TransformDirection(Vector3.forward);
//        Vector3 right = transform.TransformDirection(Vector3.right);

//        bool isRunning = Input.GetKey(KeyCode.LeftShift);
//        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
//        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
//        float movementDirectionY = moveDirection.y;
//        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

//        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
//        {
//            moveDirection.y = jumpPower;
//        }
//        else
//        {
//            moveDirection.y = movementDirectionY;
//        }

//        if (!characterController.isGrounded)
//        {
//            moveDirection.y -= gravity * Time.deltaTime;
//        }

//        if (Input.GetKey(KeyCode.R) && canMove)
//        {
//            characterController.height = crouchHeight;
//            walkSpeed = crouchSpeed;
//            runSpeed = crouchSpeed;

//        }
//        else
//        {
//            characterController.height = defaultHeight;
//            walkSpeed = 6f;
//            runSpeed = 12f;
//        }

//        characterController.Move(moveDirection * Time.deltaTime);

//        if (canMove)
//        {
//            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
//            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
//            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
//            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
//        }
//    }
//}