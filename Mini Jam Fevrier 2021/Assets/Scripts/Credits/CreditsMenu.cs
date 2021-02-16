using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    //public CursorMode cursorMode = CursorMode.Auto;
    //public Vector2 hotSpot = Vector2.zero;

    public string scene = "TitleScreen";

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.SetCursor(null, hotSpot, cursorMode);

        //AudioManager.instance.PlayBGM(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuButton()
    {
        //EssentialsLoaders.instance.RemoveFromDontDestroyOnLoad();
        // Retourner au menu principal
        SceneManager.LoadScene(scene);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
