using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConveyorBelt : Interactable
{
    public float Speed = 2;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Character>();
            player.ForwardSpeed *= player.ForwardDirection == MovingDirection.Left ? Speed : 1f / Speed;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Character>();
            player.ForwardSpeed /= player.ForwardDirection == MovingDirection.Left ? Speed : 1f / Speed;
        }

    }

}
