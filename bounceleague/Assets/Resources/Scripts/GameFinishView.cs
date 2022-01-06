using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameFinishView : MonoBehaviour
{
    public Transform overlay;
    public Transform popup;
    public TextMeshProUGUI title;
    public Button buttonNext;
    public Button buttonStart;

    GameManager gameManager;

    public void Initialize(GameManager gameManagerIn)
    {
        gameManager = gameManagerIn;
        
        buttonNext.onClick.AddListener(OnButtonNextPressed);
        buttonStart.onClick.AddListener(OnButtonStartPressed);
        
        SetTitle("BOUNCE LEAGUE");
        ShowButtons(true);
        Show();
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

    public void ShowButtons(bool isStartNewGame)
    {
        buttonStart.gameObject.SetActive(isStartNewGame);
        buttonNext.gameObject.SetActive(!isStartNewGame);
    }

    public void SetTitle(string str)
    {
        title.text = str;
    }

    void OnButtonNextPressed()
    {
        Hide();
        gameManager.NextRound();
    }

    void OnButtonStartPressed()
    {
        Hide();
        gameManager.RestartGame();
    }
}
