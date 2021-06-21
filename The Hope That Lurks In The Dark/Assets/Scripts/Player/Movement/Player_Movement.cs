using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player_Movement : MonoBehaviour
{
    #region Assignments
    CharacterController playerController;
    Player_Main player_Main;
    GameControls gameControls;
    Player_GroundCheck player_GroundCheck;

    void Awake()
    {
        playerController = GetComponent<CharacterController>();
        player_Main = GetComponent<Player_Main>();
        gameControls = new GameControls();
        player_GroundCheck = GetComponent<Player_GroundCheck>();

        #region Input Actions
        gameControls.Player.Run.performed += ctx => player_Main.playerIsRunning = true;
        gameControls.Player.Run.canceled += ctx => player_Main.playerIsRunning = false;

        #endregion Input Actions
    }
    #endregion Assignments

    #region Variables
    [Header("Walking")]
    [SerializeField] float walkSpeed;

    [Header("Running")]
    [SerializeField] float runSpeed;

    [Header("Crouching")]
    [SerializeField] float crouchWalkSpeed;

    [Header("Stand From Crouch")]
    [SerializeField] bool canStandUp;

    [Header("Jumping")]
    [SerializeField] float jumpCooldown;

    [Header("On Air")]
    [SerializeField] float onAirSpeed;

    [Header("Falling")]
    public float fallingStartValue;
    [SerializeField] float velocityBeforeConstantFallingForce;
    [Space]
    [SerializeField] float constantFallingForce;

    [Header("Slope")]
    [SerializeField] float slideSpeed;

    [Header("Landing")]
    [SerializeField] bool landedOnGround = true;

    [Header("Movement Factors")]
    public float moveBackwardFactor;
    public float moveSidewaysFactor;

    [Header("Gravity Forces")]
    [SerializeField] float groundingForceOnGround;
    [SerializeField] float gravity;

    [Header("Vertical Force Reset")]
    [SerializeField] bool verticalForceReset;

    [Header("Player Mass")]
    [SerializeField] float playerMass;

    [Header("Physic Interactions")]
    [SerializeField] float playerPushPower;

    [Header("Movement Values")]
    public float verticalMovementValue; //Vertical Movement Value//
    [SerializeField] Vector2 horizontalMovementValue; //Horizontal Movement values//
    [Space]
    [SerializeField] Vector3 MovementValue; //Final Movement value//
    float forwardBackwardMovementValue; //For movement backward

    [Header("Movement Input")]
    [SerializeField] Vector2 movementInput; //Game Controls Inputs//

    [Header("Last frame position")]
    Vector3 lastPosition;


    #endregion Variables

    void Start()
    {
        #region Start Setup
        gravity = Physics.gravity.y; //Set gravity

        #endregion Start Setup
    }

    void Update()
    {
        //Input Update//
        movementInput = gameControls.Player.Movement.ReadValue<Vector2>();


        if (player_Main.playerMainState == Player_Main.PlayerMainStates.DefaultMovement) //When Default Movement//
        {
            DefaultMovement();
        }


        whenGrounded(); //All function that need to be done when player is grounded//
        IsPlayerInputingMovement(); //Checks if player inputing movement//
        IsPlayerChangingPosition(); //Checks if player is changig position//
    }

    #region Default Movement
    void DefaultMovement()
    {
        DefaultMovement_Vetical(); //Vertical Calculation
        DefaultMovement_Horizontal(); //Horizontal Calculation
        MovementExecution(); //Movement Execution//
    }

    void DefaultMovement_Vetical()
    {
        if (player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.Jumping)
        {

        }
        else if (player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.OnAir)
        {
            verticalMovementValue += gravity * (playerMass / 10) * Time.deltaTime;
        }
        else if (player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.OnAir_Falling)
        {
            if (!verticalForceReset)
            {
                verticalMovementValue = 0;
                verticalForceReset = true;
            }

            if (verticalMovementValue <= -velocityBeforeConstantFallingForce) //When start constant fall velocity//
            {
                verticalMovementValue += -constantFallingForce * Time.deltaTime;
            }
            else //Default fall//
            {
                verticalMovementValue += gravity * (playerMass / 10) * Time.deltaTime;
            }
        }
        else if (player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.Idle || player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.Walking || player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.Running)
        {
            verticalMovementValue = -groundingForceOnGround;
        }
        else if (player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.Disabled)
        {
            verticalMovementValue = 0;
        }
        else
        {

        }
    }

    void DefaultMovement_Horizontal()
    {
        if (player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.OnAir || player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.OnAir_Falling) //When on air//
        {
            if (movementInput.y > 0) //Move Forward//
            {
                forwardBackwardMovementValue = movementInput.y * onAirSpeed;
            }
            else if (movementInput.y < 0) //Backward movement//
            {
                forwardBackwardMovementValue = movementInput.y * onAirSpeed * moveBackwardFactor;
            }
            else if (movementInput.y == 0)
            {
                forwardBackwardMovementValue = 0;
            }

            horizontalMovementValue = new Vector2(movementInput.x * onAirSpeed * moveSidewaysFactor, forwardBackwardMovementValue);
        }
        else if (player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.Idle || player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.Walking)
        {
            if (movementInput.y > 0) //Move Forward//
            {
                forwardBackwardMovementValue = movementInput.y * walkSpeed;
            }
            else if (movementInput.y < 0) //Backward movement//
            {
                forwardBackwardMovementValue = movementInput.y * walkSpeed * moveBackwardFactor;
            }
            else if (movementInput.y == 0)
            {
                forwardBackwardMovementValue = 0;
            }

            horizontalMovementValue = new Vector2(movementInput.x * walkSpeed * moveSidewaysFactor, forwardBackwardMovementValue);
        }
        else if (player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.Running)
        {
            if (movementInput.y > 0) //Move Forward//
            {
                forwardBackwardMovementValue = movementInput.y * runSpeed;
            }
            else if (movementInput.y < 0) //Backward movement//
            {
                forwardBackwardMovementValue = movementInput.y * runSpeed * moveBackwardFactor;
            }
            else if (movementInput.y == 0)
            {
                forwardBackwardMovementValue = 0;
            }

            horizontalMovementValue = new Vector2(movementInput.x * runSpeed * moveSidewaysFactor, forwardBackwardMovementValue);
        }
        else if (player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.Crouch_Idle || player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.Crouch_Walking)
        {
            if (movementInput.y > 0) //Move Forward//
            {
                forwardBackwardMovementValue = movementInput.y * crouchWalkSpeed;
            }
            else if (movementInput.y < 0) //Backward movement//
            {
                forwardBackwardMovementValue = movementInput.y * crouchWalkSpeed * moveBackwardFactor;
            }
            else if (movementInput.y == 0)
            {
                forwardBackwardMovementValue = 0;
            }

            horizontalMovementValue = new Vector2(movementInput.x * crouchWalkSpeed * moveSidewaysFactor, forwardBackwardMovementValue);
        }

    }

    #endregion Default Movement


    #region Movement Execution
    void MovementExecution()
    {
        #region Default Movement
        if (player_Main.playerMainState == Player_Main.PlayerMainStates.DefaultMovement && player_Main.canMove) //Defloat Movement
        {
            if (player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.SlidingFromEdge) //When Sliding from edge//
            {
                Vector3 slideHitVector = player_GroundCheck.slopeHit_Ray.normal;
                MovementValue = new Vector3(slideHitVector.x, -slideHitVector.y, slideHitVector.z);
                Vector3.OrthoNormalize(ref slideHitVector, ref MovementValue);
                MovementValue *= slideSpeed;
                playerController.Move(MovementValue * Time.deltaTime);
            }
            else if (player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.OnAir && player_Main.playerDefaultState == Player_Main.PlayerDefaultStates.OnAir_Falling) //When On Air//
            {
                MovementValue = new Vector3(horizontalMovementValue.x, verticalMovementValue, horizontalMovementValue.y);
                playerController.Move(transform.rotation * MovementValue * Time.deltaTime);
            }
            else //default movement//
            {
                MovementValue = new Vector3(horizontalMovementValue.x, verticalMovementValue, horizontalMovementValue.y);
                playerController.Move(transform.rotation * MovementValue * Time.deltaTime);
            }
        }
        #endregion Default Movement

    }

    #endregion Movement Execution

    #region Special Functions

    #region Checking Functions
    void IsPlayerInputingMovement()
    {
        if (movementInput.x != 0 || movementInput.y != 0)
        {
            player_Main.playerIsMoving = true;
        }
        else
        {
            player_Main.playerIsMoving = false;
        }
    }
    void IsPlayerChangingPosition()
    {
        if (transform.position != lastPosition)
        {
            player_Main.playerIsChangingPosition = true;
        }
        else
        {

            player_Main.playerIsChangingPosition = false;
        }

        lastPosition = transform.position;
    }
    #endregion Checking Functions

    void whenGrounded()
    {
        if (player_Main.playerIsGrounded)
        {
            if (player_Main.playerMainState == Player_Main.PlayerMainStates.DefaultMovement) //When Default Movement//
            {
                verticalForceReset = false; // Reset Reset Vertivcal force//


            }
        }
    }

    #region Physic with objects

    //When Colides with objects//
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        MoveObject(hit);
    }

    void MoveObject(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        Vector3 force;

        if (body == null || body.isKinematic)
        {
            return;
        }
        else
        {
            if (hit.moveDirection.y < -0.3f)
            {
                force = new Vector3(0, -0.5f, 0) * gravity * (playerMass / 10);
            }
            else
            {

                force = new Vector3(hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z);
            }
            body.AddForce(force * playerPushPower);
        }

    }

    #endregion Physic with objects

    #endregion Special Functions

    #region Debug
    private void OnDrawGizmos()
    {

    }
    #endregion Debug

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
