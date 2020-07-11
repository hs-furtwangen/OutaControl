using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Api.Models.v5.Users;
using TwitchLib.Api.Models.v5.Chat;
using System.Collections;
using System.Collections.Generic;

public class TwitchClient : MonoBehaviour
{
    private Client client;

    public string channelname;

    [ReadOnly]
    public string channelid;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Application.runInBackground = true;

        ConnectionCredentials credentials = new ConnectionCredentials(TwitchSecrets.USERNAME, TwitchSecrets.ACCESS_TOKEN);

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
    }

    private void OnConnected(object sender, OnConnectedArgs e)
    {
        if (channelname != "")
            client.JoinChannel(channelname);
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
            ChatCommandParser.Parse(e.ChatMessage.DisplayName, e.ChatMessage.Message);
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

        yield return api.InvokeAsync(api.Users.v5.GetUserByNameAsync(channelname), UserResolveCallback);
    }

    private void UserResolveCallback(Users obj)
    {
        if (obj.Total > 0)
            channelid = obj.Matches[0].Id;
    }
}