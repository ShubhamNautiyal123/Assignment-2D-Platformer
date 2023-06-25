using UnityEngine;
using Assignment.State.Movement;
using Assignment.Manager;
using Assignment.Factory;

namespace Assignment.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AnimationController))]
    public class PlayerMovementController : MonoBehaviour
    {

        #region Fields
        [Header("Player Variables")]
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpForce = 5f;
        
        [SerializeField] Transform startBoundary;
        [SerializeField] Transform endBoundary;
     
       
        private Rigidbody2D _rb;
        private Vector3 _moveDirection;
        private bool _isJumping;
    
        private bool _isRunning;
        private bool _gameCompleted;
        

        

        private SpriteRenderer spriteRenderer;


        //References for Movement State
        IMovement iMovement;
        JumpingMovement jumpingMovement;
        HorizontalMovement horizontalMovement;

        #endregion

        #region Monobehaviours
        private void Awake()
        {
            //Movement state
            jumpingMovement = new JumpingMovement(jumpForce);
            horizontalMovement = new HorizontalMovement(moveSpeed);
            iMovement = horizontalMovement;

            _rb = GetComponent<Rigidbody2D>();
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
                AnimationController.currentAnimState = AnimationController.AnimationState.Idle;
                GameManager.Instance.GameLose?.Invoke();
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

                GameManager.Instance.UpdateCoins(1);
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
            PlayerDirection();

            // Movement
            horizontalMovement.horizontalInput = horizontalInput;
            iMovement = horizontalMovement;

            iMovement.Execute(_rb);
          

            // Jump
            if (IsGrounded() && Input.GetButtonDown("Jump") && !_isJumping)
            {
                iMovement = jumpingMovement;
                _isJumping = true;
                iMovement.Execute(_rb);
              
            }
            else
            {
                UpdateAnimator();
            }

           

        }

        void PlayerDirection()
        {
            if (Input.GetKey(KeyCode.A))
            {
                spriteRenderer.flipX = true; // Face left when pressing 'A'
            }
            else if (Input.GetKey(KeyCode.D))
            {
                spriteRenderer.flipX = false; // Face right when pressing 'D'
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
                AnimationController.currentAnimState=AnimationController.AnimationState.Run;

            else
                AnimationController.currentAnimState = AnimationController.AnimationState.Idle;


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
