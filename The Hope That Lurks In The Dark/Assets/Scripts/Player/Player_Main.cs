using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player_Main : MonoBehaviour
{
    #region Assignments

    #endregion Assignments

    #region Variables

    #region Player States

    #region Main State
    //Main State//
    public enum PlayerMainStates { DefaultMovement, SpecialMovement, Hidding, OnCutscene, OnPause, Death };
    [Header("Main State")]
    public PlayerMainStates playerMainState;
    #endregion

    #region Default Movement
    //Default States//
    public enum PlayerDefaultStates { Disabled, Idle, Walking, Running, Jumping, Crouch_Idle, Crouch_Walking, OnAir, OnAir_Falling, SlidingFromEdge };
    [Header("Default State")]
    public PlayerDefaultStates playerDefaultState;
    #endregion

    #region Special Movement
    //Special States//
    public enum PlayerSpecialStates { Disabled, OnLadder, };
    [Header("Special State")]
    public PlayerSpecialStates playerSpecialState;
    #endregion

    #region Hide States
    //On Hide States//
    public enum PlayerHideStates { Disabled, InCloset };
    [Header("Hide State")]
    public PlayerHideStates playerHideState;
    #endregion

    #region Death States
    //Death State//
    public enum PlayerDeathStates { Alive, FallDeath, };
    [Header("Death State")]
    public PlayerDeathStates playerDeathState;
    #endregion

    #region OnCutscene States
    //Cutscene State//
    public enum PlayerCutsceneStates { Disabled, OnCutscene, };
    [Header("Cutscene State")]
    public PlayerCutsceneStates playerCutsceneState;
    #endregion OnCutscene States

    #endregion Player States

    #region Player Parameters
    [Header("Player Health")]
    public float playerSanity;

    [Header("Player Stamina")]
    public float playerStamina;
    #endregion Player Parameters

    #region Player Possibilities
    [Header("Player Possibilities")]
    //Looking//
    public bool canLook;
    [Space]

    //Movement//
    public bool canMove;
    public bool canRun;
    public bool canJump;
    public bool canCrouch;
    [Space]

    //Interaction//
    public bool canInteract;

    #endregion Player Possibilities

    #region Player Cases
    //Movement Cases//
    [Header("Movement Cases")]
    public bool playerIsGrounded; //If player is grounded//
    [Space]
    public bool playerIsMovingHead; //When player is looking around//
    public bool playerIsMoving; //When player is inputing controls//
    public bool playerIsChangingPosition; //When player is changing his position//
    [Space]
    public bool playerIsRunning; //When player is Running//
    public bool playerIsCrouching; //When player is crouching//
    [Space]
    public bool playerIsSliding; //When player is sliding from slope//

    //Pause, Debug, and others//
    [Header("Pause & Debug")]
    //Pause//
    public bool playerIsInPauseMenu;

    //Debug Console//
    public bool playerIsInDebugConsole;

    #endregion Player Cases

    #region Player Objects
    //Player GameObjects//
    [Header("Player Objects")]
    //Head//
    public GameObject playerHead;
    public GameObject cameraHolder;
    [Space]

    //Body//
    public GameObject playerModel;


    #endregion Player Objects

    #endregion Variables
}
