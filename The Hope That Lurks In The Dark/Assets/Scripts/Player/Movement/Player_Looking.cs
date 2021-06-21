using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player_Looking : MonoBehaviour
{
    #region Assignments
    Player_Main player_Main;
    GameControls gameControls;
    Controls_Settings controls_Settings;

    void Awake()
    {
        player_Main = GetComponent<Player_Main>();
        gameControls = new GameControls();
        controls_Settings = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Controls_Settings>();
    }
    #endregion Assignements

    #region Variables
    [Header("Look Input")]
    Vector2 lookInput; //Mouse inpit Value//

    [Header("Max Look Angles")]
    [SerializeField] float maxLookAngle_up;
    [SerializeField] float maxLookAngle_down;

    [Header("Looking Values")]
    float lookingHorizontalValue; //Value of horizontal looking//
    float lookingVerticalValue; //Value of vertical looking//
    #endregion Variables

    void Update()
    {
        //Update Controls//
        lookInput = gameControls.Player.Looking.ReadValue<Vector2>();
        #region Looking Checker
        //Checking if player is looking around//
        if (lookInput != Vector2.zero)
        {
            player_Main.playerIsMovingHead = true;
        }
        else
        {
            player_Main.playerIsMovingHead = false;
        }
        #endregion Looking Checker

        if (player_Main.playerMainState == Player_Main.PlayerMainStates.DefaultMovement && player_Main.canLook)
        {
            DefaultLooking();
        }

    }

    #region Default Looking
    void DefaultLooking()
    {
        #region Horizontal Looking
        //Calculate
        lookingHorizontalValue = lookInput.x * (controls_Settings.lookSensitivity_x_actual / 10);

        //Execute
        transform.Rotate(0, lookingHorizontalValue, 0);

        #endregion Horizontal Looking

        #region Vertical Looking

        if (!controls_Settings.inverseYAxis_actual)//If not inversing y axis//
        {
            lookingVerticalValue -= lookInput.y * (controls_Settings.lookSensitivity_y_actual / 10);
        }
        else // If inversing y axis//
        {
            lookingVerticalValue -= -lookInput.y * (controls_Settings.lookSensitivity_y_actual / 10);
        }

        //Max Angles Set//
        lookingVerticalValue = Mathf.Clamp(lookingVerticalValue, -maxLookAngle_down, maxLookAngle_up);

        //Execution//
        player_Main.playerHead.transform.localRotation = Quaternion.Euler(lookingVerticalValue, 0, 0);

        #endregion Vertical Looking
    }


    #endregion Deafult Looking

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
