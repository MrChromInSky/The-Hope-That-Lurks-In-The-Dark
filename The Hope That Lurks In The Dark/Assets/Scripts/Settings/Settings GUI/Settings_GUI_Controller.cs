using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Settings_GUI_Controller : MonoBehaviour
{
    #region Assignments
    Settings_Controller settings_Controller;

    Video_Settings video_Settings;
    Controls_Settings controls_Settings;
    Audio_Settings audio_Settings;
    Visual_Settings visual_Settings;

    private void Awake()
    {
        settings_Controller = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Settings_Controller>();

        video_Settings = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Video_Settings>();
        controls_Settings = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Controls_Settings>();
        audio_Settings = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Audio_Settings>();
        visual_Settings = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<Visual_Settings>();

        StartCoroutine("TemporaryUpdate");
    }
    #endregion Assignments

    #region Variables
    [Header("Settings Arrays")]
    public GameObject[] switchOptions;
    public GameObject[] sliderOptions;
    public GameObject[] toggleOptions;
    public GameObject[] inputOptions;


    #endregion Variables

    void Start()
    {
        //Create Arrays//
        switchOptions = GameObject.FindGameObjectsWithTag("Option_Switch");
        sliderOptions = GameObject.FindGameObjectsWithTag("Option_Slider");
        toggleOptions = GameObject.FindGameObjectsWithTag("Option_Toggle");
        inputOptions = GameObject.FindGameObjectsWithTag("Option_Input");

        //Set values//
        StartCoroutine("SwitchOptionsInitial"); //Initial Switches state//
        StartCoroutine("SliderOptionsInitial"); //Initial Sliders State//
        StartCoroutine("ToggleOptionsInitial"); //Initial Toggles State//
        StartCoroutine("InputOptionsInitial"); // Initial Inputs State//

    }

    #region Switch Functions
    IEnumerator SwitchOptionsInitial() //On start Setup, and enable actual states//
    {
        yield return new WaitForSecondsRealtime(0.1f);

        foreach (GameObject switchOption in switchOptions)
        {
            // switchOption.GetComponent<Switch_Option>() // Template//

            if (switchOption.GetComponent<Switch_Option>().switchSettingID == 0)
            {
                switchOption.GetComponent<Switch_Option>().switchActualState = video_Settings.screenMode_actual;
            }
            else if (switchOption.GetComponent<Switch_Option>().switchSettingID == 1)
            {
                switchOption.GetComponent<Switch_Option>().switchActualState = video_Settings.resolution_actual;
            }
            if (switchOption.GetComponent<Switch_Option>().switchSettingID == 2)
            {
                switchOption.GetComponent<Switch_Option>().switchActualState = video_Settings.vSync_actual;
            }

            switchOption.GetComponent<Switch_Option>().SetText();
        }
    }

    public void SwitchValueUpdate(int SwitchOptionID, int SwitchOptionValue)
    {
        if (SwitchOptionID == 0)
        {
            video_Settings.screenMode_temporary = SwitchOptionValue;
        }
        else if (SwitchOptionID == 1)
        {
            video_Settings.resolution_temporary = SwitchOptionValue;
        }
        else if (SwitchOptionID == 2)
        {
            video_Settings.vSync_temporary = SwitchOptionValue;
        }
    }
    #endregion Switch Functions

    #region Slider Functions

    IEnumerator SliderOptionsInitial() //On start Setup, and enable actual states//
    {
        yield return new WaitForSecondsRealtime(0.1f);

        foreach (GameObject sliderOption in sliderOptions)
        {
            if (sliderOption.GetComponent<Slider_Option>().sliderSettingID == 0)
            {
                sliderOption.GetComponent<Slider_Option>().optionSlider.value = audio_Settings.masterVolume_actual;
            }
            else if (sliderOption.GetComponent<Slider_Option>().sliderSettingID == 1)
            {
                sliderOption.GetComponent<Slider_Option>().optionSlider.value = audio_Settings.sfxVolume_actual;
            }
            else if (sliderOption.GetComponent<Slider_Option>().sliderSettingID == 2)
            {
                sliderOption.GetComponent<Slider_Option>().optionSlider.value = audio_Settings.musicVolume_actual;
            }
            else if (sliderOption.GetComponent<Slider_Option>().sliderSettingID == 3)
            {
                sliderOption.GetComponent<Slider_Option>().optionSlider.value = audio_Settings.dialoguesVolume_actual;
            }

        }

    }


    public void SliderValueUpdate(int sliderOptionID, int sliderOptionValue)
    {
        if (sliderOptionID == 0)
        {
            audio_Settings.masterVolume_temporary = sliderOptionValue;
        }
        else if (sliderOptionID == 1)
        {
            audio_Settings.sfxVolume_temporary = sliderOptionValue;
        }
        else if (sliderOptionID == 2)
        {
            audio_Settings.musicVolume_temporary = sliderOptionValue;
        }
        else if (sliderOptionID == 3)
        {
            audio_Settings.dialoguesVolume_temporary = sliderOptionValue;
        }
    }

    #endregion Slider Functions

    #region Toggle Functions

    IEnumerator ToggleOptionsInitial()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (GameObject toggleOption in toggleOptions)
        {
            if (toggleOption.GetComponent<Toggle_Option>().toggleSettingID == 0)
            {
                toggleOption.GetComponent<Toggle_Option>().optionToggle.isOn = controls_Settings.inverseYAxis_actual;
            }
            else if (toggleOption.GetComponent<Toggle_Option>().toggleSettingID == 1)
            {
                toggleOption.GetComponent<Toggle_Option>().optionToggle.isOn = audio_Settings.muteSound_actual;
            }
            else if (toggleOption.GetComponent<Toggle_Option>().toggleSettingID == 2)
            {
                toggleOption.GetComponent<Toggle_Option>().optionToggle.isOn = video_Settings.customResolution_actual;
            }
            else if (toggleOption.GetComponent<Toggle_Option>().toggleSettingID == 3)
            {
                toggleOption.GetComponent<Toggle_Option>().optionToggle.isOn = video_Settings.limitFramerate_actual;
            }
        }
    }

    public void ToggleStateUpdate(int toggleOptionID, bool toggleOptionState)
    {
        if (toggleOptionID == 0)
        {
            controls_Settings.reverseYAxis_temporary = toggleOptionState;
        }
        else if (toggleOptionID == 1)
        {
            audio_Settings.muteSound_temporary = toggleOptionState;
        }
        else if (toggleOptionID == 2)
        {
            video_Settings.customResolution_temporary = toggleOptionState;
        }
        else if (toggleOptionID == 3)
        {
            video_Settings.limitFramerate_temporary = toggleOptionState;
        }
    }



    #endregion Toggle Functions

    #region Input Functions
    IEnumerator InputOptionsInitial()
    {
        yield return new WaitForSeconds(0.1f);

        foreach(GameObject inputOption in inputOptions)
        {
            if(inputOption.GetComponent<Input_Option>().inputSettingID == 0)
            {
                inputOption.GetComponent<Input_Option>().inputActualValue = controls_Settings.lookSensitivity_x_actual.ToString();
            }
            else if (inputOption.GetComponent<Input_Option>().inputSettingID == 1)
            {
                inputOption.GetComponent<Input_Option>().inputActualValue = controls_Settings.lookSensitivity_y_actual.ToString();
            }
            else if (inputOption.GetComponent<Input_Option>().inputSettingID == 2)
            {
                inputOption.GetComponent<Input_Option>().inputActualValue = video_Settings.customResolutionWidth_actual.ToString();
            }
            else if (inputOption.GetComponent<Input_Option>().inputSettingID == 3)
            {
                inputOption.GetComponent<Input_Option>().inputActualValue = video_Settings.customResolutionHeight_actual.ToString();
            }
            else if (inputOption.GetComponent<Input_Option>().inputSettingID == 4)
            {
                inputOption.GetComponent<Input_Option>().inputActualValue = video_Settings.maxFramerate_actual.ToString();
            }
        }
    }

    public void InputStateUpdate(int inputOptionID, string inputOptionValue)
    {
        if(inputOptionID == 0)
        {
            controls_Settings.lookSensitivity_x_temporary = float.Parse(inputOptionValue);
        }
        else if (inputOptionID == 1)
        {
            controls_Settings.lookSensitivity_y_temporary = float.Parse(inputOptionValue);
        }
        else if (inputOptionID == 2)
        {
            video_Settings.customResolutionHeight_temporary = Convert.ToInt32(inputOptionValue);
        }
        else if (inputOptionID == 3)
        {
            video_Settings.customResolutionWidth_temporary = Convert.ToInt32(inputOptionValue);
        }
        else if (inputOptionID == 4)
        {
            video_Settings.maxFramerate_temporary = Convert.ToInt32(inputOptionValue);
        }
    }

    #endregion Input Functions

    public void ApplySettingsFromGUI() //Option Apply Function//
    {
        video_Settings.TemporaryToActual();
        audio_Settings.TemporaryToActual();
        controls_Settings.TemporaryToActual();

        //Save settings//
        settings_Controller.SaveSettings();
    }

    public void RestoreDefaultSettings()
    {
        //Restore Settings to default
        video_Settings.VideoDefaultValueSet();
        audio_Settings.AudioDefaultValueSet();
        controls_Settings.ControlsDefaultValueSet();
        
        //Update Temporary settings//
        video_Settings.ActualToTemporary();
        audio_Settings.ActualToTemporary();
        controls_Settings.ActualToTemporary();

        //Special for input options//
        foreach(GameObject inputOption in inputOptions)
        {
            inputOption.GetComponent<Input_Option>().UpdateTextInField();
        }

        //Change to new Values//
        StartCoroutine("SwitchOptionsInitial"); //Initial Switches state//
        StartCoroutine("SliderOptionsInitial"); //Initial Sliders State//
        StartCoroutine("ToggleOptionsInitial"); //Initial Toggles State//
        StartCoroutine("InputOptionsInitial"); // Initial Inputs State//

        //Check and save//
        ApplySettingsFromGUI();
    }

    IEnumerator TemporaryUpdate()
    {
        yield return new WaitForSeconds(0.8f);

        video_Settings.ActualToTemporary();
        audio_Settings.ActualToTemporary();
        controls_Settings.ActualToTemporary();

    }
}
