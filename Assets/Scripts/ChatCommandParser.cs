using System.Collections.Generic;
using TwitchLib.Client.Models;
using UnityEngine;

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
    private List<string> _goodPlayser;
    private List<string> _evilPlayers;

    public ChatCommandParser()
    {
        _joinedPlayers = new List<string>();
        _goodPlayser = new List<string>();
        _evilPlayers = new List<string>();
    }

    public void Parse(string name, string msg)
    {
        var commands = msg.ToLower().Substring(1).Split();

        if (commands.Length > 3)
            return;

        MsgCmd cmd = MsgCmd.none;

        if (!(System.Enum.TryParse(commands[0], out cmd)))
            return;


        switch (cmd)
        {
            case MsgCmd.move:
                break;

            case MsgCmd.activate:
                break;

            case MsgCmd.join:
                //Check Gamestate for preparing
                if (!_joinedPlayers.Contains(name))
                    _joinedPlayers.Add(name);
                break;

            case MsgCmd.pause:
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
}
