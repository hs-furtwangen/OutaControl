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

        private new void Start()
        {
            identifier = 'P';
            commands.Add("up", new Command(Up));
            commands.Add("down", new Command(Down));
            commands.Add("right", new Command(Right));
            commands.Add("left", new Command(Left));
            TargetPosition = transform.position;
        }

        private void Update()
        {
            if (transform.position.x <= TargetPosition.x && transform.position.y <= TargetPosition.y)
                transform.position += new Vector3(MoveDirection.x * Speed * Time.deltaTime, MoveDirection.y * Speed * Time.deltaTime, 0);
        }

        //TODO: probably make sure that the thing doesn't collide with other things
        private void Up(string message)
        {
            TargetPosition.y += commandWeight;
        }
        private void Down(string message)
        {
            TargetPosition.y -= commandWeight;
        }
        private void Right(string message)
        {
            TargetPosition.x += commandWeight;
        }
        private void Left(string message)
        {
            TargetPosition.x -= commandWeight;
        }
    }
}
