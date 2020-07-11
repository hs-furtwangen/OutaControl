using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager
{
  public static InteractableManager instance = new InteractableManager();
  Dictionary<string, Interactable> interactables = new Dictionary<string, Interactable>();
  Dictionary<char, int> intIndex = new Dictionary<char, int>();

  public void Register(Interactable it)
  {
    int foundAmount = 0;
    if (intIndex.ContainsKey(it.identifier))
    {
      foundAmount = intIndex[it.identifier];
      foundAmount++;
      intIndex[it.identifier]++;
    }
    else
    {
      foundAmount = 1;
      intIndex.Add(it.identifier, foundAmount);
    }
    string newName = it.identifier + foundAmount.ToString();
    interactables.Add(newName, it);
    it.SetName(newName);
  }

  public void StartGame()
  {

  }

  public void DistributeCommand(string objectID, string command, string args = ""){
    if(!interactables.ContainsKey(objectID)) return;
    interactables[objectID].DoCommand(command, args);
  }
}
