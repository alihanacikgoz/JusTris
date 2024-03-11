using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = 0, lines, minLine = 1, maxLine = 5;
    
    public int lineCountInLevel= 5, level = 1;
    
    public TextMeshProUGUI scoreText, levelText, lineText;

    public bool isLevelPassed = false;

    private void Awake()
    {
        ResetTextValues();
    }

    private void ResetTextValues()
    {
        scoreText.text = 0.ToString();
        levelText.text = 1.ToString();
        lines = lineCountInLevel * level;
        UpdateTextsFNC();
    }

    public void LineScore(int n)
    {
        isLevelPassed = false;
        n=Mathf.Clamp(n, minLine, maxLine);
        switch (n)
        {
            case 1:
                score += 30 * level;
                break;
            case 2:
                score += 40 * level;
                break;
            case 3:
                score += 50 * level;
                break;
            case 4:
                score += 60 * level;
                break;
            case 5:
                score += 70 * level;
                break;
        }
        lines -= n;
        if (lines <= 0)
        {
            LevelUpFNC();
        }
        UpdateTextsFNC();
    }

    public void LevelUpFNC()
    {
            level++;
            lines = lineCountInLevel * level;
            isLevelPassed = true;
    }

    private void UpdateTextsFNC()
    {
        if (scoreText) 
            scoreText.text = AddZeroToTheFrontFNC(score,5);

        if (levelText)
            levelText.text = level.ToString();

        if (lineText)
            lineText.text = lines.ToString();
    }
    
    string AddZeroToTheFrontFNC(int score, int numberCount)
    {
        string scoreText = score.ToString();
        while (scoreText.Length < numberCount)
        {
            scoreText = "0" + scoreText;
        }
        return scoreText;
    }
}
