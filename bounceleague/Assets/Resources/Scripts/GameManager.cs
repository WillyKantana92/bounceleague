using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum Team
{
    TeamA, TeamB
}

public enum GameState
{
    Home, Join, Gameplay
}

public class GameManager : MonoBehaviour
{
    [HideInInspector] public int characterIndex = 0;
    public TestBallScript ballScript;
    public Character[] characters;

    [Header("Score")]
    public const int ScoreToWin = 3;
    public int teamAScore;
    public int teamBScore;

    [Header("UI")]
    public Text scoreText;
    public GameFinishView gameFinishView;
    public AssignPlayerView assignPlayerView;
    
    Vector2 move;
    public GameState currentGameState;
    public PlayerInputManager playerInputManager;
    public FieldController fieldController;
    public bool gameStart;

    void Awake()
    {
        ballScript.Init(this);
        foreach(Character character in characters)
        {
            character.Init(this);
        }
        gameFinishView.Initialize(this);
        assignPlayerView.DoInit(this);
        assignPlayerView.Hide();
    }

    public void OnGoal(Team team)
    {
        currentGameState = GameState.Home;
        ResetGameObj();
        
        if(team == Team.TeamA)
        {
            teamAScore++;
            RefreshScoreText();
            
            if(teamAScore >= ScoreToWin)
            {
                //! Game end, team A win
                gameFinishView.SetTitle("Team A Won!!");
                gameFinishView.ShowButtons(true);
                gameFinishView.Show();
            }
            else
            {
                //! Next round
                gameFinishView.SetTitle("Point for Team A");
                gameFinishView.ShowButtons(false);
                gameFinishView.Show();
            }
        }
        else
        {
            teamBScore++;
            RefreshScoreText();
            
            if(teamBScore >= ScoreToWin)
            {
                //! Game end, team B win
                gameFinishView.SetTitle("Team B Won!!");
                gameFinishView.ShowButtons(true);
                gameFinishView.Show();
            }
            else
            {
                //! Next round
                gameFinishView.SetTitle("Point for Team B");
                gameFinishView.ShowButtons(false);
                gameFinishView.Show();
            }
        }
    }

    void RefreshScoreText()
    {
        scoreText.text = string.Format("{0} - {1}", teamAScore, teamBScore);
    }

    public void AssignPlayer()
    {
        characters[characterIndex].gameObject.SetActive(true);
        characterIndex++;
        if(characterIndex >= 2)
        {
            assignPlayerView.UpdateAssignInfo(characterIndex-1);
            assignPlayerView.ShowStart();
        }
        else
        {
            assignPlayerView.UpdateAssignInfo(characterIndex-1);
        }
    }

    public void GameStart()
    {
        gameStart = true;
        currentGameState = GameState.Gameplay;
    }

    public void RestartGame()
    {
        teamAScore = 0;
        teamBScore = 0;
        RefreshScoreText();
        playerInputManager.enabled = true;

        if(characterIndex < 2)
        {
            currentGameState = GameState.Join;
            assignPlayerView.Show();
        }
        else
        {
            currentGameState = GameState.Gameplay;
        }
    }

    public void NextRound()
    {
        currentGameState = GameState.Gameplay;
    }

    void ResetGameObj()
    {
        ballScript.ResetBall();
        foreach(Character character in characters)
        {
            character.ResetChar();
        }
    }
}
