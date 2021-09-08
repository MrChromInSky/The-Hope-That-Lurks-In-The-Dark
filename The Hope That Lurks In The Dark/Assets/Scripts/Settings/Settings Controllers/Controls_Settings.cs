using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls_Settings : MonoBehaviour
{
    #region Control Settings

    //Actual Settings//
    #region Actual Settings
    [Header("Look Sensitivity")]
    public float lookSensitivity_x_actual;
    public float lookSensitivity_y_actual;
    public bool inverseYAxis_actual;
    #endregion Actual Settings

    //Default Settings//
    #region Default Settings
    readonly float lookSensitivity_x_default = 3;
    readonly float lookSensitivity_y_default = 3;
    readonly bool reverseYAxis_default = false;
    #endregion Deafault Settings

    //Temporary Settings//
    #region Temporary Settings
    [Header("Look Sensitivity")]
    public float lookSensitivity_x_temporary;
    public float lookSensitivity_y_temporary;
    public bool reverseYAxis_temporary;
    #endregion Temporary Settings
    #endregion Control Settings

    public void ControlsDefaultValueSet()
    {
        lookSensitivity_x_actual = lookSensitivity_x_default;
        lookSensitivity_y_actual = lookSensitivity_y_default;
        inverseYAxis_actual = reverseYAxis_default;
    }

    public void TemporaryToActual()
    {
        lookSensitivity_x_actual = lookSensitivity_x_temporary;
        lookSensitivity_y_actual = lookSensitivity_y_temporary;
        inverseYAxis_actual = reverseYAxis_temporary;
    }

    public void ActualToTemporary()
    {
        lookSensitivity_x_temporary = lookSensitivity_x_actual;
        lookSensitivity_y_temporary = lookSensitivity_y_actual;
        reverseYAxis_temporary = inverseYAxis_actual;
    }


}
