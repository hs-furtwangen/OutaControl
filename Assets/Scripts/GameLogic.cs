using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum GameState
{
    Perperation,
    Playing,
    GameOver
}

public class GameLogic : MonoBehaviour
{
    private static GameState _state;

    public float TimeToWinInSeconds = 300;

    public float TimeRemaining;

    public static EventHandler<EventArgs> PreperationStateTriggered;
    public static EventHandler<EventArgs> PlayingStateTriggered;
    public static EventHandler<EventArgs> GameOverStateTriggered;

    private static float gameStartTime;

    public static GameState State
    {
        get => _state;
        set
        {
            _state = value;

            switch (_state)
            {
                case GameState.Perperation:
                    PreperationStateTriggered?.Invoke(null, EventArgs.Empty);
                    break;
                case GameState.Playing:
                    PlayingStateTriggered?.Invoke(null, EventArgs.Empty);
                    break;
                case GameState.GameOver:
                    GameOverStateTriggered?.Invoke(null, EventArgs.Empty);
                    break;
            }
        }
    }

    GameLogic()
    {
        PreperationStateTriggered += (s, e) => Debug.Log("PerperationStateTriggered");
        PlayingStateTriggered += (s, e) => Debug.Log("PlayingStateTriggered");
        GameOverStateTriggered += (s, e) => Debug.Log("GameOverStateTriggered");
    }

    private void Update()
    {
        var remainingTime = Mathf.RoundToInt(TimeToWinInSeconds - (Time.realtimeSinceStartup - gameStartTime));
        TimeRemaining = remainingTime < 0 ? 0 : remainingTime;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // call this at level start
        //StartGameTimer();
    }

    public void StartGameTimer()
    {
        gameStartTime = Time.realtimeSinceStartup;
        StartCoroutine(CheckForTimeout());
    }

    public void StopGameTimer()
    {
        StopCoroutine(CheckForTimeout());
    }

    public void ResetGameTimer()
    {
        StopGameTimer();
        StartGameTimer();
    }

    private IEnumerator CheckForTimeout()
    {
        for (;;)
        {
            if (Time.realtimeSinceStartup - gameStartTime > TimeToWinInSeconds)
            {
                Debug.LogError("TIME IS OVER!");
                State = GameState.GameOver;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}

