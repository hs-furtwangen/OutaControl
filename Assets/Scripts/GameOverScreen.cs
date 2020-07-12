using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var text = GameObject.Find("TeamWinText");
        text.GetComponent<TextMeshProUGUI>().text = $"The {GameLogic.WinningTeam} team won!";

        var charAlive = GameObject.Find("AliveCharacter");
        var charDead = GameObject.Find("DeadCharacter");

        if (GameLogic.WinningTeam == TEAM.GOOD)
            charDead.SetActive(false);
        if(GameLogic.WinningTeam == TEAM.BAD)
            charAlive.SetActive(false);
    }

    public void RestartClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
