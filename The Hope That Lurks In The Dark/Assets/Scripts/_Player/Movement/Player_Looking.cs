using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player_Looking : MonoBehaviour
{
    #region Assignments
    Player_Main player_Main; //Player main script//
    GameControls gameControls; //Input Actions//
    Controls_Settings controls_Settings; //Controll settings to get look sensitivity//

    void Awake()
    {
        player_Main = GetComponent<Player_Main>();
        gameControls = new GameControls();
        controls_Settings = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Controls_Settings>();
    }

    #endregion Assignments

    #region Variables
    [Header("Player's Head")]
    [SerializeField] GameObject cameraHolder;

    [Header("Max Look Angles")]
    [SerializeField] float maxLookAngle_up;
    [SerializeField] float maxLookAngle_down;

    [Header("Look Input")]
    [SerializeField] Vector2 lookInput;

    //Final calculation of Looking//
    float horizontalLook;
    float verticalLook;

    #endregion Variables

    void Update()
    {
        LookInputUpdate();

        if (player_Main.canLook) //If player Can look//
        {
            if (player_Main.playerMainState == Player_Main.PlayerMainStates.Default) //If player Main State is default
            {
                Looking_Default();
            }
        }
        else
        {
            return;
        }

    }

    #region Looking Functions
    void LookInputUpdate()
    {
        lookInput = gameControls.Player.Looking.ReadValue<Vector2>();

        #region Check if player is looking
        if (lookInput != Vector2.zero) //If player is looking around//
        {
            player_Main.playerInputsLooking = true;
        }
        else //When not
        {
            player_Main.playerInputsLooking = false;
        }

        #endregion Check if player is looking
    }

    void Looking_Default()
    {
        #region Horizontal look
        //Calculation//
        horizontalLook = lookInput.x * (controls_Settings.lookSensitivity_x_actual / 10);


        //Execution//
        transform.Rotate(0, horizontalLook, 0, Space.World);

        #endregion Horizontal look

        #region Vertical look
        //Calculation & reverse//
        if (!controls_Settings.inverseYAxis_actual) //When Inverse y axis is off//
        {
            verticalLook -= lookInput.y * (controls_Settings.lookSensitivity_y_actual / 10);
        }
        else //When is on//
        {
            verticalLook -= -lookInput.y * (controls_Settings.lookSensitivity_y_actual / 10);
        }

        //Max Angle Set//
        verticalLook = Mathf.Clamp(verticalLook, -maxLookAngle_down, maxLookAngle_up);

        //Execution//
        cameraHolder.transform.localRotation = Quaternion.Euler(verticalLook, 0, 0);

        #endregion Vertical look
    }

    #endregion Looking Functions

    #region OnDisable, OnEnable
    void OnEnable()
    {
        gameControls.Enable();
    }
    void OnDisable()
    {
        gameControls.Disable();
    }
    #endregion OnDisable, OnEnable
}
