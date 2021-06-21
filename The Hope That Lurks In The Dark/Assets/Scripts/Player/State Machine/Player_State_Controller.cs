using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Controller : MonoBehaviour
{
    #region Assignments
    Player_Main player_Main;
    Player_Movement player_Movement;

    void Awake()
    {
        player_Main = GetComponent<Player_Main>();
        player_Movement = GetComponent<Player_Movement>();
    }
    #endregion Assignments

    void Update()
    {
        if (player_Main.playerMainState == Player_Main.PlayerMainStates.OnPause) //Pause
        {

        }
        else if (player_Main.playerMainState == Player_Main.PlayerMainStates.Death) //Death
        {

        }
        else if (player_Main.playerMainState == Player_Main.PlayerMainStates.DefaultMovement) //Basic Movement
        {
            //Default state control function//
            DefaultMovementState();

            #region Turn Off Other States
            //Special Movement
            player_Main.playerSpecialState = Player_Main.PlayerSpecialStates.Disabled;
            //Death State
            player_Main.playerDeathState = Player_Main.PlayerDeathStates.Alive;
            //Pause//
            player_Main.playerHideState = Player_Main.PlayerHideStates.Disabled;
            //Cutscene//
            player_Main.playerCutsceneState = Player_Main.PlayerCutsceneStates.Disabled;

            #endregion Turn Off Other States
        }


    }

    #region Default Movement State
    void DefaultMovementState()
    {

        if (player_Main.playerIsSliding) //when sliding from edge//
        {
            player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.SlidingFromEdge;
        }
        else if (!player_Main.playerIsGrounded) //When is on Air//
        {
            if (player_Movement.verticalMovementValue > player_Movement.fallingStartValue) //On air
            {
                player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.OnAir;
            }
            else if (player_Movement.verticalMovementValue <= player_Movement.fallingStartValue) //On Air but when falling :)
            {
                player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.OnAir_Falling;
            }
        }
        else if (player_Main.playerIsGrounded) //When is grounded//
        {
            if (false) //player will to jump
            {
                player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Jumping;
            }
            else if (player_Main.playerIsCrouching)
            {
                if (player_Main.playerIsMoving) //When player walking in crouch
                {
                    if (player_Main.playerIsRunning) //Stay from crouch when run//
                    {
                        //Change state
                        player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Running;
                        //Not crouching anymore
                        player_Main.playerIsCrouching = false;
                    }
                    else
                    {
                        player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Crouch_Walking;
                    }
                }
                else if (!player_Main.playerIsMoving) //when player is idle in crouch//
                {
                    player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Crouch_Idle;
                }
            }
            else //Normal movement
            {
                if (player_Main.playerIsMoving) //When player will to move//
                {
                    if (player_Main.playerIsRunning)
                    {
                        player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Running;
                    }
                    else //basic Walking
                    {
                        player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Walking;
                    }
                }
                else if (!player_Main.playerIsMoving) //Basic Idle//
                {
                    player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Idle;

                }
                else //When Noting is working//
                {
                    player_Main.playerDefaultState = Player_Main.PlayerDefaultStates.Disabled;
                }
            }
        }
    }

    #endregion Default Movement State
}
