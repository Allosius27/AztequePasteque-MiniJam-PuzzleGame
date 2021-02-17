using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreObtained : MonoBehaviour
{
    public TextMeshProUGUI scoreObtainedText;
    public int scoreobtainedAmount;

    public Animator animatorScoreObtainsLabel;
    public Animator animatorScoreObtainsAmount;


    // Start is called before the first frame update
    void Start()
    {
        //scoreObtainedText.text = " ";
        //scoreobtainedAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
