using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UITitle : GameBehaviour
{
    [SerializeField] private TMP_Dropdown difficultyDropdown;

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
        _GM.ChangeGameState(GameState.Playing);
        Time.timeScale = 1;
        _GM.timer = _GM.startTime;
        _GM.isTiming = false;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
        _GM.ChangeGameState(GameState.Title);
        Time.timeScale = 0;
        _GM.timer = _GM.startTime;
        _GM.isTiming = false;

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeDifficulty()
    {
        _GM.SetDifficulty(difficultyDropdown.value);
    }
}
