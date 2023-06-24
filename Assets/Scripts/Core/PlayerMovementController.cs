using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assignment.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {

        #region Fields
        [Header("Player Variables")]
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpForce = 5f;
        [SerializeField] GameObject gameover;
        [SerializeField] Transform startBoundary;
        [SerializeField] Transform endBoundary;

        private Rigidbody2D _rb;
        private Vector3 _moveDirection;
        private bool _isJumping;
    
        private bool _isRunning;
        private bool _gameCompleted;
        

        private Animator animator;

        private SpriteRenderer spriteRenderer;
      

        #endregion

        #region Monobehaviours
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (_gameCompleted )
                return;

            float horizontalInput = Input.GetAxis("Horizontal");
            
            MovementHandler(in horizontalInput);
            CheckBoundary();


        }

        private void OnCollisionEnter2D(Collision2D collision)
        {


            // Game over condition
            if (collision.gameObject.layer == 8)
            {
                animator.Play("idle");
                gameover.SetActive(true);
                _rb.velocity = new Vector2(0, 0);
                _gameCompleted = true;
                return;
            }
            
            else if (collision.gameObject.layer == 6)
            {
             
                _isJumping = false;
            }
            else if (collision.gameObject.layer == 9)
            {
                Debug.LogError("Icrease coin");
                Destroy(collision.gameObject);
            }
       
            
           
        }

       
        #endregion

        #region Methods
        void MovementHandler(in float horizontalInput)
        {



            if (horizontalInput != 0f)
            {
                _isRunning = true;
            }
            else
            {
                _isRunning = false;
            }
            if (Input.GetKey(KeyCode.A))
            {
                spriteRenderer.flipX = true; // Face left when pressing 'A'
            }
            else if (Input.GetKey(KeyCode.D))
            {
                spriteRenderer.flipX = false; // Face right when pressing 'D'
            }
            // Move the player horizontally
            _rb.velocity = new Vector2(horizontalInput * moveSpeed, _rb.velocity.y);

            // Jump
            if (IsGrounded() && Input.GetButtonDown("Jump") && !_isJumping)
            {
                _isJumping = true;
                _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                UpdateAnimator();
            }

           

        }


        bool IsGrounded()
        {
            float raycastDistance = 0.1f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance);
            return hit.collider != null;
        }
        void UpdateAnimator()
        {
            if (_isRunning)
                animator.Play("run");

            else
                animator.Play("idle");

        }
        private void CheckBoundary()
        {
            if (transform.position.x <= startBoundary.position.x)
            {
                transform.position = new Vector3(startBoundary.position.x, transform.position.y, transform.position.z);
            }
            else if (transform.position.x >= endBoundary.position.x)
            {
                transform.position = new Vector3(endBoundary.position.x, transform.position.y, transform.position.z);
            }
        }

       



        #endregion


    }
}
