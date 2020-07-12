using System.Collections.Generic;
using UnityEngine.Windows.Speech;

public static class Config
{
    public static string TwitchUsername;
    public static string TwitchAccessToke;
    public static string TwitchChannel;

    public static int MaxPlayers = 50;
    public static int PlayerCount;


    public static Dictionary<Cmd, float> CmdCooldown = new Dictionary<Cmd, float> 
    {
        { Cmd.arm, 3f },
        { Cmd.disarm, 3f },
        { Cmd.pause, 5f },
        { Cmd.left, 1f },
        { Cmd.right, 1f },
        { Cmd.up, 1f },
        { Cmd.down, 1f },
    };

}
