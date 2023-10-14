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
    [SerializeField] private Text scoreText;
    [SerializeField] private GameOverScreen gameOverScreen;
    public int gameTimeLimit = 60;
    private int timeRemaining;
    private int score = 0;
    internal bool gameOver = false;
    void Start()
    {
        timeRemaining = gameTimeLimit;
        StartCoroutine(UpdateTimerUI());
        UpdateScoreUI();
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
        gameOver = true;
        gameOverScreen.Setup(score);
    }

    public void IncreaseScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "" + score;
    }
}
