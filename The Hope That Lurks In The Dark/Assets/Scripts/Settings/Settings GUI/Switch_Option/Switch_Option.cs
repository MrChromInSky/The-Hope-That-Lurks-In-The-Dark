using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[SelectionBase]
public class Switch_Option : MonoBehaviour
{
    #region Assignments
    Settings_GUI_Controller settings_GUI_Controller;

    private void Awake()
    {
       settings_GUI_Controller = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Settings_GUI_Controller>();
    }
    #endregion Assignment

    #region Variables
    [Header("Switch Attributes")]
    public int switchSettingID; // Id of this option//
    [Space]
    public string switchOptionName; //Setting name//
    public string[] optionStateNames; //All option states//
    public int switchActualState;


    [Space, Header("Texts")]
    public TextMeshProUGUI switchOptionNameText;
    public TextMeshProUGUI switchOptionStateText;

    int allStates; //number of all states//
    #endregion Variables

    void Start()
    {
        allStates = optionStateNames.Length - 1;
        SetText();
        StartCoroutine("LateStateUpdate");
    }

    IEnumerator LateStateUpdate()
    {
        yield return new WaitForSeconds(0.5f);
        switchOptionStateText.SetText(optionStateNames[switchActualState]);
    }

    #region Button Functions
    public void buttonLeft()
    {
        switchActualState--;
        correctValueCheck();
        SetText();
        settings_GUI_Controller.SwitchValueUpdate(switchSettingID, switchActualState);
    }

    public void buttonRight()
    {
        switchActualState++;
        correctValueCheck();
        SetText();
        settings_GUI_Controller.SwitchValueUpdate(switchSettingID, switchActualState);
    }

    void correctValueCheck() //Checking if state value is correct, if not then repairs it//
    {
        if(switchActualState > allStates) //When Higher yhan max value, then reset from 0//
        {
            switchActualState = 0;
        }
        else if (switchActualState < 0) //If less than 0, then change to max value//
        {
            switchActualState = allStates;
        }
    }
    #endregion Button Functions

    public void SetText()
    {
        switchOptionNameText.SetText(switchOptionName);
        switchOptionStateText.SetText(optionStateNames[switchActualState]);
    }
}
