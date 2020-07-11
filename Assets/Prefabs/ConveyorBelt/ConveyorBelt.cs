using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConveyorBelt : Interactable
{
    public float Speed = 2;

    private bool enteredOnce;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if (enteredOnce) return;

            //enteredOnce = true;

            var player = collision.gameObject.GetComponent<Player>();
            player.ForwardSpeed *= player.ForwardDirection == MovingDirection.Left ? Speed : 1f / Speed; 
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           // enteredOnce = false;
            var player = collision.gameObject.GetComponent<Player>();
            player.ForwardSpeed /= player.ForwardDirection == MovingDirection.Left ? Speed : 1f / Speed;
        }

    }

}
