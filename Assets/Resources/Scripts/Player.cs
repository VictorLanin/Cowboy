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
        private const float _speed = 10f;
        private Rigidbody2D _rbody;
        private float _horAxis;
        private Destructable _destructable;
        private bool _isJumping;
        private PlayerCursor _playerCursor;
        
         
        public void Awake()
        {
            _playerCursor = GetComponentInChildren<PlayerCursor>(true);
            _rbody = GetComponent<Rigidbody2D>();
            _destructable = GetComponent<Destructable>();
            _destructable.SetDestructee(this);
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

        private Dictionary<WeaponName, int> _currentAmmoSupply = new ()
        { 
            { WeaponName.Grenade, 4 }
        };

        public void AddAmmo(WeaponName nameOfWeapon,int amountToAdd)
        {
            var weaponAmmo = WeaponInGameObject.GetWeaponBack(nameOfWeapon) as ILimitedAmmo;
            if (weaponAmmo == null) throw new InvalidCastException($"Cant cast {nameOfWeapon} to ILimitedAmmo");
            var currentAmount = _currentAmmoSupply[nameOfWeapon];
            var posAmmoAmount = currentAmount + amountToAdd;
            _currentAmmoSupply[nameOfWeapon] = posAmmoAmount < weaponAmmo.MaxAmmo ? posAmmoAmount : weaponAmmo.MaxAmmo;
        }

        public void ReduceAmmo(WeaponBack weapon)
        {
            var currentAmount = _currentAmmoSupply[weapon.Name];
            var posAmmoAmount = currentAmount - ((ILimitedAmmo)weapon).ReduceAmmoRate;
            _currentAmmoSupply[weapon.Name] = posAmmoAmount > 0 ? posAmmoAmount :0;
        }

        public string Ammo
        {
            get
            {
                var ammoAmount = GetAmountOfAmmo();
                return ammoAmount.ToString();
            }
        }
        
        public int GetAmountOfAmmo()
        {
            // if (_playerCursor == null) return -1;
            // var weaponName = _playerCursor.SelectedWeaponGameObject.EquippedWeapon.Name;
            // var ammoAmount = _playerCursor.SelectedWeaponGameObject.EquippedWeapon is ILimitedAmmo
            //     ? _currentAmmoSupply[weaponName]
            //     : -1;
            // return ammoAmount;
            return -1;
        }

        public Vector3 CursorPosistion => _playerCursor.transform.position;
    }
}