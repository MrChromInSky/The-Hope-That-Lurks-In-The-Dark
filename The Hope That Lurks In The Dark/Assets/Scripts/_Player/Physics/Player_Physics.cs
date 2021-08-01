using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Physics : MonoBehaviour
{
    #region Assignments
    Player_Main player_Main;
    CharacterController playerController;

    void Awake()
    {
        player_Main = GetComponent<Player_Main>();
        playerController = GetComponent<CharacterController>();
    }
    #endregion Assignments

    #region Variables
    [Header("Pushing Powers")]
    [SerializeField] float defaultPushPower;
    [Space]
    [SerializeField] float boxPushPower;
    #endregion Variables

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (player_Main.canPhysicallyInteractWithObjects)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            #region When not to interact
            if (body == null || body.isKinematic) //When Kinematic or static, then return//
            {
                return;
            }
            else if (hit.moveDirection.y < -0.3f) // Do not push objects below us//
            {
                return;
            }
            #endregion When not to interact

            else if (player_Main.playerInputsMovement) //When everything is ok//
            {
                if (body.gameObject.CompareTag("Physics_Box"))
                {
                    PhysicsInteraction_Box(hit, body);
                }
                else
                {
                    PhysicsInteraction_Default(hit, body);
                }
            }

        }
        else
        {
            return;
        }
    }

    void PhysicsInteraction_Default(ControllerColliderHit hit, Rigidbody body)
    {
        //Set Push direction//
        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        //Add force in that direction//
        body.AddForce(pushDirection * defaultPushPower, ForceMode.Acceleration);
    }

    void PhysicsInteraction_Box(ControllerColliderHit hit, Rigidbody body)
    {
        //Set Push direction//
        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.AddForce(pushDirection * boxPushPower, ForceMode.Acceleration);

    }
}
