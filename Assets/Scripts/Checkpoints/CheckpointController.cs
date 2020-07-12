using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckpointController
{
  public static Vector3 latestActivatedCheckpoint;

  public static void activateCheckpoint(Vector3 position){
    latestActivatedCheckpoint = position;
  }
}
