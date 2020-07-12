﻿using System.Collections;
using System.Dynamic;
using UnityEngine;

[SerializeField]
public enum MovingDirection
{
    Right = 0,
    Left
}

public class Character : MonoBehaviour
{
    private int _health = 5;
    private float pauseCooldown = 0;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0)
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsFalling", false);
                animator.SetBool("IsDead", true);
                animator.speed = 0.75f;
                _ = ResetAfter2Seconds();
            }
        }
    }

    public bool IsAlive => Health > 0;

    public bool IsPaused { get; private set; }

    public Vector2 PlayerDirection => ForwardDirection == MovingDirection.Right ? Vector3.right : Vector3.left;

    public float InvulnerabilityDuration = 5.0f;
    public float ForwardSpeed = 1.0f;

    [SerializeField]
    private MovingDirection _forwardDirection = MovingDirection.Right;

    [SerializeField]
    public MovingDirection ForwardDirection
    {
        get => _forwardDirection;
        set
        {
            _forwardDirection = value;
            GetComponent<SpriteRenderer>().flipX = _forwardDirection == MovingDirection.Left;
        }
    }
    public float MaxAllowedZAngleInDeg = 35.0f;

    private float invulnerabilityCooldown = 0.0f;

    private Animator animator;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    private void Start()
    {
        InteractableManager.instance.Register(this);
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.centerOfMass = new Vector2(0, -0.125f);
    }



    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("a"))
            Pause(5);

        if (IsAlive && !IsPaused)
        {
            rigidBody.AddForce(PlayerDirection * ForwardSpeed, ForceMode2D.Impulse);
            CheckAndFixHeadFirst();
            CheckAndSetAnimationState();
        }

        //update invulnerability value
        if (invulnerabilityCooldown >= 0)
        {
            invulnerabilityCooldown -= Time.deltaTime;
        }
        if (pauseCooldown > 0)
        {
            pauseCooldown -= Time.deltaTime;
        }

    }
    public void DealDamage(int amount)
    {
        //check invulnerability
        if (invulnerabilityCooldown <= 0)
        {
            Health -= amount;
            invulnerabilityCooldown = InvulnerabilityDuration;
            //TODO CHECKPOINT IF DEAD
        }
    }

    private IEnumerator ResetAfter2Seconds()
    {
        yield return new WaitForSeconds(2);

        Debug.Log("Reset to checkpoint called");
        transform.position = CheckpointController.latestActivatedCheckpoint.transform.position;
    }

    public void Pause(int secondsToWait)
    {
        if (IsPaused) return;
        if (pauseCooldown > 0) return;
        Debug.LogWarning("Pause called");
        IsPaused = true;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", true);
        StartCoroutine(PauseForSeconds(secondsToWait));
        pauseCooldown = secondsToWait + 5;
    }

    private IEnumerator PauseForSeconds(int secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        animator.SetBool("IsIdle", false);
        IsPaused = false;
    }

    private void CheckAndSetAnimationState()
    {
        var vel = rigidBody.GetPointVelocity(Vector2.zero);

        if (vel.y > 0.5f)
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsWalking", false);

            animator.SetBool("IsFalling", true);
        }
        else if ((vel.x > 0.015f || vel.x < -0.015f) && vel.y < 0.5f)
        {
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsFalling", false);

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsFalling", false);

            animator.SetBool("IsIdle", true);
        }
    }

    private float t;

    private void CheckAndFixHeadFirst()
    {
        t += t < 1 ? 3.0f * Time.deltaTime : 1;

        if (transform.rotation.eulerAngles.z > 35.0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, t);
        }

        // reset t if we are upright again
        if (Mathf.RoundToInt(transform.rotation.eulerAngles.z) == 0)
            t = 0;

    }
}
