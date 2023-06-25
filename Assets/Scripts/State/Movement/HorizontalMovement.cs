using Assignment.Factory;
using UnityEngine;

namespace Assignment.State.Movement
{
    public class HorizontalMovement : IMovement
    {
        private float _moveSpeed;
        public float horizontalInput;

        public HorizontalMovement(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public void Execute(Rigidbody2D rb)
        {
            rb.velocity = new Vector2(horizontalInput * _moveSpeed, rb.velocity.y);
        }

    }
}
