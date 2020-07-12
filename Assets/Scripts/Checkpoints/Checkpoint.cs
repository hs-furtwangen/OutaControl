using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
  public bool PausePlayerOnReaching = false;
  bool Activated = false;
  private void OnCollisionEnter2D(Collision2D other)
  {
    if(!other.gameObject.CompareTag("Player")) return;
    CheckpointController.activateCheckpoint(transform.position);
  }
}
