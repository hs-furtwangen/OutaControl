using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Interactable
{
  int counter = 0;
  new void Start()
  {
    identifier = 'P';
    commands.Add("up", new Command(up));
    base.Start();
  }

  // Update is called once per frame
  void Update()
  {
    counter++;
    if (counter == 300)
    {
      InteractableManager.instance.DistributeCommand("P1", "up");
    }
    
  }


  private void up(string message)
  {
    Debug.Log(this.gameObject);
  }
}
