using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Interactable
{
  new void Start()
  {
    identifier = 'P';
    commands.Add("up", new Command(up));
    base.Start();
  }

  // Update is called once per frame
  void Update()
  {
    
  }


  private void up(string message)
  {
    Debug.Log(this.gameObject);
  }
}
