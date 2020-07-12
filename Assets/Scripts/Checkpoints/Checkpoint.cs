using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Checkpoint : MonoBehaviour
{
    public bool PausePlayerOnReaching = false;
    bool Activated = false;

    public GameObject CheckpointCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger) return;
        if (!other.gameObject.CompareTag("Player")) return;

        if (CheckpointCam != null)
        {
            var cam = CheckpointCam.GetComponent<CinemachineVirtualCamera>();
            var activecam = Camera.main.GetComponent<CinemachineBrain>()?.ActiveVirtualCamera;

            if (cam != null && activecam != null)
            {
                if ((object)activecam != cam)
                {
                    activecam.Priority = 1;
                    cam.Priority = 100;
                }
            }
        }

        CheckpointController.activateCheckpoint(transform.position);
        Debug.Log($"Player reached {this.gameObject.name}");
    }

    private IEnumerator SetCamActiveForSeconds(CinemachineVirtualCamera cam, float time)
    {
        cam.Priority = 100;
        yield return new WaitForSeconds(time);
        cam.Priority = 1;
    }
}
