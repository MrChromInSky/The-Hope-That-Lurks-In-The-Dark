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
    [Header("Player Head")]
    [SerializeField] GameObject playersHead;

    [Header("Standing")]
    [SerializeField] float headStandHeight;
    [SerializeField] float headStandSpeed;
    [Space]
    [SerializeField] float standHeight;
    [SerializeField] float standSpeed;

    [Header("Crouching")]
    [SerializeField] float headCrouchHeight;
    [SerializeField] float headCrouchSpeed;
    [Space]
    [SerializeField] float crouchHeight;
    [SerializeField] float crouchSpeed;
    #endregion Variables

    void Start()
    {
        headStandHeight = playersHead.transform.localPosition.y;
        standHeight = playerController.height;
    }

    void Update()
    {
        CrouchController();

    }

    void CrouchController()
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
    }
}
