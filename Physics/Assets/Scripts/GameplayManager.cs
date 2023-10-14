using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private Text timerText;
    public int gameTimeLimit = 60;
    private int timeRemaining;
    private int score = 0;

    //public Text scoreText;

    void Start()
    {
        timeRemaining = gameTimeLimit;
        StartCoroutine(UpdateTimerUI());
        //UpdateScoreUI();
    }

    private IEnumerator UpdateTimerUI()
    {
        while(timeRemaining >= 0)
        {
            timerText.text = $"{timeRemaining / 60:00}:{timeRemaining % 60:00}";
            uiFill.fillAmount = Mathf.InverseLerp(0, gameTimeLimit, timeRemaining);
            timeRemaining--;
            yield return new WaitForSeconds(1f);
        }

        OnEnd();
    }

    private void OnEnd()
    {
        print("End");
    }

    public void IncreaseScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        //scoreText.text = "Score: " + score;
    }
}
