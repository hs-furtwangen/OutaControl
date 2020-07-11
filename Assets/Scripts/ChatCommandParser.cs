using UnityEngine;

public static class ChatCommandParser
{
    public static void Parse(string name, string msg)
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
