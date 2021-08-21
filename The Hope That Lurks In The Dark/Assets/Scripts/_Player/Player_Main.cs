using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(CharacterController))]
public class Player_Main : MonoBehaviour
{
    #region Assignments
    CharacterController playerController;

    void Awake()
    {
        playerController = GetComponent<CharacterController>();
    }
    #endregion Assignments

    #region Variables

    #region States

    #region Main States
    //Main States//
    public enum PlayerMainStates { Default, Special, OnPause, Death };
    [Header("Main State")]
    public PlayerMainStates playerMainState;
    #endregion Main States

    #region Default
    //Default Player States//
    public enum PlayerDefaultStates { Disabled, Idle, Walking, Running, Jumping, OnAir, OnAir_Falling, Sneaking_Idle, Sneaking_Walk };
    [Header("Default States")]
    public PlayerDefaultStates playerDefaultState;
    #endregion Default

    #endregion States

    #region Parameters

    #endregion Parameters

    #region Cases
    [Header("Main Cases")]
    public bool playerInputsLooking; //If players moving looking inputs//
    public bool playerInputsMovement; //If player Enters Movement Inputs//
    [Space]
    public bool playerChangingPosition; //If player changes position in-game//
    [Space]

    #region Default Movement Cases
    [Header("Default Movement Cases")]
    public bool playerIsGrounded; //If player is grounded//
    public bool playerIsOnSteepSlope; //If player is on slope that is too steep//
    [Space]
    public bool playerIsRunning; //If player Is Running//
    public bool playerIsCrouching; //If player is Crouching//
    [Space]
    public bool playerIsSliding; //If player is on slope that is too steep//
    [Space]
    public bool playerWillToJump; //If player will to jump//
    public bool playerHitsCeiling; //If player Hits Celling//
    #endregion Default Movement Cases

    #region Special Cases

    #endregion Special Cases

    [Header("Pause & Debug")]
    public bool playerIsInPauseMenu; //Pause Menu//
    public bool playerIsInDebugConsole; //Debug Console//


    #endregion Cases

    #region Possibilities
    [Header("Possibilities")]

    //Looking//
    public bool canLook = true;
    [Space]

    //Movement//
    public bool canMove = true; //If player can change position//
    [Space]

    public bool canWalk = true;
    public bool canRun = true;
    public bool canJump = true;
    public bool canCrouch = true;
    [Space]

    //Interaction//
    public bool canUseInteractiveObjects = true;
    [Space]

    //Physics//
    public bool canPhysicallyInteractWithObjects = true;
    //[Space(2)]

    #endregion Possibilities

    #region Player Objects

    #endregion Player Objects

    #endregion Variables


}


