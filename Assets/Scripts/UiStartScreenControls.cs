using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiStartScreenControls : MonoBehaviour
{
    public TMP_InputField UsernameField;
    public TMP_InputField AccessTokenField;
    public TMP_InputField ChannelField;

    public void OnClickStart()
    {
        if (UsernameField.text != "" && AccessTokenField.text != "" && ChannelField.text != "")
        {
            Config.TwitchUsername = UsernameField.text;
            Config.TwitchAccessToke = AccessTokenField.text;
            Config.TwitchChannel = ChannelField.text;

            SceneManager.LoadScene("Twitch");
        }
    }

    public void OnClickTokenlink()
    {
        Application.OpenURL("https://twitchtokengenerator.com/");
    }
}
