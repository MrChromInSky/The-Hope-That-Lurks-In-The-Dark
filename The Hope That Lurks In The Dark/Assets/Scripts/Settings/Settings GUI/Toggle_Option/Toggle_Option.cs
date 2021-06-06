using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

[SelectionBase]
public class Toggle_Option : MonoBehaviour
{
    #region Assignments
    Settings_GUI_Controller settings_GUI_Controller;

    private void Awake()
    {
        settings_GUI_Controller = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Settings_GUI_Controller>();
    }
    #endregion Assignment

    #region Variables
    [Header("Toggle Option Attributes")]
    public int toggleSettingID; //Id of slider option//
    [Space]
    public string toggleOptionName;
    public bool toggleOptionValue;

    [Header("Text & UI Elements")]
    public TextMeshProUGUI toggleOptionNameText;
    public Toggle optionToggle;
    #endregion Variables

    void Start()
    {
        toggleOptionNameText.SetText(toggleOptionName);
        optionToggle.onValueChanged.AddListener(delegate { ValueUpdate(); });
    }

    void ValueUpdate()
    {
        toggleOptionValue = optionToggle.isOn;
        settings_GUI_Controller.ToggleStateUpdate(toggleSettingID, toggleOptionValue);
    }
}
