using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject settingsMenu;
    public GameObject pauseMenuUI;

    //public PlayerController playerControllerBody;
    //public Player playerBody;

    //public PlayerController playerControllerSoul;
    //public Player playerSoul;

    //public Texture2D cursorTextureSight;
    //public CursorMode cursorMode = CursorMode.Auto;
    //public Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            if (gameIsPaused)
            {
                Resume();
                //AudioManager.instance.PlaySFX(7);
            }
            else
            {
                Paused();
                //AudioManager.instance.PlaySFX(7);
            }
        }
    }

    public void Paused()
    {
        //playerBody.enabled = false;
        //playerControllerBody.enabled = false;
        //playerSoul.enabled = false;
        //playerControllerSoul.enabled = false;
        //PlayerMovement.instance.enabled = false;
        //Afficher le menu pause
        pauseMenuUI.SetActive(true);
        //Cursor.SetCursor(null, hotSpot, cursorMode);
        // Arrêter le temps
        Time.timeScale = 0;
        // Changer le statut du jeu (l'état : pause ou jeu actif)
        gameIsPaused = true;

    }

    public void Resume()
    {
        //playerControllerBody.enabled = true;
        //playerBody.enabled = true;
        //playerControllerSoul.enabled = true;
        //playerSoul.enabled = true;
        //PlayerMovement.instance.enabled = true;

        pauseMenuUI.SetActive(false);
        //Cursor.SetCursor(cursorTextureSight, hotSpot, cursorMode);
        settingsMenu.SetActive(false);

        Time.timeScale = 1;

        gameIsPaused = false;

    }

    public void LoadSettings()
    {
        Debug.Log("Loading Settings menu");
        settingsMenu.SetActive(true);
        //pauseMenuUI.SetActive(false);
    }

    public void LoadMainMenu()
    {
        //EssentialsLoaders.instance.RemoveFromDontDestroyOnLoad();
        //Resume();
        //AudioManager.instance.StopMusic();
        //SceneManager.LoadScene("TitleScreen");
        Application.Quit();
    }
}
