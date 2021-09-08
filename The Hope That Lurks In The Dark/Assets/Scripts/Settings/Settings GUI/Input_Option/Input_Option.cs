using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

[SelectionBase]
public class Input_Option : MonoBehaviour
{
    #region Assignments
    Settings_GUI_Controller settings_GUI_Controller;

    private void Awake()
    {
        settings_GUI_Controller = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Settings_GUI_Controller>();
    }
    #endregion Assignment

    #region Variables
    [Header("Input Option Attributes")]
    public int inputSettingID;
    [Space]
    public string inputOptionName;
    public string inputActualValue = "placeholder";

    [Space, Header("Texts and UI Elements")]
    public TextMeshProUGUI inputOptionNameText;
    public TMP_InputField optionInputField;
    #endregion Variables

    void Start()
    {
        inputOptionNameText.SetText(inputOptionName);
        
        optionInputField.onEndEdit.AddListener(delegate { UpdateValue(); });

        StartCoroutine("SetText");
    }

    IEnumerator SetText()
    {
        yield return new WaitForSeconds(0.2f);
        optionInputField.text = inputActualValue;
    }

    void UpdateValue()
    {
        inputActualValue = optionInputField.text.ToString();
        settings_GUI_Controller.InputStateUpdate(inputSettingID, inputActualValue);
    }

    public void UpdateTextInField()
    {
        StartCoroutine("SetText");
    }

}
