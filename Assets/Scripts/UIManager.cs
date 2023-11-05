using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("InGamePanel")]
    public GameObject inGamePanel;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text targetCountText;
    public TMP_Text weaponText;
    public Image timerFill;

    [Header("GameOverPanel")]
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;

    private void Start()
    {
        if (_GM.gameState == GameState.Playing)
        {
            UpdateScore(0);
            UpdateTime(0f, 30f);
            UpdateTargetCount(0);
            _GM.isTiming = true;
        }
    }

    public void UpdateScore(int _score)
    {
        scoreText.text = "Score: " + _score.ToString();
    }

    public void UpdateTime(float _time, float _startTime)
    {
        timeText.text = _time.ToString("F2");
        timerFill.fillAmount = MapTo01(_time, 0, _startTime);
    }

    public void UpdateTargetCount(int _targetCount)
    {
        targetCountText.text = "Target Count: " + _targetCount.ToString();
    }

    public void UpdateWeapon(GameObject _projectile)
    {
        weaponText.text = "Weapon: " + _projectile.name;
    }

    public void GameOver(int _finalScore)
    {
        Time.timeScale = 0;
        inGamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + _finalScore.ToString();
    }
}
