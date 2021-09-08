using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateController : MonoBehaviour
{
    #region Assignments
    Player_Main player_Main; //Player Main Script//
    Player_Movement_Default player_Movement_Default;
    void Awake()
    {
        player_Main = GetComponent<Player_Main>();
        player_Movement_Default = GetComponent<Player_Movement_Default>();
    }
    #endregion Assignments

    void Update()
    {
        #region Main State Controller

        if (player_Main.playerMainState == Player_Main.PlayerMainStates.Default)
        {
            if (player_Main.canMove)
            {
                DefaultStateController();
            }
            else //If player Cannot move, then disable that possibility//
            {
                player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Disabled;
            }
        }

        #endregion Main State Controller
    }

    #region Default State
    void DefaultStateController()
    {
        if (!player_Main.playerIsGrounded) //All states that need tobe done when player is on air//
        {
            DefaultState_OnAirStates();
        }
        else if (player_Main.playerIsGrounded) //All States that need to be done when player is on the ground//
        {
            DefaultState_OnGroundStates();
        }
        else
        {
            Debug.Log("Something is broken with grounding");
            player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Disabled;
        }
    }

    void DefaultState_OnAirStates() //All states that required to be on air//
    {
        if (player_Movement_Default.verticalVelocity >= 0)
        {
            player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.OnAir;
        }
        else if (player_Movement_Default.verticalVelocity < 0)
        {
            player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.OnAir_Falling;
        }
        else
        {
            Debug.LogWarning("Something broken which on air states!!!");
        }
    }

    void DefaultState_OnGroundStates() //All states that happens on the ground//
    {
        if (player_Main.playerIsCrouching && player_Main.canCrouch) //When player is crouching//
        {
            if (player_Main.playerInputsMovement) //If player Inputs movement buttons
            {
                if (player_Main.playerIsRunning)
                {
                    player_Movement_Default.CrouchToRun(); //If player starts to run from crouching, then stand up, and start running//
                }
                else
                {
                    player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Sneaking_Walk;
                }
            }
            else
            {
                player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Sneaking_Idle;
            }
        }
        else if (!player_Main.playerIsCrouching) //When player is not moving//
        {
            /*  if(player_Main.playerWillToJump && player_Main.canJump) //Jump State//
              {
                  player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Jumping;
              }
              else */
            if (player_Main.playerInputsMovement && player_Main.canWalk) //If player Inputs movement buttons
            {
                if (player_Main.playerIsRunning && player_Main.canRun) //If player Running then run
                {
                    player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Running;
                }
                else //Else Normal walking
                {
                    player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Walking;
                }
            }
            else //When nothing is inputing, then idle//
            {
                player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Idle;
            }
        }
        else //If Something Is somehow Broken//
        {
            Debug.Log("Something is broken with crouching");
            player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Disabled;
            return;
        }
    }

    #endregion Default State

}
