using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    public bool PausePlayerOnReaching = false;
    bool Activated = false;

    public GameObject CheckpointCam;

    private Animator indicator;

    private void Start()
    {
        indicator = GetComponentInChildren<Animator>();
    }

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

        if (indicator != null)
        {
            indicator.ResetTrigger("Deactivate");
            indicator.SetTrigger("Activate");
        }

        CheckpointController.activateCheckpoint(this.gameObject);
        Debug.Log($"Player reached {this.gameObject.name}");
    }

    private IEnumerator SetCamActiveForSeconds(CinemachineVirtualCamera cam, float time)
    {
        cam.Priority = 100;
        yield return new WaitForSeconds(time);
        cam.Priority = 1;
    }
}
