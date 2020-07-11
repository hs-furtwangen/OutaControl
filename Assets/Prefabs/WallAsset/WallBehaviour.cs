using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : Interactable
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.ForwardDirection = player.ForwardDirection == MovingDirection.Right ? MovingDirection.Left : MovingDirection.Right; 
        }
        
    }
}
