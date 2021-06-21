using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundCheck : MonoBehaviour
{
    #region Assignments
    Player_Main player_Main;

    void Awake()
    {
        player_Main = GetComponent<Player_Main>();
    }
    #endregion Assignments

    #region Variables
    [Header("Is Player Grounded?")]
    public bool playerIsGrounded;

    [Header("Ground Check Settings")]
    //oigin
    public Transform checkOrigin;
    [Space]

    //Check Settings//
    [SerializeField] float groundCheckDistance;
    [SerializeField] float groundCheckSphereRadius;

    //Layer Mask
    public LayerMask groundLayers;

    //Raycast hit//
    public RaycastHit sphereGroundHit;

    [Header("Slope Check Settings")]
    [SerializeField] float maxSlopeAngle;

    [Header("Slope Check Ray Settings")]
    [SerializeField] float slopeCheckRayLenght;
    public RaycastHit slopeHit_Ray;

    [Header("Slope Check Sphere Settings")]
    [SerializeField] float slopeCheckSphereRayLenght;
    [SerializeField] float slopeControlSphereCheckRadius;
    public RaycastHit slopeHit_Sphere;


    #endregion Variables

    void Update()
    {
        GroundCheck();
        IsOnSlopeCheck();
    }

    void GroundCheck()
    {
        if (Physics.SphereCast(checkOrigin.position, groundCheckSphereRadius, Vector3.down, out sphereGroundHit, groundCheckDistance, groundLayers))
        {
            playerIsGrounded = true;

            //For Gizmo
            hitDistance = sphereGroundHit.distance;
        }
        else
        {
            playerIsGrounded = false;

            //For Gizmo
            hitDistance = groundCheckDistance;
        }

        //Update Main State//
        player_Main.playerIsGrounded = playerIsGrounded;
    }

    void IsOnSlopeCheck()
    {
        if (Physics.Raycast(checkOrigin.position, Vector3.down, out slopeHit_Ray, slopeCheckRayLenght, groundLayers))
        {
            if (Vector3.Angle(slopeHit_Ray.normal, Vector3.up) > maxSlopeAngle && playerIsGrounded)
            {
                player_Main.playerIsSliding = true;
            }
            else
            {
                player_Main.playerIsSliding = false;
            }
        }
        else //If check ray does not hit anything, then check sphere//
        {
            if (Physics.SphereCast(checkOrigin.position, slopeControlSphereCheckRadius, Vector3.down, out slopeHit_Ray, slopeCheckSphereRayLenght, groundLayers))
            {
                if (Vector3.Angle(slopeHit_Ray.normal, Vector3.up) > maxSlopeAngle)
                {
                    player_Main.playerIsSliding = true;
                }
                else
                {
                    player_Main.playerIsSliding = false;
                }
            }
            else //If nothing, then nothing :P//
            {
                player_Main.playerIsSliding = false;
            }
        }

    }

    #region Draw Gizmos
    //Debug Variables///
    float hitDistance;

    //For Debuging
    private void OnDrawGizmos()
    {
        //Ground Gizmo
        Gizmos.color = Color.red;
        Debug.DrawLine(checkOrigin.position, transform.position + Vector3.down * hitDistance);
        Gizmos.DrawWireSphere(checkOrigin.position + Vector3.down * hitDistance, groundCheckSphereRadius);

        //Slide Gizmo
        Gizmos.color = Color.cyan;
        Debug.DrawLine(checkOrigin.position, transform.position + Vector3.down * slopeCheckRayLenght);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(checkOrigin.position + Vector3.down * slopeCheckSphereRayLenght, slopeControlSphereCheckRadius);
    }
    #endregion Draw Gizmos
}
