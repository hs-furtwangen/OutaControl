using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chargeable : Interactable
{
  public int Damage = 0;
  public LineRenderer ProgressBar;
  public float ActivationThreshold = 5f;
  protected float currentArmedStatus = 0.0f;
  protected new void Start()
  {
    commands.Add(Cmd.arm, new Command(Arm));
    commands.Add(Cmd.disarm, new Command(Disarm));
    InteractableManager.instance.Register(this);
    updateProgress(0);
    ProgressBar.startColor = Color.green;
    ProgressBar.endColor = Color.green;
  }

  void Update()
  {
    if (currentlyActiveCooldown > 0)
    {
      currentlyActiveCooldown -= Time.deltaTime;
      if (currentlyActiveCooldown < 0)
      {
        currentReactivationCooldown = reactivationCooldown;
        Deactivate();
        ProgressBar.startColor = Color.red;
        ProgressBar.endColor = Color.red;
      }
    }
    if (currentReactivationCooldown > 0)
    {
      currentReactivationCooldown -= Time.deltaTime;
      if (currentReactivationCooldown < 0)
      {
        ProgressBar.startColor = Color.green;
        ProgressBar.endColor = Color.green;
        currentArmedStatus = 0;
        updateProgress(0);
        Enable();
      }
    }
  }

  protected void Arm(string message)
  {
    //TODO: add the possibility to "overheat"
    if (currentlyActiveCooldown > 0 || currentReactivationCooldown > 0)
    {
      return;
    }
    updateProgress(1);
    if (currentArmedStatus >= 1)
    {
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
    updateProgress(-1);
  }
  private void updateProgress(int sign)
  {
    currentArmedStatus += (sign * commandWeight) / ActivationThreshold;
    Mathf.Clamp(currentArmedStatus, 0, 2);
    ProgressBar.SetPosition(1, Vector3.up * currentArmedStatus);
  }
  abstract protected void Activate();
  abstract protected void Deactivate();
  abstract protected void Enable();

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player") && currentlyActiveCooldown > 0)
    {
      Character player = other.gameObject.GetComponent<Character>();
      player.DealDamage(Damage);
    }
  }
  public new void SetCommandWeight(int activePlayerCount)
  {
    commandWeight = 5.0f / activePlayerCount;
  }
}
