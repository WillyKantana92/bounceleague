using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Team
{
    TeamA, TeamB
}

public class GameManager : MonoBehaviour
{
    public TestBallScript ballScript;
    public Character characterA;
    public Character characterB;

    [Header("Score")]
    public const int ScoreToWin = 3;
    public int teamAScore;
    public int teamBScore;

    [Header("UI")]
    public Text scoreText;
    
    Vector2 move;

    void Awake()
    {
        ballScript.Init(this);
        RestartGame();
    }

    void FixedUpdate()
    {
        
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    public void OnGoal(Team team)
    {
        if(team == Team.TeamA)
        {
            teamAScore++;
            RefreshScoreText();
            
            if(teamAScore >= ScoreToWin)
            {
                //! Game end, team A win
            }
            else
            {
                //! Next round
            }
        }
        else
        {
            teamBScore++;
            RefreshScoreText();
            
            if(teamBScore >= ScoreToWin)
            {
                //! Game end, team B win
            }
            else
            {
                //! Next round
            }
        }
    }

    void RefreshScoreText()
    {
        scoreText.text = string.Format("{0} - {1}", teamAScore, teamBScore);
    }

    public void RestartGame()
    {
        teamAScore = 0;
        teamBScore = 0;
        RefreshScoreText();
        ballScript.ResetBall();
    }

    public void NextRound()
    {
        ballScript.ResetBall();
        
    }
}
