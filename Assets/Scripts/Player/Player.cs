﻿using UnityEngine;

public enum MovingDirection
{
    Right = 0,
    Left
}

public class Player : MonoBehaviour
{
    public int Health = 5;
    public float InvulnerabilityDuration = 5.0f;
    public float ForwardSpeed = 1.0f;
    public MovingDirection ForwardDirection = MovingDirection.Right;
    public int FrameCntAfterToCheckForStuck = 5;
    public float MaxAllowedZAngleInDeg = 35.0f;

    private Vector3 lastPosition;
    private int positionCheckCnt;
    private float invulnerabilityCooldown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        // TODO: Refactor. Make it good :)
        var vec = (ForwardDirection == MovingDirection.Right ? 1.0f : -1.0f) * ForwardSpeed * Time.deltaTime;
        transform.position += Vector3.left * vec;

        ++positionCheckCnt;

        // check every x frames (user set) if we kept moving, if not jump
        if ((positionCheckCnt * Time.deltaTime) > FrameCntAfterToCheckForStuck)
        {
            if (transform.position.x - lastPosition.x < ForwardSpeed * 5f)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400f);
            }

            lastPosition = transform.position;
            positionCheckCnt = 0;
        }

        //update invulnerability value
        if (invulnerabilityCooldown <= 0) {
            invulnerabilityCooldown -= Time.deltaTime;
        }

    }
    public void DealDamage(int amount) {
        //check invulnerability
        if (invulnerabilityCooldown <= 0) {
            Health -= amount;
            invulnerabilityCooldown = InvulnerabilityDuration;
            //TODO CHECKPOINT IF DEAD
        }
    }
}
