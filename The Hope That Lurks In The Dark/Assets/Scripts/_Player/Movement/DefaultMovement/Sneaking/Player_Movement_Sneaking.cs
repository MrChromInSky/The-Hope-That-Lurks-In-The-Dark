using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_Sneaking : MonoBehaviour
{
    #region Assignments
    CharacterController playerController;
    Player_Main player_Main;

    void Awake()
    {
        playerController = GetComponent<CharacterController>();
        player_Main = GetComponent<Player_Main>();
    }
    #endregion Assignments

    #region Variables
    [Header("Can Stand Up")]
    public bool canStandUp = true;

    [Header("DEBUG")]
    [SerializeField] float actualHeight;

    [Header("Standing")]
    [SerializeField] float standingHeight;
    [SerializeField] float standingSpeed;

    [Header("Crouching")]
    [SerializeField] float crouchingHeight;
    [SerializeField] float crouchingSpeed;

    #endregion Variables

    void Start()
    {

    }

    void Update()
    {
        actualHeight = playerController.height;

        CrouchController();
    }

    void CrouchController()
    {
        if (player_Main.playerIsCrouching) //When you start to crouch
        {
            CrouchController_ToCrouch();
        }
        else if (!player_Main.playerIsCrouching && canStandUp) //When you stand up
        {
            CrouchController_ToStand();
        }
        else //When you cannot stand up right now, or when something is broken//
        {
            Debug.Log("You Cannot Stand Up Now");
        }
    }

    void CrouchController_ToCrouch()
    {
        float lastHeight = playerController.height; 

        if (playerController.height != crouchingHeight)
        {
            if (playerController.height <= crouchingHeight + 0.01f)
            {
                playerController.height = crouchingHeight;
            }
            else
            {
                playerController.height = Mathf.Lerp(playerController.height, crouchingHeight, crouchingSpeed * Time.deltaTime);
            }

        }

        transform.position = new Vector3(transform.position.x, transform.position.y + (playerController.height - lastHeight) * 0.5f, transform.position.z);
    }

    void CrouchController_ToStand()
    {
        float lastHeight = playerController.height;

        if (playerController.height != standingHeight)
        {
            if (playerController.height >= standingHeight - 0.01f)
            {
                playerController.height = standingHeight;
            }
            else
            {
                playerController.height = Mathf.Lerp(playerController.height, standingHeight, standingSpeed * Time.deltaTime);
            }

            transform.position = new Vector3(transform.position.x, transform.position.y + (playerController.height - lastHeight) * 0.5f, transform.position.z); //Height corection//
        }
    }

    /*  void CrouchController_StandUppp()
      {

          if (player_Main.playerIsCrouching)
          {
              //Head
              if (playersHead.transform.localPosition.y > headCrouchHeight)
              {
                  playersHead.transform.localPosition = new Vector3(playersHead.transform.localPosition.x, Mathf.Lerp(playersHead.transform.localPosition.y, headCrouchHeight, headCrouchSpeed), playersHead.transform.localPosition.z);
              }

              //Body
              if (playerController.height > crouchHeight)
              {
                  playerController.height = Mathf.Lerp(playerController.height, crouchHeight, crouchSpeed * Time.deltaTime);
              }
          }
          else
          {
              if (playersHead.transform.localPosition.y < headStandHeight)
              {
                  playersHead.transform.localPosition = new Vector3(playersHead.transform.localPosition.x, Mathf.Lerp(playersHead.transform.localPosition.y, headStandHeight, headStandSpeed), playersHead.transform.localPosition.z);
              }



              if (playerController.height < standHeight)
              {
                  playerController.height = Mathf.Lerp(playerController.height, standHeight, standSpeed * Time.deltaTime);
              }
          }
      }*/
}

