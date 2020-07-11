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
  public char identifier = 'X';
  public float activationCooldown = 0;
  public float activeDuration = 0;
  public TEAM allowedTeam = TEAM.BOTH;
  private string fullIdentifier = "";
  protected Dictionary<string, Command> commands = new Dictionary<string, Command>();
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

  public void DoCommand(string action, string message = "")
  {
    if (commands.ContainsKey(action))
    {
      commands[action](message);
    }
  }
}
