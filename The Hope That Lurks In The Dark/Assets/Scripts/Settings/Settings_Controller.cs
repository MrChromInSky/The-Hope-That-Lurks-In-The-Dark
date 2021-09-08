using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Settings_Controller : MonoBehaviour
{
    #region Assigments
    Video_Settings video_Settings; //Video Settings Script
    Visual_Settings visual_Settings; //Visual Settings Script
    Audio_Settings audio_Settings; //Audio Settings Script
    Controls_Settings controls_Settings; //Controls Settings Script

    GameSettings gameSettings = new GameSettings();

    //Declarations//
    private void Awake()
    {
        video_Settings = GetComponent<Video_Settings>();
        visual_Settings = GetComponent<Visual_Settings>();
        audio_Settings = GetComponent<Audio_Settings>();
        controls_Settings = GetComponent<Controls_Settings>();

        //Declaration of file path//
        fullConfigurationPath = Application.persistentDataPath + "/" + configurationDirectoryPath + "/" + configFileName + ".json";
    }
    #endregion Assigments

    #region Variables
    //Path to Configuration file//
    [Header("Setting Directory Name")]
    public string configurationDirectoryPath;

    [Header("Setting File Name")]
    public string configFileName;

    string fullConfigurationPath;
    #endregion Variables

    private void Start()
    {
        //On Game Start, check Path, and load settings//
        CheckPath();
    }

    void CheckPath()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/" + configurationDirectoryPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + configurationDirectoryPath);
        }

        LoadSettingsFile();
    }

    #region Save Settings Functions

    public void ActualSettingsSetup() //Applying Actual Settings To Save//
    {
        #region Video
        //Screen Mode//
        gameSettings.ScreenMode = video_Settings.screenMode_actual;

        //Resolution//
        gameSettings.Resolution = video_Settings.resolution_actual;

        //Custom Resolution//
        gameSettings.CustomResolution = video_Settings.customResolution_actual;
        gameSettings.CustomResolution_Height = video_Settings.customResolutionHeight_actual;
        gameSettings.CustomResolution_Width = video_Settings.customResolutionWidth_actual;

        //VSync//
        gameSettings.VSync = video_Settings.vSync_actual;

        //Limit Frame Rate//
        gameSettings.LimitFrameRate = video_Settings.limitFramerate_actual;
        gameSettings.MaxFrameRate = video_Settings.maxFramerate_actual;
        #endregion Video
        #region Audio
        //Mute//
        gameSettings.MuteAudio = audio_Settings.muteSound_actual;

        //Volumes//
        gameSettings.MasterVolume = audio_Settings.masterVolume_actual;
        gameSettings.SFXVolume = audio_Settings.sfxVolume_actual;
        gameSettings.MusicVolume = audio_Settings.musicVolume_actual;
        gameSettings.DialoguesVolume = audio_Settings.dialoguesVolume_actual;
        #endregion Audio
        #region Controls
        gameSettings.LookSensitivity_X = controls_Settings.lookSensitivity_x_actual;
        gameSettings.LookSensitivity_Y = controls_Settings.lookSensitivity_y_actual;
        gameSettings.Inverse_Y_Axis = controls_Settings.inverseYAxis_actual;
        #endregion Controls

        SettingsExecution();
    }

    public void SaveSettings() //Saving Config file//
    {
        ActualSettingsSetup();
        string settingsDataSave = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(fullConfigurationPath, settingsDataSave);
    }

    #endregion Save Settings Functions

    #region Load Settings Functions
    public void LoadSettingsFile() //Loading Settings From File//
    {
        if (File.Exists(fullConfigurationPath)) //If exist then do it//
        {
            string settingsDataLoad = File.ReadAllText(fullConfigurationPath);
            gameSettings = JsonUtility.FromJson<GameSettings>(settingsDataLoad);
        }
        else //If not Reset to defloat//
        {
            DefloatSettings();
        }

        ApplyLoadedSettings();
    }

    public void ApplyLoadedSettings() //Applying Loaded settings, and executing them//
    {
        #region Video
        //Screen Mode//
        video_Settings.screenMode_actual = gameSettings.ScreenMode;

        //Resolution//
        video_Settings.resolution_actual = gameSettings.Resolution;

        //Custom Resolution//
        video_Settings.customResolution_actual = gameSettings.CustomResolution;
        video_Settings.customResolutionHeight_actual = gameSettings.CustomResolution_Height;
        video_Settings.customResolutionWidth_actual = gameSettings.CustomResolution_Width;

        //VSync//
        video_Settings.vSync_actual = gameSettings.VSync;

        //Limit Frame Rate//
        video_Settings.limitFramerate_actual = gameSettings.LimitFrameRate;
        video_Settings.maxFramerate_actual = gameSettings.MaxFrameRate;
        #endregion Video
        #region Audio
        //Mute//
        audio_Settings.muteSound_actual = gameSettings.MuteAudio;

        //Volumes//
        audio_Settings.masterVolume_actual = gameSettings.MasterVolume;
        audio_Settings.sfxVolume_actual = gameSettings.SFXVolume;
        audio_Settings.musicVolume_actual = gameSettings.MusicVolume;
        audio_Settings.dialoguesVolume_actual = gameSettings.DialoguesVolume;
        #endregion Audio
        #region Controls
        //Look Sensitivity//
        controls_Settings.lookSensitivity_x_actual = gameSettings.LookSensitivity_X;
        controls_Settings.lookSensitivity_y_actual = gameSettings.LookSensitivity_Y;
        //Reverse Y axis//
        controls_Settings.inverseYAxis_actual = gameSettings.Inverse_Y_Axis;
        #endregion Controls
        SettingsExecution();
    }
    #endregion Load Settings Functions

    #region Settings Execution Functions
    public void DefloatSettings() //Sets all settings to defloat, then save//
    {
        video_Settings.VideoDefaultValueSet();
        audio_Settings.AudioDefaultValueSet();
        controls_Settings.ControlsDefaultValueSet();
        SaveSettings();
    }

    public void SettingsExecution() //Executing all settings//
    {
        video_Settings.ExecuteVideoSettings();
        audio_Settings.ExecuteAudioSettings();
    }

    #endregion Setting Execution Functions

    public class GameSettings
    {
        #region Video
        public int ScreenMode;

        public int Resolution;

        public bool CustomResolution;
        public int CustomResolution_Height;
        public int CustomResolution_Width;

        public int VSync;

        public bool LimitFrameRate;
        public int MaxFrameRate;
        #endregion Video

        #region Audio
        public bool MuteAudio;

        public int MasterVolume;
        public int SFXVolume;
        public int MusicVolume;
        public int DialoguesVolume;
        #endregion Audio

        #region Controls
        public float LookSensitivity_X;
        public float LookSensitivity_Y;
        public bool Inverse_Y_Axis;
        #endregion Controls
    }

}
