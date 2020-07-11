using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 MoveDirection = new Vector3(0, 0.1f, 0);
    public bool Oscillating = true;
    public float Frequency = 3f;
    public float Offset = 0f;

    private void Update()
    {
        var sin = Oscillating ? (float)Math.Sin(Time.time * Frequency + Offset) : 1f;
        transform.position += new Vector3(MoveDirection.x * sin, MoveDirection.y * sin, MoveDirection.y * sin);
    }
}
