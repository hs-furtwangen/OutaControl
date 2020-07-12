using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : Chargeable
{
  public GameObject Flame;
  private new void Start()
  {
    identifier = "F";
    // reactivationCooldown = 20;
    // activeDuration = 10;
    base.Start();
  }
  protected override void Activate()
  {
    Debug.Log("Activate");
    Flame.SetActive(true);
  }
  protected override void Enable(){}
  protected override void Deactivate()
  {
    Debug.Log("Deactivate");
    Flame.SetActive(false);
  }
}
