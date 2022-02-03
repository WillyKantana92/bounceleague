using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignPlayerView : MonoBehaviour
{
    public Transform overlay;
    public Transform popup;
    public Button startButton;
    public List<Image> readyIndicators;

    GameManager gameManager;

    public void DoInit(GameManager inGameManager)
    {
        gameManager = inGameManager;
        for(int i = 0; i < readyIndicators.Count; i++)
        {
            readyIndicators[i].color = Color.red;
        }
        startButton.onClick.AddListener(OnStartButton);
        startButton.gameObject.SetActive(false);
    }

    public void UpdateAssignInfo(int index)
    {
        readyIndicators[index].color = Color.green;
    }

    public void OnStartButton()
    {
        Hide();
        gameManager.GameStart();
    }

    public void ShowStart()
    {
        startButton.gameObject.SetActive(true);
    }

    public void Show()
    {
        overlay.gameObject.SetActive(true);
        popup.gameObject.SetActive(true);
    }

    public void Hide()
    {
        overlay.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
    }
}
