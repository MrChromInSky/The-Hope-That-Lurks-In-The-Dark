using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

[SelectionBase]
public class Slider_Option : MonoBehaviour
{
    #region Assignments
    Settings_GUI_Controller settings_GUI_Controller;

    private void Awake()
    {
        settings_GUI_Controller = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Settings_GUI_Controller>();
    }
    #endregion Assignment

    #region Variables
    [Header("Slider Option Attributes")]
    public int sliderSettingID; //Id of slider option//
    [Space]
    public string sliderOptionName;
    public float sliderActualValue;

    [Space, Header("Texts and UI Elements")]
    public TextMeshProUGUI sliderOptionNameText;
    public TextMeshProUGUI sliderValueText;
    public Slider optionSlider;
    #endregion Variables

    private void Start()
    {
        optionSlider.onValueChanged.AddListener( delegate { ValueUpdate(); });
        sliderOptionNameText.SetText(sliderOptionName);
        SetText();
    }

    void ValueUpdate()
    {
        SetText();
        settings_GUI_Controller.SliderValueUpdate(sliderSettingID, (int)optionSlider.value);
    }

    void SetText()
    {
        sliderValueText.SetText(optionSlider.value.ToString());
    }
}
