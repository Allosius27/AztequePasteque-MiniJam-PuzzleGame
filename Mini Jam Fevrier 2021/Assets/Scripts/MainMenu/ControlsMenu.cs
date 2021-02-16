using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    public SettingsMenu settingsMenu;

    public GameObject Settings;

    public Button generalCommandsButton;
    public GameObject generalsCommands;
    public GameObject generalsCommandsHoverText;

    // Start is called before the first frame update
    void Start()
    {
        //GeneralsControls();
    }

    // Update is called once per frame
    void Update()
    {
        if(settingsMenu.windowControls == true)
        {
            settingsMenu.windowControls = false;
            settingsMenu.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitSettings();
        }
    }

    public void ExitSettings()
    {
        settingsMenu.enabled = true;
        //GeneralsControls();
        gameObject.SetActive(false);
        settingsMenu.ExitSettings();
    }

    public void Return()
    {
        settingsMenu.enabled = true;
        //GeneralsControls();
        gameObject.SetActive(false);
    }
}
