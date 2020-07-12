using System;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Api.Enums;
using UnityEngine;

public static class CheckpointController
{
    public static Tuple<GameObject, MovingDirection> latestActivatedCheckpoint;

    public static void activateCheckpoint(GameObject checkpoint, MovingDirection direction)
    {
        if (latestActivatedCheckpoint != null && latestActivatedCheckpoint.Item1 != checkpoint)
        {
            var indicator = latestActivatedCheckpoint.Item1.GetComponentInChildren<Animator>();
            if (indicator != null)
            {
                indicator.ResetTrigger("Activate");
                indicator.SetTrigger("Deactivate");
            }
        }

        latestActivatedCheckpoint = Tuple.Create(checkpoint, direction);
    }
}
