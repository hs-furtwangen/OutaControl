using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum GameState
{
    Perperation,
    Playing,
    GameOver
}

public static class GameLogic
{
    private static GameState _state;

    public static EventHandler<EventArgs> PreperationStateTriggered;
    public static EventHandler<EventArgs> PlayingStateTriggered;
    public static EventHandler<EventArgs> GameOverStateTriggered;

    public static GameState State
    {
        get => _state;
        set
        {
            _state = value;
            
            switch(_state)
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

    static GameLogic()
    {
        PreperationStateTriggered += (s, e) => Debug.Log("PerperationStateTriggered");
        PlayingStateTriggered += (s, e) => Debug.Log("PlayingStateTriggered");
        GameOverStateTriggered += (s, e) => Debug.Log("GameOverStateTriggered");
    }
}
