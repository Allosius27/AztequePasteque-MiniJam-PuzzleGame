using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public static MainMenu instance;

    //public GameObject continueButton;
    //public static bool continueActive;
    public Button buttonContinue;

    public string loadGameScene;

    public GameObject SettingsMenu;
    public bool activeSettings;
    public bool activesButtons = true;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayBGM(0);

        Cursor.SetCursor(null, hotSpot, cursorMode);

        /*if (PlayerPrefs.HasKey("Current_Scene"))
        {
            continueActive = true;
        }
        else
        {
            continueActive = false;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if(continueActive)
        {
            buttonContinue.interactable = true;
        }
        else
        {
            buttonContinue.interactable = false;
        }*/
    }

    public void Continue()
    {
        SceneManager.LoadScene(loadGameScene);
    }

    public void NewGame()
    {
        StartCoroutine(LoadAsynchronously(newGameScene));

    }

    IEnumerator LoadAsynchronously(string SceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

    public void Options()
    {
        Debug.Log("Launch Menu Options");
        SettingsMenu.SetActive(true);
        activesButtons = false;
        activeSettings = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}

