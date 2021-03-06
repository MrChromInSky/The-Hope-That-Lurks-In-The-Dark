using System.Collections;
using UnityEngine;

public class Video_Settings : MonoBehaviour
{
    #region Video Settings
    //Actual Settings//
    #region Actual Settings
    //Screen Mode//
    public int screenMode_actual;

    //Resolution//
    public int resolution_actual;

    //Custom resolution//
    public bool customResolution_actual;
    public int customResolutionHeight_actual;
    public int customResolutionWidth_actual;

    //V Sync//
    public int vSync_actual;

    //FPS Limit//
    public bool limitFramerate_actual;
    public int maxFramerate_actual;


    #endregion

    //Defloat Settings//
    #region Defloat Video Settings

    //Screen Mode//
    public readonly int screenMode_default = 0;

    //Resolution//
    public readonly int resolution_default = 2;

    //Custom resolution//
    public readonly bool customResolution_default = false;
    public readonly int customResolutionHeight_default = 0;
    public readonly int customResolutionWidth_default = 0;

    //V Sync//
    public readonly int vSync_default = 0;

    //FPS Limit//
    public readonly bool limitFramerate_default = false;
    public readonly int maxFramerate_default = 0;

    #endregion Defloat Video Settings 

    //Temporary Settings//
    #region Temporary Settings
    //Screen Mode//
    [HideInInspector]public int screenMode_temporary;

    //Resolution//
    [HideInInspector] public int resolution_temporary;

    //Custom resolution//
    [HideInInspector] public bool customResolution_temporary;
    [HideInInspector] public int customResolutionHeight_temporary;
    [HideInInspector] public int customResolutionWidth_temporary;

    //V Sync//
    [HideInInspector] public int vSync_temporary;

    //FPS Limit//
    [HideInInspector] public bool limitFramerate_temporary;
    [HideInInspector] public int maxFramerate_temporary;


    #endregion Temporary Settings

    //Resolutions//
    int[] resolutionHeight = { 720, 900, 1080, 1440, 2160 };
    int[] resolutionWidth = { 1280, 1600, 1920, 2560, 3840 };

    //Windowed?//
    bool fullScreenMode;

    #endregion Settings

    public void VideoDefaultValueSet() //Settings Video options to Defloat values//
    {
        //Screen Mode//
        screenMode_actual = screenMode_default;

        //Resolution//
        resolution_actual = resolution_default;

        //Custom resolution//
        customResolution_actual = customResolution_default;
        customResolutionHeight_actual = customResolutionHeight_default;
        customResolutionWidth_actual = customResolutionWidth_default;

        //V Sync//
        vSync_actual = vSync_default;

        //FPS Limit//
        limitFramerate_actual = limitFramerate_default;
        maxFramerate_actual = maxFramerate_default;
    }

    public void ExecuteVideoSettings() //Executing Video Settings in Engine//
    {
        

        #region Screen Mode
        StartCoroutine("SetFullScreenWindowMode");

        //Screen mode// //0 ? Exclusive Full Screen, 1 ? Full Screen Windowed, 2 ? Maximized Window, 3 ? Window
        if (screenMode_actual == 0)
        {
            fullScreenMode = true;
        }
        else if (screenMode_actual == 1)
        {
            //To Set Full Screen Windowed, need Delay//
            StartCoroutine("SetFullScreenWindowMode"); 
        }
        else if (screenMode_actual == 2)
        {
            fullScreenMode = false;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            screenMode_actual = screenMode_default;
        }
        #endregion Screen Mode


        #region Resolution
        //Setting Resolution//
        if (customResolution_actual)
        {
            Screen.SetResolution(customResolutionWidth_actual, customResolutionHeight_actual, fullScreenMode);
        }
        else if (!customResolution_actual)
        {
            if (resolution_actual >= 0 && resolution_actual <= (resolutionHeight.Length - 1))
            {
                Screen.SetResolution(resolutionWidth[resolution_actual], resolutionHeight[resolution_actual], fullScreenMode);
            }
            else
            {
                resolution_actual = resolution_default;
                Screen.SetResolution(resolutionWidth[2], resolutionHeight[2], fullScreenMode);
            }
        }
        else //if someone broke config file//
        {
            customResolution_actual = customResolution_default;
            Screen.SetResolution(resolutionWidth[2], resolutionHeight[2], fullScreenMode);
        }
        #endregion Resolution

        #region VSync & Framerate Limit
        if (limitFramerate_actual)
        {
            Application.targetFrameRate = maxFramerate_actual;
        }
        else if (!limitFramerate_actual)
        {
            if (vSync_actual == 0)
            {
                QualitySettings.vSyncCount = 0;
            }
            else if (vSync_actual == 1)
            {
                QualitySettings.vSyncCount = 1;
            }
            else if (vSync_actual == 2)
            {
                QualitySettings.vSyncCount = 2;
            }
            else if (vSync_actual == 3)
            {
                QualitySettings.vSyncCount = 3;
            }
            else if (vSync_actual == 4)
            {
                QualitySettings.vSyncCount = 4;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
                vSync_actual = vSync_default;
            }
        }
        else //if someone broke config file//
        {
            limitFramerate_actual = limitFramerate_default;
            QualitySettings.vSyncCount = 0;
        }
        #endregion VSync & FPS Limiter
    }

    IEnumerator SetFullScreenWindowMode()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }


    public void TemporaryToActual()
    {
        //Screen Mode//
        screenMode_actual = screenMode_temporary;

        //Resolution//
        resolution_actual = resolution_temporary;

        //Custom resolution//
        customResolution_actual = customResolution_temporary;
        customResolutionHeight_actual = customResolutionHeight_temporary;
        customResolutionWidth_actual = customResolutionWidth_temporary;

        //V Sync//
        vSync_actual = vSync_temporary;

        //FPS Limit//
        limitFramerate_actual = limitFramerate_temporary;
        maxFramerate_actual = maxFramerate_temporary;
    }

    public void ActualToTemporary()
    {
        //Screen Mode//
        screenMode_temporary = screenMode_actual;

        //Resolution//
        resolution_temporary = resolution_actual;

        //Custom resolution//
        customResolution_temporary = customResolution_actual;
        customResolutionHeight_temporary = customResolutionHeight_actual;
        customResolutionWidth_temporary = customResolutionWidth_actual;

        //V Sync//
        vSync_temporary = vSync_actual;

        //FPS Limit//
        limitFramerate_temporary = limitFramerate_actual;
        maxFramerate_temporary = maxFramerate_actual;
    }
}
