using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Title, Playing, Paused, GameOver }
public enum Difficulty { Easy, Medium, Hard }

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;
    public int score = 0;
    int scoreMultiplier = 1;
    public float timer = 30f;
    public float startTime = 30f;
    public bool isTiming;

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (!isTiming)
            return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            _UI.UpdateTime(timer, startTime);
        }
        else
        {
            ChangeGameState(GameState.GameOver);
            _UI.GameOver(score);
        }
    }

    public void SetDifficulty(int _difficulty)
    {
        switch (_difficulty)
        {
            case 0:
                difficulty = Difficulty.Easy;
                scoreMultiplier = 1;
                break;
            case 1:
                difficulty = Difficulty.Medium;
                scoreMultiplier = 2;
                break;
            case 2:
                difficulty = Difficulty.Hard;
                scoreMultiplier = 3;
                break;
        }
    }

    void OnTargetHit(GameObject _target)
    {
        timer += 5f;
        score += 100 * scoreMultiplier;

        _UI.UpdateTime(timer, startTime);
        _UI.UpdateScore(score);
    }

    private void OnEnable()
    {
        Target.OnTargetHit += OnTargetHit;
    }

    private void OnDisable()
    {
        Target.OnTargetHit -= OnTargetHit;
    }

    public void ChangeGameState(GameState _gameState)
    {
        gameState = _gameState;
    }
}
