using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Settings : MonoBehaviour
{
    #region Assignments

    //Fmod Banks//
    FMOD.Studio.Bus masterBase;
    FMOD.Studio.Bus sfxBase;
    FMOD.Studio.Bus musicBase;
    FMOD.Studio.Bus dialoguesBase;
    FMOD.Studio.Bus testsBase;
    void Awake()
    {


        //Fmod//
        masterBase = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        sfxBase = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        musicBase = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        dialoguesBase = FMODUnity.RuntimeManager.GetBus("bus:/Master/Dialogues");
        testsBase = FMODUnity.RuntimeManager.GetBus("bus:/Master/Tests");
    }
    #endregion Assignments

    #region Audio Settings
    //Mute Sound//
    public bool muteSound_actual;

    //Volumes//
    [Space, Header("Volume Levels")]
    [Range(0, 100)] public int masterVolume_actual;
    [Range(0, 100)] public int sfxVolume_actual;
    [Range(0, 100)] public int musicVolume_actual;
    [Range(0, 100)] public int dialoguesVolume_actual;

    public int testsVolume;
    #endregion Audio Settings

    #region Audio Settings Default
    //Mute Sound//
    readonly bool muteSound_default;

    //Volumes//
    readonly int masterVolume_default = 50;
    readonly int sfxVolume_default = 100;
    readonly int musicVolume_default = 100;
    readonly int dialoguesVolume_default = 100;
    #endregion Audio Settings Default

    #region Audio Settings Temporary
    [Header("Temporary Settings")]
    //Mute Sound//
    public bool muteSound_temporary;

    //Volumes//
    [Range(0, 100)] public int masterVolume_temporary;
    [Range(0, 100)] public int sfxVolume_temporary;
    [Range(0, 100)] public int musicVolume_temporary;
    [Range(0, 100)] public int dialoguesVolume_temporary;
    #endregion Audio Settings Temporary
    public void AudioDefaultValueSet()
    {
        muteSound_actual = muteSound_default;

        masterVolume_actual = masterVolume_default;
        sfxVolume_actual = sfxVolume_default;
        musicVolume_actual = musicVolume_default;
        dialoguesVolume_actual = dialoguesVolume_default;
    }

    public void TemporaryToActual()
    {
        muteSound_actual = muteSound_temporary;

        masterVolume_actual = masterVolume_temporary;
        sfxVolume_actual = sfxVolume_temporary;
        musicVolume_actual = musicVolume_temporary;
        dialoguesVolume_actual = dialoguesVolume_temporary;
    }

    public void ActualToTemporary()
    {
        muteSound_temporary = muteSound_actual;

        masterVolume_temporary = masterVolume_actual;
        sfxVolume_temporary = sfxVolume_actual;
        musicVolume_temporary = musicVolume_actual;
        dialoguesVolume_temporary = dialoguesVolume_actual;
    }

    public void ExecuteAudioSettings()
    {
        #region New Audio Volume 
        //Final Master Volume//
        float newMasterVolume = (masterVolume_actual * 0.01f);
        //Final SFX Volume//
        float newSFXVolume = (sfxVolume_actual * 0.01f);
        //Final Music Volume//
        float newMusicVolume = (musicVolume_actual * 0.01f);
        //Final Dialogues Volume//
        float newDialoguesVolume = (dialoguesVolume_actual * 0.01f);
        #endregion New Audio Volume


        if (!muteSound_actual) //If false
        {
            #region Set New Volumes
            //Master//
            masterBase.setVolume(newMasterVolume);
            //SFX//
            sfxBase.setVolume(newSFXVolume);
            //Music//
            musicBase.setVolume(newMusicVolume);
            //Dialogues//
            dialoguesBase.setVolume(newDialoguesVolume);
            #endregion Set New Volumes
        }
        else //Mute audio
        {
            #region Mute
            //Master//
            masterBase.setVolume(0);
            //SFX//
            sfxBase.setVolume(0);
            //Music//
            musicBase.setVolume(0);
            //Dialogues//
            dialoguesBase.setVolume(0);
            #endregion Mute
        }


    }



    private void Update()
    {
        ExecuteAudioSettings();
        float newTest = testsVolume * 0.01f;
        testsBase.setVolume(newTest);
    }

}
