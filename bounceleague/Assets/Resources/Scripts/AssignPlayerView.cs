using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignPlayerView : MonoBehaviour
{
    public Transform overlay;
    public Transform popup;
    public TextMeshProUGUI title;

    GameManager gameManager;

    public void DoInit(GameManager inGameManager)
    {
        gameManager = inGameManager;
    }

    public void UpdateAssignInfo()
    {
        title.text = string.Format("Player {0}\nPress any key to join", (gameManager.characterIndex + 1));
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
