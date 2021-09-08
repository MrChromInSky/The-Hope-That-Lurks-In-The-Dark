using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Controller : MonoBehaviour
{
    #region Variables
    [Header("Cursor Options")]
    public bool cursorVisibility;
    public bool cursorLocked;
    #endregion Variables

    private void Start()
    {

#if UNITY_EDITOR

        cursorLocked = false;
        cursorVisibility = true;

#else
        cursorLocked = true;
        cursorVisibility = false;

#endif

        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }


        if (cursorVisibility)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }

    }
}
