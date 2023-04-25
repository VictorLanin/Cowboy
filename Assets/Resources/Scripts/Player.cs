using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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
        private PlayerCursor _playerCursor;
        
         
        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
            _destructable = GetComponent<Destructable>();
            _destructable.SetDestructee(this);
            _playerCursor = GetComponent<PlayerCursor>();
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

        private Dictionary<string, int> _currentAmmoSupply = new Dictionary<string, int>()
        { 
            { "Grenade", 2 }
        };

        public void AddAmmo(string nameOfWeapon,int amountToAdd)
        {
            var weaponAmmo = WeaponInGameObject.GetWeaponBack(nameOfWeapon) as ILimitedAmmo;
            if (weaponAmmo == null) throw new InvalidCastException($"Cant cast {nameOfWeapon} to ILimitedAmmo");
            var currentAmount = _currentAmmoSupply[nameOfWeapon];
            var posAmmoAmount = currentAmount + amountToAdd;
            _currentAmmoSupply[nameOfWeapon] = posAmmoAmount < weaponAmmo.MaxAmmo ? posAmmoAmount : weaponAmmo.MaxAmmo;
        }

        public string Ammo
        {
            get
            {
                var weaponName = _playerCursor.WeapInGameObject.CurrentWeapon.Name;
                var ammoAmount= _playerCursor.WeapInGameObject.CurrentWeapon is ILimitedAmmo
                    ? _currentAmmoSupply[weaponName]
                    : -1;
                return ammoAmount.ToString();
            }

        }

    }
}