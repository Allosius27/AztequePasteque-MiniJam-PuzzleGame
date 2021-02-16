using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenuPause : MonoBehaviour
{
    public AudioMixer audioMixer;

    //public Dropdown resolutionDropDown;

    public GameObject Settings;

    public ControlsMenuPause controlsMenuPause;
    public GameObject Controls;
    public bool windowControls;

    //Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        //resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        //resolutionDropDown.ClearOptions();

        /*List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }*/

        //resolutionDropDown.AddOptions(options);
        //resolutionDropDown.value = currentResolutionIndex;
        //resolutionDropDown.RefreshShownValue();

        Screen.fullScreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            ExitSettings();
        }
    }

    /*public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }*/

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void ControlsMenu()
    {
        Controls.SetActive(true);
        controlsMenuPause.GeneralsControls();
        windowControls = true;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void ExitSettings()
    {
        Settings.SetActive(false);
    }
}
