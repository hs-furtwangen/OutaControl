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
        public Vector2 TargetPosition;
        public float Speed = 3f;

        private float t = 0;

        protected new void Start()
        {
            commands.Add(Cmd.up, new Command(Up));
            commands.Add(Cmd.down, new Command(Down));
            commands.Add(Cmd.right, new Command(Right));
            commands.Add(Cmd.left, new Command(Left));
            TargetPosition = transform.position;

            identifier = "M";
            commands.Add("Up", new Command(Up));
            commands.Add("Down", new Command(Down));
            commands.Add("Right", new Command(Right));
            commands.Add("Left", new Command(Left));
            base.Start();
        }

        private void Update()
        {
            //if (transform.position.x <= TargetPosition.x && transform.position.y <= TargetPosition.y)
                t = t < 1 ? Speed * Time.deltaTime : 1;

            transform.position = Vector3.Lerp(transform.position, TargetPosition, t);

            if (Input.GetKeyDown("a"))
                Left("");
            if (Input.GetKeyDown("s"))
                Right("");

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
