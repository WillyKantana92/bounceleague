using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestBallScript : MonoBehaviour
{
    public Rigidbody ballRb;

    [Header("Goal Animation")]
    public float animDuration = 0.3f;

    bool isAnimatingGoal;
    int force = 100;
    Vector3 initPos;
    GameManager gameManager;

    void Awake()
    {
        isAnimatingGoal = false;
        initPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAnimatingGoal) return;
        if(gameManager.currentGameState != GameState.Gameplay) return;
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ballRb.AddForce(Vector3.left * force);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ballRb.AddForce(Vector3.back * force);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ballRb.AddForce(Vector3.forward * force);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ballRb.AddForce(Vector3.right * force);
        }
    }

    public void Init(GameManager gameManagerIn)
    {
        gameManager = gameManagerIn;
    }

    public void OnGoal(Team team, Vector3 goalPos)
    {
        if(isAnimatingGoal) return;
        Debug.Log("Goal!!");
        
        //! Animate goal
        isAnimatingGoal = true;
        Sequence goalSeq = DOTween.Sequence();
        Vector3 scaleTo = new Vector3(0.5f, 0.5f, 1);
        goalSeq.Join(transform.DOScale(scaleTo, animDuration).SetEase(Ease.InOutQuad));
        goalSeq.Join(transform.DOMove(goalPos, animDuration).SetEase(Ease.InOutQuad));
        goalSeq.AppendCallback(delegate { OnGoalAnimFinish(team); });
    }

    void OnGoalAnimFinish(Team team)
    {
        isAnimatingGoal = false;
        gameManager.OnGoal(team);
    }

    public void ResetBall()
    {
        Debug.Log("Reset ball");
        
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        
        Transform t = transform;
        t.localScale = Vector3.one;
        t.position = initPos;
    }

    public void ForceBall(Vector3 contactPoint)
    {
        // ballRb.velocity = Vector3.zero;
        Vector3 direction = transform.position - contactPoint;
        ballRb.AddForceAtPosition(direction.normalized * force, contactPoint);
    }
}
