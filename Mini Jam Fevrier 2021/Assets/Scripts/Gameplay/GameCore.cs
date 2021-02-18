using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{
    public float comboScoreMultiplier = 1.0f;
    public int comboNumber = 0;

    public static int totalScore = 0;
    public Text textTotalScore;

    public int stockBalls = 10;
    public int score = 0;
    public int value;
    public int setScoreAmount;

    public int scoreToAdd = 0;

    public Text textStockBalls;
    public Text textScore;
    public Text textCurrentLevel;

    public Level currentLevel;
    public Level[] levels;
    public static int s_current_level;

    public Launcher launcher;
    public PointsNumber pointsNumbers;

    public TextEffect textEffectBallSaver;
    public TextEffect textEffectDoublePoints;
    public TextEffect textEffectTrapShoot;

    //public Canvas uiCanvas;
    public GameObject dynamics;

    public ScoreObtained scoreObtained;
    public bool scoreObtainedActive;
    public int scoreAddingValue = 100;

    public BoardScore boardScore;
    public bool setTotalScoreActive;
    public int actualTotalScore = 0;

    public int currentLevelIndex = 0;
    public string mainMenuScene;

    public bool desactivateArrow = false;

    public GameObject gameOverPanel;
    

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
        
        SetTotalScore(0);
        

        currentLevel = levels[s_current_level];
        currentLevel.gameObject.SetActive(true);
        textCurrentLevel.text = currentLevel.name;

        scoreObtained.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(stockBalls == 0 && currentLevel.listTargetBlocs.Count > 0)
        {
            gameOverPanel.SetActive(true);
        }

        /*if (comboNumber > 0)
        {
            comboScoreMultiplier = 1.0f + (comboNumber / 10.0f);
        }*/

        if(scoreObtainedActive)
        {
            ScoreObtainedAtLevel();
        }

        if (setScoreAmount > 0)
        {
            SetScore(setScoreAmount);
        }

        if (setTotalScoreActive)
        {
            SetTotalScore(actualTotalScore);
        }


    }

    public void SetStockBalls(int amount)
    {
        this.stockBalls += amount;
        textStockBalls.text = stockBalls.ToString();
    }

    public void SetScore(int amount)
    {
        textScore.text = score.ToString();
        boardScore.textScoreAmount.text = score.ToString();

        if (value >= amount)
        {
            setScoreAmount = 0;
            value = 0;
            return;
        }

        this.score += scoreAddingValue;
        

        value += scoreAddingValue;

        
    }

    public void SetTotalScore(int actualtotalScore)
    {

        textTotalScore.text = totalScore.ToString();
        boardScore.textScoreTotalAmount.text = totalScore.ToString();

        if (totalScore >= (actualtotalScore + score))
        {
            setTotalScoreActive = false;
            return;
        }

        totalScore += scoreAddingValue;
    }

    public void DisplayPointsScore(Transform target, int amount)
    {
        PointsNumber display = Instantiate(pointsNumbers, target.transform.position, transform.rotation);
        display.SetPoints(amount);
        display.gameObject.transform.SetParent(dynamics.transform);
    }


    public void DisplayTextEffect(Transform target, TextEffect textEffect)
    {
        TextEffect display = Instantiate(textEffect, target.transform.position, transform.rotation);
        display.SetPoints();
        display.gameObject.transform.SetParent(dynamics.transform);
    }


    public void ScoreObtainedAtLevel()
    {
        scoreObtained.scoreObtainedText.text = scoreObtained.scoreobtainedAmount.ToString();

        if (scoreObtained.scoreobtainedAmount >= score)
        {
            StartCoroutine(ScoreObtainsAtLevelEndDisplay());

            scoreObtainedActive = false;

            return;
        }

        scoreObtained.gameObject.SetActive(true);
        scoreObtained.scoreobtainedAmount += scoreAddingValue;
        //Debug.Log(scoreObtained.scoreobtainedAmount);
        

        
    }

    IEnumerator ScoreObtainsAtLevelEndDisplay()
    {
        yield return new WaitForSeconds(0.25f);
        scoreObtained.animatorScoreObtainsLabel.SetBool("TextLeave", true);
        yield return new WaitForSeconds(0.25f);
        scoreObtained.animatorScoreObtainsAmount.SetBool("TextLeave", true);
        yield return new WaitForSeconds(0.5f);
        scoreObtained.gameObject.SetActive(false);
        BoardScore();
    }

    public void BoardScore()
    {
        boardScore.gameObject.SetActive(true);

        actualTotalScore = totalScore;

        setTotalScoreActive = true;
    }

    public void NextLevel()
    {
        s_current_level++;
        totalScore = actualTotalScore + score;
        desactivateArrow = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        return;
    }

    public void Retry()
    {
        s_current_level = 0;
        totalScore = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMainMenu()
    {
        s_current_level = 0;
        totalScore = 0;

        SceneManager.LoadScene(mainMenuScene);
    }
}
