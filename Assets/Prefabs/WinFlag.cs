using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinFlag : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            GameLogic.WinningTeam = TEAM.GOOD;
            GameLogic.State = GameState.GameOver;
        }
    }
