using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Desctructable))]
    public class Player:MonoBehaviour,IOnDamage
    {
        private const float _speed = 5f;
        private Rigidbody2D _rbody;
        private float _horAxis;
        private Desctructable _desctructable;
        private bool _isJumping;
        
        private readonly List<string> _namesOfWeapons = new List<string>()
        {
            "MachineGun"
        };
        
        
        public string ChosenWeaponName { get; private set; }

        private void Awake()
        {
            ChosenWeaponName = _namesOfWeapons[0];
            _rbody = GetComponent<Rigidbody2D>();
            _desctructable = GetComponent<Desctructable>();
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


        public void DestroyObject()
        {
            if (_desctructable.Health > 0) return;
            Debug.Log("Game over!");
        }
    }
}