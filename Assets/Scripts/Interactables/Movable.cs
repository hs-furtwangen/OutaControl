using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactables
{
    public class Movable : TurnAroundOnCollisionBehaviour
    {
        public Vector2 MoveDirection = new Vector2(1, 1);
        public Vector2 TargetPosition;
        public float Speed = 3f;

        private float t = 0;

        private new void Start()
        {
            identifier = "M";
            commands.Add("up", new Command(Up));
            commands.Add("down", new Command(Down));
            commands.Add("right", new Command(Right));
            commands.Add("left", new Command(Left));
            TargetPosition = transform.position;
        }

        private void Update()
        {
            if (transform.position.x <= TargetPosition.x && transform.position.y <= TargetPosition.y)
                t = t < 1 ? Speed * Time.deltaTime : 1;

            transform.position = Vector3.Lerp(transform.position, TargetPosition, t);
        }

        //TODO: probably make sure that the thing doesn't collide with other things
        private void Up(string message)
        {
            TargetPosition.y += commandWeight;
            t = 0;
        }
        private void Down(string message)
        {
            TargetPosition.y -= commandWeight;
            t = 0;
        }
        private void Right(string message)
        {
            TargetPosition.x += commandWeight;
            t = 0;
        }
        private void Left(string message)
        {
            TargetPosition.x -= commandWeight;
            t = 0;
        }
    }
}
