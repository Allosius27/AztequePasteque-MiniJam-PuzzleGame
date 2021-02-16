using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{
    public int stockBalls = 10;
    public int score = 0;

    public int scoreToAdd = 0;

    public Text textStockBalls;
    public Text textScore;
    public Text textCurrentLevel;

    public Level currentLevel;
    public Level[] levels;
    public static int s_current_level;

    public Launcher launcher;
    public PointsNumber pointsNumbers;
    //public Canvas uiCanvas;
    public GameObject dynamics;

    public int currentLevelIndex = 0;
    

    public static GameCore instance;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetStockBalls(0);
        SetScore(0);

        currentLevel = levels[s_current_level];
        currentLevel.gameObject.SetActive(true);
        textCurrentLevel.text = currentLevel.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStockBalls(int amount)
    {
        this.stockBalls += amount;
        textStockBalls.text = stockBalls.ToString();
    }

    public void SetScore(int amount)
    {
        this.score += amount;
        textScore.text = score.ToString();
    }

    public void DisplayPointsScore(Transform target, int amount)
    {
        PointsNumber display = Instantiate(pointsNumbers, target.transform.position, target.transform.rotation);
        display.SetPoints(amount);
        display.gameObject.transform.SetParent(dynamics.transform);
    }

    public void NextLevel()
    {
        s_current_level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        return;
    }
}
