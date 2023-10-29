using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { Easy, Medium, Hard }

public class GameManager : Singleton<GameManager>
{
    public Difficulty difficulty;
    public int score = 0;
    int scoreMultiplier = 1;
    public float timer = 30f;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetKeyDown("1"))
        {
            difficulty = Difficulty.Easy;
            SetDifficulty();
        }
        if (Input.GetKeyDown("2"))
        {
            difficulty = Difficulty.Medium;
            SetDifficulty();
        }
        if (Input.GetKeyDown("3"))
        {
            difficulty = Difficulty.Hard;
            SetDifficulty();
        }
    }

    void SetDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                scoreMultiplier = 1;
                TargetManager.INSTANCE.ResizeTargetsDifficulty(2);
                break;
            case Difficulty.Medium:
                scoreMultiplier = 2;
                TargetManager.INSTANCE.ResizeTargetsDifficulty(1);
                break;
            case Difficulty.Hard:
                scoreMultiplier = 3;
                TargetManager.INSTANCE.ResizeTargetsDifficulty(0);
                break;
        }
    }

    void OnTargetHit(GameObject _target)
    {
        timer += 5f;
        score += 100 * scoreMultiplier;
    }

    private void OnEnable()
    {
        Target.OnTargetHit += OnTargetHit;
    }

    private void OnDisable()
    {
        Target.OnTargetHit -= OnTargetHit;
    }
}
