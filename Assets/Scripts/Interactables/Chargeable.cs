using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chargeable : Interactable
{
  public LineRenderer ProgressBar;
  protected float currentArmedStatus = 0.0f;
  new void Start()
  {
    commands.Add(Cmd.arm, new Command(Arm));
    commands.Add(Cmd.disarm, new Command(Disarm));
    InteractableManager.instance.Register(this);
  }

  void Update()
  {
    if (currentlyActiveCooldown > 0)
    {
      currentlyActiveCooldown -= Time.deltaTime;
    }
    if (currentReactivationCooldown > 0)
    {
      currentReactivationCooldown -= Time.deltaTime;
    }
  }

  protected void Arm(string message)
  {
    //TODO: add the possibility to "overheat"
    if (currentlyActiveCooldown > 0 || currentReactivationCooldown > 0)
    {
      return;
    }
    currentArmedStatus += commandWeight;
    Mathf.Clamp(currentArmedStatus, 0, 2);

    if(currentArmedStatus > activationThreshold){
      currentlyActiveCooldown = activeDuration;
      Activate();
    }
    
  }
  protected void Disarm(string message)
  {
    if (currentlyActiveCooldown > 0 || currentReactivationCooldown > 0)
    {
      return;
    }
    currentArmedStatus -= commandWeight;
    Mathf.Clamp(currentArmedStatus, 0, 2);
  }
  abstract protected void Activate();
}
