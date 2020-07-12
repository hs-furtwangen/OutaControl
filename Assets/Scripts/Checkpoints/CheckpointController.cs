using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckpointController
{
    public static GameObject latestActivatedCheckpoint;

    public static void activateCheckpoint(GameObject checkpoint)
    {
        if (latestActivatedCheckpoint != null && latestActivatedCheckpoint != checkpoint)
        {
            var indicator = latestActivatedCheckpoint.GetComponentInChildren<Animator>();
            if (indicator != null)
            {
                indicator.ResetTrigger("Activate");
                indicator.SetTrigger("Deactivate");
            }
        }

        latestActivatedCheckpoint = checkpoint;
    }
}
