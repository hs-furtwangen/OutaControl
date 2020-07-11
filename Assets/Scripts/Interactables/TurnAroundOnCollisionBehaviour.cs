using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class TurnAroundOnCollisionBehaviour : Interactable
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                for(var i = 0; i < collision.contacts.Length; i++)
                {
                    var currentContactPt = collision.contacts[i];
                    if(currentContactPt.normal.x != 0)
                    {
                        var player = collision.gameObject.GetComponent<Character>();
                        player.ForwardDirection = player.ForwardDirection == MovingDirection.Right ? MovingDirection.Left : MovingDirection.Right;
                        break;
                    }
                }
            }

        }
    }
}
