using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Api.Models.v5.Users;
using System.Collections;
using TwitchLib.Api.Models.Helix.Games.GetGames;
using System;

public class TwitchClient : MonoBehaviour
{
    private Client client;

    [ReadOnly]
    public string channelid;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Application.runInBackground = true;

        ConnectionCredentials credentials = new ConnectionCredentials(Config.TwitchUsername, Config.TwitchAccessToke);

        client = new Client();
        client.Initialize(credentials);

        client.OnConnected += OnConnected;
        client.OnConnectionError += OnConnectionError;
        client.OnDisconnected += OnDisconnected;

        client.OnMessageReceived += OnMessageReceived;
        client.OnMessageSent += OnMessageSent;
        client.OnWhisperReceived += OnWhisperReceived;
        client.OnWhisperSent += OnWhisperSent;

        //Enable for verbose logging
        client.OnLog += OnLog;

        yield return StartCoroutine(ResolveIds());

        client.Connect();

        GameLogic.PreperationStateTriggered += OnPreparationStart;
        GameLogic.PlayingStateTriggered += OnPlayStart;
        GameLogic.GameOverStateTriggered += OnGameOver;
    }

    private void OnPreparationStart(object sender, EventArgs e)
    {
        client.SendMessage(client.JoinedChannels[0], "### Preparation phase has started, please type \"!join\" if you want to play. ###");
    }

    private void OnPlayStart(object sender, EventArgs e)
    {
        client.SendMessage(client.JoinedChannels[0], "### Preparation phase is over, here we gooooo! ###");
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        client.SendMessage(client.JoinedChannels[0], "### Game is over :(, ask your fellow streamer for another round if you liked it, also nothing broke so far yay! ###");
    }

    // Update is called once per frame
    void Update()
    {
        if (channelid == "")
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            client.SendMessage(client.JoinedChannels[0], "viking64Friendo");
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            GameLogic.State = GameState.Perperation;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            GameLogic.State = GameState.Playing;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            GameLogic.State = GameState.GameOver;
        }
    }

    private void OnConnected(object sender, OnConnectedArgs e)
    {
        if (Config.TwitchChannel != "")
            client.JoinChannel(Config.TwitchChannel);
    }

    private void OnConnectionError(object sender, OnConnectionErrorArgs e)
    {
        Debug.LogError("Connection Error: " + e.Error.Message);
    }

    private void OnDisconnected(object sender, OnDisconnectedArgs e)
    {
        Debug.LogWarning("Disconnected");
    }

    void OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
        Debug.Log(e.ChatMessage.Channel + ": " + e.ChatMessage.DisplayName + ": " + e.ChatMessage.Message);

        if (e.ChatMessage.Message.Trim()[0] == '!')
        {
            ChatCommandParser.Instance.Parse(e.ChatMessage.DisplayName, e.ChatMessage.Message);
        }
    }
    void OnMessageSent(object sender, OnMessageSentArgs e)
    {
        Debug.Log(e.SentMessage.Channel + ": " + e.SentMessage.DisplayName + ": " + e.SentMessage.Message);
    }

    void OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
    {
        Debug.Log("Whisper received: " + e.WhisperMessage.Username + ": " + e.WhisperMessage.Message);
    }

    void OnWhisperSent(object sender, OnWhisperSentArgs e)
    {
        Debug.Log("Whisper sent: " + e.Receiver + ": " + e.Message);
    }

    private void OnLog(object sender, OnLogArgs e)
    {
        Debug.Log(e.Data);
    }

    private IEnumerator ResolveIds()
    {
        Api api = new Api();
        api.Settings.AccessToken = TwitchSecrets.ACCESS_TOKEN;
        api.Settings.ClientId = TwitchSecrets.CLIENT_ID;

        yield return api.InvokeAsync(api.Users.v5.GetUserByNameAsync(Config.TwitchChannel), UserResolveCallback);
    }

    private void UserResolveCallback(Users obj)
    {
        if (obj.Total > 0)
            channelid = obj.Matches[0].Id;
    }
}