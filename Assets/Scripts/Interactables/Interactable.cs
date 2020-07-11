using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TEAM
{
  GOOD,
  BAD,
  BOTH
}

public delegate void Command(string message);

public abstract class Interactable : MonoBehaviour
{
  public char identifier = 'X';
  public float activationCooldown = 0;
  public float activeDuration = 0;
  public TEAM allowedTeam = TEAM.BOTH;
  public int damage = 0;
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
  }

  public void DoCommand(string action, string message = "")
  {
    if (commands.ContainsKey(action))
    {
      commands[action](message);
    }
  }
}
