using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player_Movement_Default : MonoBehaviour
{
    #region Assignments

    CharacterController playerController;
    Player_Main player_Main;
    GameControls gameControls;
    Player_GroundChecks player_GroundChecks;


    void Awake()
    {
        playerController = GetComponent<CharacterController>();
        player_Main = GetComponent<Player_Main>();
        gameControls = new GameControls();
        player_GroundChecks = GetComponentInChildren<Player_GroundChecks>();

        #region Input Actions
        //Running//
        gameControls.Player.Run.performed += ctx => InputController_Run(true);
        gameControls.Player.Run.canceled += ctx => InputController_Run(false);

        //Crouch//
        gameControls.Player.Crouch.performed += ctx => InputController_Crouch();
        #endregion Input Actions
    }
    #endregion Assignments

    #region Variables

    #region Speeds and Accelerations
    [Header("Stoping")]
    [SerializeField] float stopAxisAccelerationSpeed; //Stoping speed on single acceleration//
    [SerializeField] float stopAccelerationSpeed; //Stoping on all Acceleration//

    [Header("Sliding")]
    [SerializeField] float slideSpeed;

    [Header("Walking")]
    [SerializeField] float walkSpeed;
    [SerializeField] float walkAccelerationSpeed;

    [Header("Running")]
    [SerializeField] float runSpeed;
    [SerializeField] float runAccelerationSpeed;

    [Header("Sneaking")]
    [SerializeField] float crouchSpeed;
    [SerializeField] float crouchAccelerationSpeed;

    [Header("On Air")]
    [SerializeField] float onAirSpeed;
    [SerializeField] float onAirAccelerationSpeed;

    [Header("Factors")]
    [SerializeField] float backwardMovementFactor;
    [SerializeField] float sidewaysMovementFactor;

    [Header("Landing")]
    [SerializeField] bool isLandedOnGround;
    [SerializeField] float onGroundVerticalForce;
    [SerializeField] float groundingSpeed;

    [Header("Gravitation Force")]
    [SerializeField] float fallingSpeed;

    [Header("Player Speed")]
    public float DEBUG_Player_Speed;

    #endregion Speeds and Accelerations

    #region Input 
    [Header("Movement Input")]
    [SerializeField] Vector2 movementInputVector; //Vector of basic movement Inputs//
    [SerializeField] bool isAxisInput_Y;
    [SerializeField] bool isAxisInput_X;
    [SerializeField] bool isMovingBackward;

    #endregion Input

    #region Movement Values
    [Header("Movement Vectors")]
    [SerializeField] Vector2 horizontalMoveVector;
    [SerializeField] Vector2 desiredHorizontalSpeeds;

    public float verticalVelocity;

    [SerializeField] Vector3 moveVector;
    #endregion Movement Values

    #endregion Variables

    void Update()
    {
        if (player_Main.playerMainState == Player_Main.PlayerMainStates.Default)
        {
            InputController_Movement(); //Updates movement inputs//

            MovementController(); //Controls movement mode, and switch to good calculating function//

            OnSlopeStateCheck(); //Check if player will slide//

            MovementExecutionController(); //Execution movement Controller//
        }
        else
        {
            player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Disabled;
            return;
        }

        #region Crouch Disabler
        if (!player_Main.canCrouch) //If player cannot crouch, then resets state
        {
            InputController_Crouch();
        }
        #endregion Crouch Disabler
    }

    #region Movement Functions
    void MovementController()
    {
        DEBUG_Player_Speed = playerController.velocity.magnitude; //Debug player speed//

        //State Execution Controller//
        switch (player_Main.playerDefaultState)
        {
            //On Airs States//
            case Player_Main.PlayerDefaultStates.OnAir:
            case Player_Main.PlayerDefaultStates.OnAir_Falling:
                Movement_OnAir();
                VerticalController_OnAir();
                return;

            //Walk State//
            case Player_Main.PlayerDefaultStates.Walking:
                Movement_Walk();
                VerticalController_Grounded();
                return;

            //Run State//
            case Player_Main.PlayerDefaultStates.Running:
                Movement_Run();
                VerticalController_Grounded();
                return;

            //Crouch State//
            case Player_Main.PlayerDefaultStates.Sneaking_Walk:
                Movement_Crouch();
                VerticalController_Grounded();
                return;

            //When Idle states, or other unscripted, then Stop speeds//
            case Player_Main.PlayerDefaultStates.Idle:
            case Player_Main.PlayerDefaultStates.Sneaking_Idle:
            default:
                Movement_Stop();
                VerticalController_Grounded();
                return;
        }
    }

    #region Horizontal Movement Controllers
    void Movement_Stop()
    {
        #region Axis Stop

        if ((horizontalMoveVector.x > 0 && horizontalMoveVector.x < 0.01f) || (horizontalMoveVector.x < 0 && horizontalMoveVector.x > -0.01f)) //Float Calculation Optimization, no need to calculate something so small//
        {
            horizontalMoveVector.x = 0;
        }
        else
        {
            horizontalMoveVector.x = Mathf.Lerp(horizontalMoveVector.x, 0f, stopAccelerationSpeed * Time.deltaTime);
        }

        if ((horizontalMoveVector.y > 0 && horizontalMoveVector.y < 0.01f) || (horizontalMoveVector.y < 0 && horizontalMoveVector.y > -0.01f))
        {
            horizontalMoveVector.y = 0;
        }
        else
        {
            horizontalMoveVector.y = Mathf.Lerp(horizontalMoveVector.y, 0f, stopAccelerationSpeed * Time.deltaTime);
        }

        #endregion Axis stop

        horizontalMoveVector = Vector2.Lerp(horizontalMoveVector, Vector3.zero, stopAccelerationSpeed * Time.deltaTime);
    }

    void Movement_Walk()
    {

        if (!isMovingBackward) //When moving forward
        {
            desiredHorizontalSpeeds = new Vector2(movementInputVector.x * walkSpeed * sidewaysMovementFactor, movementInputVector.y * walkSpeed);
        }
        else //When moving bakward
        {
            desiredHorizontalSpeeds = new Vector2(movementInputVector.x * walkSpeed * sidewaysMovementFactor, movementInputVector.y * walkSpeed * backwardMovementFactor);
        }

        horizontalMoveVector = Vector2.Lerp(horizontalMoveVector, desiredHorizontalSpeeds, walkAccelerationSpeed * Time.deltaTime);
    }

    void Movement_Run()
    {
        if (!isMovingBackward) //When moving forward
        {
            desiredHorizontalSpeeds = new Vector2(movementInputVector.x * runSpeed * sidewaysMovementFactor, movementInputVector.y * runSpeed);

        }
        else //When moving bakward
        {
            desiredHorizontalSpeeds = new Vector2(movementInputVector.x * runSpeed * sidewaysMovementFactor, movementInputVector.y * runSpeed * backwardMovementFactor);
        }

        horizontalMoveVector = Vector2.Lerp(horizontalMoveVector, desiredHorizontalSpeeds, runAccelerationSpeed * Time.deltaTime);
    }

    void Movement_Crouch()
    {
        if (!isMovingBackward) //When moving forward
        {
            desiredHorizontalSpeeds = new Vector2(movementInputVector.x * crouchSpeed * sidewaysMovementFactor, movementInputVector.y * crouchSpeed);

        }
        else //When moving bakward
        {
            desiredHorizontalSpeeds = new Vector2(movementInputVector.x * crouchSpeed * sidewaysMovementFactor, movementInputVector.y * crouchSpeed * backwardMovementFactor);
        }

        horizontalMoveVector = Vector2.Lerp(horizontalMoveVector, desiredHorizontalSpeeds, crouchAccelerationSpeed * Time.deltaTime);
    }

    void Movement_OnAir()
    {
        if (!isMovingBackward) //When moving forward
        {
            desiredHorizontalSpeeds = new Vector2(movementInputVector.x * onAirSpeed * sidewaysMovementFactor, movementInputVector.y * onAirSpeed);
        }
        else //When moving bakward
        {
            desiredHorizontalSpeeds = new Vector2(movementInputVector.x * onAirSpeed * sidewaysMovementFactor, movementInputVector.y * onAirSpeed * backwardMovementFactor);
        }

        horizontalMoveVector = Vector2.Lerp(horizontalMoveVector, desiredHorizontalSpeeds, onAirAccelerationSpeed * Time.deltaTime);
    }

    #endregion Horizontal Movement Controllers

    #region Vertical Movement Controllers
    void VerticalController_Grounded()
    {
        #region Landing
        if (isLandedOnGround == false)
        {
            Debug.Log("Landed On Ground with force of: " + verticalVelocity);

            isLandedOnGround = true;
        }
        #endregion Landing


        verticalVelocity = Mathf.Lerp(verticalVelocity, -onGroundVerticalForce, groundingSpeed * Time.deltaTime);
    }

    void VerticalController_OnAir()
    {
        #region Landing
        if (isLandedOnGround == true)
        {
            isLandedOnGround = false;
            verticalVelocity = -2f;
        }
        #endregion Landing

        verticalVelocity += Physics.gravity.y * fallingSpeed * Time.deltaTime;
    }

    #endregion Vertical Movement Controllers

    #endregion Movement Functions

    #region Stopping Functions

    void SingleAxis_Stopping() //Stopping on single axis//
    {
        if (!isAxisInput_X)
        {
            if ((horizontalMoveVector.x > 0 && horizontalMoveVector.x < 0.01f) || (horizontalMoveVector.x < 0 && horizontalMoveVector.x > -0.01f)) //Float Calculation Optimization, no need to calculate something so small//
            {
                horizontalMoveVector.x = 0;
            }
            else
            {
                horizontalMoveVector.x = Mathf.Lerp(horizontalMoveVector.x, 0f, stopAxisAccelerationSpeed * Time.deltaTime);
            }
        }

        if (!isAxisInput_Y)
        {
            if ((horizontalMoveVector.y > 0 && horizontalMoveVector.y < 0.01f) || (horizontalMoveVector.y < 0 && horizontalMoveVector.y > -0.01f))
            {
                horizontalMoveVector.y = 0;
            }
            else
            {
                horizontalMoveVector.y = Mathf.Lerp(horizontalMoveVector.y, 0f, stopAxisAccelerationSpeed * Time.deltaTime);
            }

        }
    }
    #endregion Stopping functions

    #region Execute Movement Functions
    //Movement Execution Controller by condition//
    void MovementExecutionController()
    {

        if (player_Main.playerIsSliding)
        {
            MovementExecution_Sliding();
        }
        else
        {
            MovementExecution_Default();
        }


    }

    void MovementExecution_Sliding()
    {
        Vector3 inputValue = new Vector3(movementInputVector.x, 0, movementInputVector.y);

        Vector3 slideHitVector = player_GroundChecks.slopeHit.normal;
        moveVector = new Vector3(slideHitVector.x, -slideHitVector.y, slideHitVector.z);
        Vector3.OrthoNormalize(ref slideHitVector, ref moveVector);
        moveVector *= slideSpeed;
        playerController.Move((transform.rotation * inputValue + moveVector) * Time.deltaTime);
    }

    void MovementExecution_Default()
    {
        if (player_Main.playerInputsMovement)
        {
            SingleAxis_Stopping();
        }

        moveVector = new Vector3(horizontalMoveVector.x, verticalVelocity, horizontalMoveVector.y);
        playerController.Move(transform.rotation * moveVector * Time.deltaTime);
    }

    #endregion Execute Movement Functions

    #region Input Functions
    void InputController_Movement()
    {
        movementInputVector = gameControls.Player.Movement.ReadValue<Vector2>(); //Checks value of inputs//

        #region Is Inputing Movement Check
        //Checking if player is inputing movement
        if (movementInputVector != Vector2.zero)
        {
            player_Main.playerInputsMovement = true;
        }
        else
        {
            player_Main.playerInputsMovement = false;
        }
        #endregion Is Inputing Movement

        #region Check which axises is inputed
        //Check inputing axises//
        if (movementInputVector.y != 0)
        {
            isAxisInput_Y = true;
        }
        else
        {
            isAxisInput_Y = false;
        }

        if (movementInputVector.x != 0)
        {
            isAxisInput_X = true;
        }
        else
        {
            isAxisInput_X = false;
        }

        #endregion Check which axises is inputed

        #region Is Moving Backward
        if (movementInputVector.y < 0)
        {
            isMovingBackward = true;
        }
        else
        {
            isMovingBackward = false;
        }
        #endregion Is Moving Backward
    }

    void InputController_Run(bool runInput) //Run Input Controller//
    {
        if (player_Main.canRun) //When can run
        {
            if (runInput) //When Inputing Run, and changing position//
            {
                player_Main.playerIsRunning = true;
            }
            else
            {
                player_Main.playerIsRunning = false;
            }
        }
        else
        {
            player_Main.playerIsRunning = false;
        }
    }

    void InputController_Crouch() //Crouch Input Controller
    {
        if (player_Main.canCrouch) //When can crouch
        {
            player_Main.playerIsCrouching = !player_Main.playerIsCrouching;
        }
        else
        {
            player_Main.playerIsCrouching = false;
        }
    }

    #endregion Input Functions

    #region Check Functions
    void OnSlopeStateCheck()
    {
        if (player_Main.playerIsOnSteepSlope && player_Main.playerIsGrounded)
        {
            player_Main.playerIsSliding = true;
        }
        else
        {
            player_Main.playerIsSliding = false;
        }
    }
    #endregion Check Functions

    #region OnEnable, On Disable
    void OnEnable()
    {
        gameControls.Enable();
    }

    void OnDisable()
    {
        gameControls.Disable();
    }

    #endregion OnEnable, On Disable


}
