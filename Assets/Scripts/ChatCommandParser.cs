using System;
using System.Collections.Generic;
using TwitchLib.Api.Models.Helix.Games.GetGames;
using TwitchLib.Api.Models.v5.Teams;
using TwitchLib.Client.Models;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class ChatCommandParser
{
    private static ChatCommandParser _instance;

    public static ChatCommandParser Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ChatCommandParser();
            }
            return _instance;
        }
    }

    private List<string> _joinedPlayers;
    private List<string> _goodPlayers;
    private List<string> _evilPlayers;

    public ChatCommandParser()
    {
        _joinedPlayers = new List<string>(Config.MaxPlayers);
        _goodPlayers = new List<string>((int)Math.Ceiling(Config.MaxPlayers / 2f));
        _evilPlayers = new List<string>((int)Math.Floor(Config.MaxPlayers / 2f));

        GameLogic.PreperationStateTriggered += OnPreparationStart;
        GameLogic.PlayingStateTriggered += OnPlayStart;
        GameLogic.GameOverStateTriggered += OnGameOver;
    }

    private void OnPreparationStart(object sender, EventArgs e)
    {
        _joinedPlayers.Clear();
        _goodPlayers.Clear();
        _evilPlayers.Clear();
    }

    private void OnPlayStart(object sender, EventArgs e)
    {
        _joinedPlayers.Shuffle();
        (_goodPlayers, _evilPlayers) = _joinedPlayers.SplitInHalf();
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        
    }

    public void Parse(string name, string msg)
    {
        var commands = msg.ToLower().Substring(1).Split();

        if (commands.Length > 3)
            return;

        MsgCmd cmd = MsgCmd.none;

        if (!(System.Enum.TryParse(commands[0], out cmd)))
            return;

        TEAM team = TEAM.BOTH;

        if (GameLogic.State == GameState.Playing)
        {
            if (_goodPlayers.Contains(name))
            {
                team = TEAM.GOOD;
            }
            else if (_evilPlayers.Contains(name))
            {
                team = TEAM.BAD;
            }
            else
            {
                return;
            }
        }

        switch (cmd)
        {
            case MsgCmd.move:
                if (GameLogic.State == GameState.Playing)
                {
                    var subcmd = Cmd.none;
                    if (!(System.Enum.TryParse(commands[0], out subcmd)))
                        return;

                    InteractableManager.instance.DistributeCommand(subcmd, commands[1], team);
                }
                break;

            case MsgCmd.activate:
                if (GameLogic.State == GameState.Playing)
                {

                }
                break;

            case MsgCmd.join:
                if (GameLogic.State == GameState.Perperation && !_joinedPlayers.Contains(name) && _joinedPlayers.Count < Config.MaxPlayers)
                {
                    _joinedPlayers.Add(name);
                    Debug.Log($"{name} joined");
                }
                break;

            case MsgCmd.pause:
                if (GameLogic.State == GameState.Playing)
                {

                }
                break;

            case MsgCmd.help:
                break;
        }
    }
}

public enum MsgCmd
{
    none = -1,
    move,
    activate,
    pause,
    help,
    join
}

public enum Cmd
{
    none = -1,
    up,
    down,
    left,
    right,
    arm,
    disarm,
}