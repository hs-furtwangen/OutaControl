using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager
{
  public static InteractableManager instance = new InteractableManager();
  Dictionary<string, Interactable> interactables = new Dictionary<string, Interactable>();
  Dictionary<string, int> intIndex = new Dictionary<string, int>();

  public void Register(Interactable it)
  {
    int foundAmount = 0;
    if (intIndex.ContainsKey(it.identifier))
    {
      foundAmount = intIndex[it.identifier];
      foundAmount++;
      intIndex[it.identifier]++;
      if (foundAmount == 2)
      {
        Interactable i = interactables[it.identifier];
        interactables.Remove(i.identifier);
        interactables.Add(i.identifier + "1", i);
        i.SetName(i.identifier + "1");
      }
      string newName = it.identifier + foundAmount.ToString();
      interactables.Add(newName, it);
      it.SetName(newName);
    }
    else
    {
      foundAmount = 1;
      intIndex.Add(it.identifier, foundAmount);
      interactables.Add(it.identifier, it);
      it.SetName(it.identifier);
    }
  }

  public void StartGame(int amountPlayers)
  {
    foreach (Interactable it in interactables.Values)
    {
      it.SetCommandWeight(amountPlayers);
    }
  }

  public void DistributeCommand(Cmd command, string objectID, TEAM team, string args = "")
  {
    if (!interactables.ContainsKey(objectID)) return;
    interactables[objectID].DoCommand(command, args);
  }
}
