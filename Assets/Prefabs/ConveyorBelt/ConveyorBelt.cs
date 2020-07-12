using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConveyorBelt : Interactable
{
    public Vector2 Force = new Vector2(2, 0);

    [SerializeField]
    private MovingDirection _rotatingDirection = MovingDirection.Right;

    [SerializeField]
    public MovingDirection RotatingDirection
    {
        get => _rotatingDirection;
        set
        {
            _rotatingDirection = value;
            if(RotatingDirection == MovingDirection.Left)
            {
                gameObject.GetComponent<Animator>().speed = -1.0f;
            }
            else
            {
                gameObject.GetComponent<Animator>().speed = 1.0f;
            }
        }
    }

    private Vector2 _force => RotatingDirection == MovingDirection.Right ? new Vector2(Force.x, Force.y) : new Vector2(-Force.x, Force.y);

    private Rigidbody2D rigidBody;
    private bool entered;

    private void Update()
    {
        if(entered)
            rigidBody.AddForce(_force);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            entered = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            entered = false;
        }

    }

}
