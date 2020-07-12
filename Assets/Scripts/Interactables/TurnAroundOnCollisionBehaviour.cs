using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class TurnAroundOnCollisionBehaviour : Interactable
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var player = collision.gameObject.GetComponent<Character>();
                player.ForwardDirection = player.ForwardDirection == MovingDirection.Right ? MovingDirection.Left : MovingDirection.Right;
            }

        }
    }
}
