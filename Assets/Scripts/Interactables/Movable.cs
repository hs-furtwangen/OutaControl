using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Interactables
{
    public class Movable : TurnAroundOnCollisionBehaviour
    {
        public Vector2 TargetPosition;
        public float Speed = 0.5f;

        private float t = 0;

        protected new void Start()
        {
            commands.Add(Cmd.up, new Command(Up));
            commands.Add(Cmd.down, new Command(Down));
            commands.Add(Cmd.right, new Command(Right));
            commands.Add(Cmd.left, new Command(Left));
            TargetPosition = transform.position;
            base.Start();
        }

        protected void Update()
        {
            t += t < 1 ? Speed * Time.deltaTime : 1;

            transform.position = Vector3.Lerp(transform.position, TargetPosition, t);
        }

        //TODO: probably make sure that the thing doesn't collide with other things
        private void Up(string message)
        {
            TargetPosition.y += commandWeight;
            //t = 0;
        }
        private void Down(string message)
        {
            TargetPosition.y -= commandWeight;
            //t = 0;
        }
        private void Right(string message)
        {
            TargetPosition.x += commandWeight;
            //t = 0;
        }
        private void Left(string message)
        {
            TargetPosition.x -= commandWeight;
            //t = 0;
        }
    }
}
