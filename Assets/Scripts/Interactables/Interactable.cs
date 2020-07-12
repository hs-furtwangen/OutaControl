using System.Collections;
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
  public float commandWeight = 1.0f;
  private string fullIdentifier = "";
  protected Dictionary<Cmd, Command> commands = new Dictionary<Cmd, Command>();
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
    //Set name and color of the name
    Color badTeamColor = new Color(1, 0, 0, 1);
    Color goodTeamColor = new Color(0, 0, 1, 1);
    Color neutralTeamColor = new Color(1, 1, 1, 1);
    this.fullIdentifier = name;
    NameTextObject.text = name;
    NameTextObject.color = neutralTeamColor;
    if (allowedTeam == TEAM.BAD)
      NameTextObject.color = badTeamColor;
    if (allowedTeam == TEAM.GOOD)
      NameTextObject.color = goodTeamColor;
  }

  public void SetCommandWeight(int activePlayerCount)
  {
    commandWeight = 1.0f / activePlayerCount;
  }

  public void DoCommand(Cmd cmd, string message = "")
  {
    if (commands.ContainsKey(cmd))
    {
      commands[cmd](message);
    }
  }
}
