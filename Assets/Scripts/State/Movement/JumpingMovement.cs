using Assignment.Factory;
using UnityEngine;

namespace Assignment.State.Movement
{
    public class JumpingMovement : IMovement
    {
        private float _jumpForce;

        public JumpingMovement(float jumpForce)
        {
            _jumpForce = jumpForce;
        }

        public void Execute(Rigidbody2D rb)
        {
            rb.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
        }
        
    }
}
