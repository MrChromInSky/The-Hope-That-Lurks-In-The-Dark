using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundChecks : MonoBehaviour
{
    #region Assignments
    Player_Main player_Main;
    CharacterController playerControler;

    void Awake()
    {
        player_Main = GetComponentInParent<Player_Main>();
        playerControler = GetComponentInParent<CharacterController>();
    }
    #endregion Assignments

    #region Variables
    [Header("Check Positions")]
    [SerializeField] Transform groundCheck_Origin;
    [Space]

    //Ground Check//
    [Header("Is on Ground")]
    [SerializeField] bool playerIsGrounded;

    [Header("Ground Check")]
    [SerializeField] float groundRay_Lenght;
    [Space]
    [SerializeField] float groundSphere_Lenght;
    [SerializeField] float groundSphere_Radius;
    [Space]

    //Slope Check//
    [Header("Is on Slope")]
    [SerializeField] bool playerIsOnSlope;

    [Header("Slope Check")]
    [SerializeField] float maxSlopeAngle;
    [Space]
    [SerializeField] float slopeRay_Lenght;
    [Space]
    [SerializeField] bool isSupportHitSomething;
    [SerializeField] float slopeSupportSphere_Lenght;
    [SerializeField] float slopeSupportSphere_Radius;
    [Space]
    [SerializeField] float slopeSphere_Lenght;
    [SerializeField] float slopeSphere_Radius;
    [Space]
    [SerializeField] Vector3 actualSlopeNormals;



    [Header("Ground Layer Masks")]
    public LayerMask groundLayers;

    [Header("Raycast Hits")]
    RaycastHit groundHit;
    public RaycastHit slopeHit; //Main Raycast Hit for slope normal check//
    public RaycastHit supportSphereHit; //For support sphere check if hit something//

    [Header("Debug Gizmo")]
    [SerializeField] bool DEBUG_GIZMO_GroundRay;
    [SerializeField] bool DEBUG_GIZMO_GroundSphere;
    [Space]
    [SerializeField] bool DEBUG_GIZMO_SlopeRay;
    [SerializeField] bool DEBUG_GIZMO_SlopeSphere;

    //Gizmo lenghts//
    float groundRaycastHit_Distance;
    float groundRaycastHitSphere_Distance;
    float slopeRaycastHit_Distance;
    float slopeRaycastHitSphere_Distance;

    #endregion Variables

    void Start()
    {
        maxSlopeAngle = playerControler.slopeLimit; //Max slope angle set//

    }

    void Update()
    {
        GroundCheck();
        SlopeCheck();
    }

    #region Check Functions
    void GroundCheck()
    {
        if (Physics.Raycast(groundCheck_Origin.position, Vector3.down, out groundHit, groundRay_Lenght, groundLayers))
        {
            playerIsGrounded = true;

            //Gizmo
            groundRaycastHit_Distance = groundHit.distance;
        }
        else //If Ray does not hit ground, the check if sphere can//
        {
            //Gizmo
            groundRaycastHit_Distance = groundRay_Lenght;

            if (Physics.SphereCast(groundCheck_Origin.position, groundSphere_Radius, Vector3.down, out groundHit, groundSphere_Lenght, groundLayers))
            {
                playerIsGrounded = true;

                //Gizmo
                groundRaycastHitSphere_Distance = groundHit.distance;
            }
            else //If any of them are catching ground//
            {
                playerIsGrounded = false;

                //Gizmo
                groundRaycastHitSphere_Distance = groundSphere_Lenght;
            }
        }

        //State Update//
        player_Main.playerIsGrounded = playerIsGrounded;
    }

    void SlopeCheck()
    {

        if (Physics.Raycast(groundCheck_Origin.position, Vector3.down, out slopeHit, slopeRay_Lenght, groundLayers)) //Cast ray//
        {
            if (Physics.SphereCast(groundCheck_Origin.position, slopeSupportSphere_Radius, Vector3.down, out supportSphereHit, slopeSupportSphere_Lenght, groundLayers))
            {
                isSupportHitSomething = true;

                if (Vector3.Angle(slopeHit.normal, Vector3.up) > maxSlopeAngle && Vector3.Angle(supportSphereHit.normal, Vector3.up) > maxSlopeAngle)
                {
                    playerIsOnSlope = true;
                }
                else
                {
                    playerIsOnSlope = false;
                }

                //Gizmos
                slopeRaycastHit_Distance = slopeHit.distance;

            }
            else
            {
                isSupportHitSomething = false;

                //2 method
                slopeRaycastHit_Distance = slopeSphere_Lenght;

                if (Physics.SphereCast(groundCheck_Origin.position, slopeSphere_Radius, Vector3.down, out slopeHit, slopeSphere_Lenght, groundLayers))
                {
                    if (Vector3.Angle(slopeHit.normal, Vector3.up) > maxSlopeAngle)
                    {
                        playerIsOnSlope = true;
                    }
                    else
                    {
                        playerIsOnSlope = false;
                    }

                    //Gizmos
                    slopeRaycastHitSphere_Distance = slopeHit.distance;
                }
                else //If nothing
                {
                    playerIsOnSlope = false;

                    //Gizmos
                    slopeRaycastHitSphere_Distance = slopeSphere_Lenght;
                }
            }
        }
        else //If not then 2 method
        {
            slopeRaycastHit_Distance = slopeSphere_Lenght;

            if (Physics.SphereCast(groundCheck_Origin.position, slopeSphere_Radius, Vector3.down, out slopeHit, slopeSphere_Lenght, groundLayers))
            {
                if (Vector3.Angle(slopeHit.normal, Vector3.up) > maxSlopeAngle)
                {
                    playerIsOnSlope = true;
                }
                else
                {
                    playerIsOnSlope = false;
                }

                //Gizmos
                slopeRaycastHitSphere_Distance = slopeHit.distance;
            }
            else //If nothing
            {
                playerIsOnSlope = false;

                //Gizmos
                slopeRaycastHitSphere_Distance = slopeSphere_Lenght;
            }
        }

        //State Update//
        player_Main.playerIsOnSteepSlope = playerIsOnSlope;
    }

    #endregion Check Functions

    #region Debug Gizmos
    private void OnDrawGizmos()
    {
        if (DEBUG_GIZMO_GroundRay)
        {
            //Ground ray//
            Debug.DrawLine(groundCheck_Origin.position, groundCheck_Origin.position + Vector3.down * groundRay_Lenght, Color.yellow);
        }

        if (DEBUG_GIZMO_GroundSphere)
        {
            //Ground Sphere//
            Debug.DrawLine(groundCheck_Origin.position, groundCheck_Origin.position + Vector3.down * groundRaycastHitSphere_Distance, Color.green);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(groundCheck_Origin.position + Vector3.down * groundRaycastHitSphere_Distance, groundSphere_Radius);
        }

        if (DEBUG_GIZMO_SlopeRay)
        {
            //Ray
            Debug.DrawLine(groundCheck_Origin.position, groundCheck_Origin.position + Vector3.down * slopeRay_Lenght, Color.black);


            //Support Sphere//
            Debug.DrawLine(groundCheck_Origin.position, groundCheck_Origin.position + Vector3.down * slopeRaycastHit_Distance, Color.gray);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck_Origin.position + Vector3.down * slopeRaycastHit_Distance, slopeSupportSphere_Radius);
        }

        if (DEBUG_GIZMO_SlopeSphere)
        {
            //Ground Sphere//
            Debug.DrawLine(groundCheck_Origin.position, groundCheck_Origin.position + Vector3.down * slopeRaycastHitSphere_Distance, Color.gray);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(groundCheck_Origin.position + Vector3.down * slopeRaycastHitSphere_Distance, slopeSphere_Radius);
        }




    }
    #endregion Debug Gizmos
}
