using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform PlayerComponent;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(PlayerComponent.transform.position.x, PlayerComponent.transform.position.y, transform.position.z);
    }
}
