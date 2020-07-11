using UnityEngine;

namespace Assets.Scripts.Player
{
    public class TurnAroundOnCollisionBehaviour : Interactable
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // get collision side
                var relativePosition = transform.InverseTransformPoint(collision.GetContact(0).point);

                Debug.LogError(relativePosition);

                if (relativePosition.x > 0 || relativePosition.x < 0)
                {
                    var player = collision.gameObject.GetComponent<Character>();
                    player.ForwardDirection = player.ForwardDirection == MovingDirection.Right ? MovingDirection.Left : MovingDirection.Right;
                }

            }

        }
    }
}
