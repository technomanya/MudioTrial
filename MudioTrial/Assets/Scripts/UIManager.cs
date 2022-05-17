using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public TMP_Text timerText;
    public TMP_Text highScore;
    public TMP_Text waveText;

    public GameObject gameOverPanel;

    public void UpdateTimer()
    {
        if (GameManager.GM.remainingTime > 0)
        {
            GameManager.GM.remainingTime -= Time.deltaTime;
        }
        else
        {
            gameOverPanel.SetActive(true);
        }

        float minutes = Mathf.FloorToInt(GameManager.GM.remainingTime / 60);
        float seconds = Mathf.FloorToInt(GameManager.GM.remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
