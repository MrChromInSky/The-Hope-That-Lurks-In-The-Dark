using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player_Movement_Test : MonoBehaviour
{

    CharacterController ch;
    GameControls gameControls;

    public Vector2 MoveInput;

    [Header("Movement Speeds")]
    public float runSpeed;
    public float movespeed;

    [Header("Movement Accelerations")]
    public float accelerationSpeedMove;
    public float accelerationStop;

    public bool playerIsGrounded;


    [SerializeField] bool playerIsRunning;


    public float CharControllerVelocity;
    public Vector3 movmementValue;

    [SerializeField] Vector3 finalMove;


    private void Awake()
    {
        gameControls = new GameControls();
        ch = GetComponent<CharacterController>();

        gameControls.Player.Run.performed += ctx => playerIsRunning = true;
        gameControls.Player.Run.canceled += ctx => playerIsRunning = false;
    }

    void Update()
    {
        MoveInput = gameControls.Player.Movement.ReadValue<Vector2>();






        finalMove.y += -8 * Time.deltaTime;

        CharControllerVelocity = ch.velocity.magnitude;

        Movement();
        MovementExecution();
    }


    void Movement()
    {


        if (playerIsRunning && playerIsGrounded)
        {
            movmementValue = Vector3.Slerp(movmementValue, new Vector3(MoveInput.x * runSpeed, -8f, MoveInput.y * runSpeed), accelerationSpeedMove * Time.deltaTime);
        }
        else
        {
            movmementValue = Vector3.Slerp(movmementValue, new Vector3(MoveInput.x * movespeed, -8f, MoveInput.y * movespeed), accelerationSpeedMove * Time.deltaTime);
        }



    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        playerIsGrounded = true;
    }



    void MovementExecution()
    {

        if (playerIsGrounded)
        {
            if (MoveInput.x == 0)
            {
                movmementValue.x = Mathf.Lerp(movmementValue.x, 0f, accelerationStop * Time.deltaTime);

            }

            if (MoveInput.y == 0)
            {
                movmementValue.z = Mathf.Lerp(movmementValue.z, 0f, accelerationStop * Time.deltaTime);
            }

            finalMove = new Vector3(movmementValue.x, movmementValue.y, movmementValue.z);

            ch.Move(transform.rotation * finalMove * Time.deltaTime);
        }
        /* else if(OnJump)
         {
             finalDrag = new Vector3(horizontalSpeeds.x, movmementValue.y, horizontalSpeeds.y);


             ch.Move(finalDrag * Time.deltaTime);
         }*/



    }


    #region OnEnable, OnDisable 
    void OnEnable()
    {
        gameControls.Enable();
    }

    void OnDisable()
    {
        gameControls.Disable();
    }
    #endregion OnEnable, OnDisabe 
}
