using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interactables
{
  public class Saw : Movable
  {
    private new void Start()
    {
      identifier = "SAW";
      base.Start();
    }
  }
}