﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{
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
    //public Canvas uiCanvas;
    public GameObject dynamics;

    public ScoreObtained scoreObtained;
    public bool scoreObtainedActive;
    public int scoreAddingValue = 100;

    public BoardScore boardScore;
    public bool setTotalScoreActive;
    int actualTotalScore = 0;

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
        SetTotalScore(0);
        textTotalScore.text = totalScore.ToString();

        currentLevel = levels[s_current_level];
        currentLevel.gameObject.SetActive(true);
        textCurrentLevel.text = currentLevel.name;

        scoreObtained.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
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
        if (value >= amount)
        {
            setScoreAmount = 0;
            value = 0;
            return;
        }

        this.score += scoreAddingValue;
        textScore.text = score.ToString();
        boardScore.textScoreAmount.text = score.ToString();

        value += scoreAddingValue;

        
    }

    public void SetTotalScore(int actualtotalScore)
    {

        if (totalScore >= (actualtotalScore + score))
        {
            setTotalScoreActive = false;
            return;
        }

        totalScore += scoreAddingValue;
        textTotalScore.text = totalScore.ToString();
        boardScore.textScoreTotalAmount.text = totalScore.ToString();

        
    }

    public void DisplayPointsScore(Transform target, int amount)
    {
        PointsNumber display = Instantiate(pointsNumbers, target.transform.position, target.transform.rotation);
        display.SetPoints(amount);
        display.gameObject.transform.SetParent(dynamics.transform);
    }

    public void ScoreObtainedAtLevel()
    {
        if (scoreObtained.scoreobtainedAmount >= score)
        {
            StartCoroutine(ScoreObtainsAtLevelEndDisplay());

            scoreObtainedActive = false;

            return;
        }

        scoreObtained.gameObject.SetActive(true);
        scoreObtained.scoreobtainedAmount += scoreAddingValue;
        Debug.Log(scoreObtained.scoreobtainedAmount);
        scoreObtained.scoreObtainedText.text = scoreObtained.scoreobtainedAmount.ToString();

        
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        return;
    }
}
