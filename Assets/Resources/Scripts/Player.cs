using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Destructable))]
    public class Player:MonoBehaviour,IOnDamage
    {
        private const float _speed = 5f;
        private Rigidbody2D _rbody;
        private float _horAxis;
        private Destructable _destructable;
        private bool _isJumping;
        private static PlayerCursor _cursor;
        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
            _destructable = GetComponent<Destructable>();
            _destructable.SetDestructee(this);
            _cursor = GetComponentInChildren<PlayerCursor>(true);
        }

        
        private void Update()
        {
            
            if (Input.GetButtonDown("Jump"))
            {
                _isJumping = true;
            }
            
            _horAxis = Input.GetAxis("MoveHorizontal");
            if (_horAxis == 0) return;
            if (_isJumping)
            {
                int translateIndex = (_horAxis > 0) ? 1:-1;
                _rbody.AddForce(Vector2.right*(translateIndex*_speed*25f),ForceMode2D.Impulse);
                _isJumping = false;
            }
            else
            {
                _rbody.velocity = Vector2.right * (_speed * _horAxis);
            }
        }


        public void SetHealth(float health)
        {
            if (_destructable.Health > 0) return;
            Debug.Log("Game over!");
        }
    }
}