using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateController : MonoBehaviour
{
    #region Assignments
    Player_Main player_Main; //Player Main Script//

    void Awake()
    {
        player_Main = GetComponent<Player_Main>();
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
        if (!player_Main.playerIsGrounded) //All states that need tobe done when playeer is on air//
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
        player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.OnAir;
    }

    void DefaultState_OnGroundStates() //All states that happens on the ground//
    {
        if(player_Main.playerIsCrouching) //When player is crouching//
        {
            if(player_Main.playerInputsMovement) //If player Inputs movement buttons
            {
                player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Sneaking_Walk;
            }
            else
            {
                player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Sneaking_Idle;
            }
        }
        else if(!player_Main.playerIsCrouching) //When player is not moving//
        {
            if (player_Main.playerInputsMovement) //If player Inputs movement buttons
            {
                if(player_Main.playerIsRunning) //If player Running then run
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
