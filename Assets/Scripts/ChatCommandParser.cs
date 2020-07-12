using System;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Unity;
using UnityEngine;
using UnityEngine.Video;

public class ChatCommandParser : MonoBehaviour
{
    private static ChatCommandParser _instance;

    private float _helpCooldown;
    private float _helpTimer;

    private Dictionary<string, Dictionary<Cmd, float>> _playerTimers;

    public Client twitchClient;

    public static ChatCommandParser Instance { get { return _instance; } }

    private List<string> _joinedPlayers;
    private List<string> _goodPlayers;
    private List<string> _evilPlayers;

    private void Start()
    {
        _instance = this.GetComponent<ChatCommandParser>();

        _joinedPlayers = new List<string>(Config.MaxPlayers);
        _goodPlayers = new List<string>((int)Math.Ceiling(Config.MaxPlayers / 2f));
        _evilPlayers = new List<string>((int)Math.Floor(Config.MaxPlayers / 2f));

        _playerTimers = new Dictionary<string, Dictionary<Cmd, float>>();

        GameLogic.PreperationStateTriggered += OnPreparationStart;
        GameLogic.PlayingStateTriggered += OnPlayStart;
        GameLogic.GameOverStateTriggered += OnGameOver;
    }

    private void Update()
    {
        if (_helpTimer > 0)
            _helpTimer -= Time.deltaTime;

        foreach (var playerKey in _playerTimers.Keys.ToList())
        {
            foreach (var cmdKey in _playerTimers[playerKey].Keys.ToList())
            {
                _playerTimers[playerKey][cmdKey] -= Time.deltaTime;
            }
        }
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

        Config.PlayerCount = _joinedPlayers.Count;
        InteractableManager.instance.StartGame(Config.PlayerCount);
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
        }

        switch (cmd)
        {
            case MsgCmd.move:
                if (GameLogic.State == GameState.Playing)
                {
                    if (team == TEAM.BOTH)
                        return;

                    Cmd subcmd;
                    if (!(Enum.TryParse(commands[2], out subcmd)))
                        return;

                    if (IsPlayerBlockedByCooldown(name, subcmd))
                        return;

                    SetPlayerCmdCooldown(name, subcmd);

                    InteractableManager.instance.DistributeCommand(subcmd, commands[1].ToUpper(), team);
                }
                break;

            case MsgCmd.arm:
                if (GameLogic.State == GameState.Playing)
                {
                    if (team == TEAM.BOTH)
                        return;

                    if (IsPlayerBlockedByCooldown(name, Cmd.arm))
                        return;

                    SetPlayerCmdCooldown(name, Cmd.arm);

                    InteractableManager.instance.DistributeCommand(Cmd.arm, commands[1].ToUpper(), team);
                }
                break;

            case MsgCmd.disarm:
                if (GameLogic.State == GameState.Playing)
                {
                    if (team == TEAM.BOTH)
                        return;

                    if (IsPlayerBlockedByCooldown(name, Cmd.disarm))
                        return;

                    SetPlayerCmdCooldown(name, Cmd.disarm);

                    InteractableManager.instance.DistributeCommand(Cmd.disarm, commands[1].ToUpper(), team);
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
                    if (team != TEAM.GOOD)
                        return;

                    if (IsPlayerBlockedByCooldown(name, Cmd.pause))
                        return;

                    SetPlayerCmdCooldown(name, Cmd.pause);

                    InteractableManager.instance.DistributeCommand(Cmd.pause, "", team);
                }
                break;

            case MsgCmd.help:
                if (_helpTimer <= 0)
                {
                    _helpTimer = _helpCooldown;
                    twitchClient.SendMessage(twitchClient.JoinedChannels[0], "This should be helpful huh?");
                }
                break;
        }
    }

    private bool IsPlayerBlockedByCooldown(string playername, Cmd cmd)
    {
        Dictionary<Cmd, float> cmdtimer;

        //player doesn't exist in dict so it is their first cmd
        if (!_playerTimers.TryGetValue(playername, out cmdtimer))
            return false;

        float timer;

        //player did not yet issue this command so there is no cooldown
        if (!cmdtimer.TryGetValue(cmd, out timer))
            return false;

        //the cooldown ran out for this player-cmd
        if (timer <= 0)
            return false;

        return true;
    }

    private void SetPlayerCmdCooldown(string playername, Cmd cmd)
    {
        if (!Config.CmdCooldown.ContainsKey(cmd))
            return;

        if (_playerTimers.ContainsKey(playername))
        {
            if (_playerTimers[playername].ContainsKey(cmd))
            {
                _playerTimers[playername][cmd] = Config.CmdCooldown[cmd];
            }
            else
            {
                _playerTimers[playername].Add(cmd, Config.CmdCooldown[cmd]);
            }
        }
        else
        {
            var dict = new Dictionary<Cmd, float>();
            dict.Add(cmd, Config.CmdCooldown[cmd]);
            _playerTimers.Add(playername, dict);
        }
    }

    private enum MsgCmd
    {
        none = -1,
        move,
        arm,
        disarm,
        pause,
        help,
        join
    }
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
    pause
}