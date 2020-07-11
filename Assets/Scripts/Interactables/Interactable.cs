﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TEAM
{
  GOOD,
  BAD,
  BOTH
}

public delegate void Command(string message);

public abstract class Interactable : MonoBehaviour
{
  public TextMeshProUGUI NameTextObject;
  public string identifier = "X";
  public float reactivationCooldown = 0;
  public float activeDuration = 0;
  public TEAM allowedTeam = TEAM.BOTH;
  public int activationThreshold = 1;
  public float commandWeight = 1.0f;
  private string fullIdentifier = "";
  protected Dictionary<string, Command> commands = new Dictionary<string, Command>();
  protected float currentReactivationCooldown = 0.0f;
  protected float currentlyActiveCooldown = 0.0f;
  protected void Start()
  {
    InteractableManager.instance.Register(this);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetName(string name)
  {
    this.fullIdentifier = name;
    NameTextObject.text = name;
  }

  public void SetCommandWeight(int activePlayerCount)
  {
    commandWeight = 10.0f / activePlayerCount;
  }

  public void DoCommand(string action, string message = "")
  {
    if (commands.ContainsKey(action))
    {
      commands[action](message);
    }
  }
}
