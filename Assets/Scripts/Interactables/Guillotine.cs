using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guillotine : Chargeable
{
  public Animator BladeAnimator;
  private new void Start()
  {
    identifier = "G";
    base.Start();
  }
  
  protected override void Activate()
  {
    BladeAnimator.SetTrigger("PlayAnimation");
  }
  protected override void Enable() { }
  protected override void Deactivate()
  {
  }
}
