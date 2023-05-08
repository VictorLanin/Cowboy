using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(ProjectileManager))]
    public class Player : MonoBehaviour, IOnDamage
    {
        private const float _speed = 10f;
        private Rigidbody2D _rbody;
        private float _horAxis;
        private Destructible _destructible;
        private bool _isJumping;
        private PlayerCursor _playerCursor;

        private LinkedList<WeaponName> _namesOfWeapons = new(new[] { WeaponName.MachineGun, WeaponName.Grenade });
        private LinkedListNode<WeaponName> _weaponSelected;
        public WeaponName WeaponSelected => _weaponSelected!= null ? Weapons[_weaponSelected.Value].Name : Weapons[WeaponName.MachineGun].Name;

        private ProjectileManager _projectileManager;

        private bool _canMovePlayer=true;
        public void Awake()
        {
            _playerCursor = GetComponentInChildren<PlayerCursor>(true);
            _projectileManager = GetComponent<ProjectileManager>();
            _projectileManager.GetPosition += () => CursorPosistion;
            AddToPlayers();
            _rbody = GetComponent<Rigidbody2D>();
            _destructible = GetComponent<Destructible>();
            _destructible.SetDestructee(this);
            _weaponSelected = _namesOfWeapons.First;
            SetWeapon(_weaponSelected.Value);

            void AddToPlayers()
            {
                if (_playersAvailable.ContainsKey(name)) return;
                _playersAvailable.Add(name, this);
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Debug Previous"))
            {
                Debug.Log("previous");
                GetPrevious();
            }

            if (Input.GetButtonDown("Debug Next"))
            {
                Debug.Log("next");
                GetNext();
            }
            
            if (Input.GetButtonDown("Jump"))
            {
                _isJumping = true;
            }

            if(!_canMovePlayer) return;
            _horAxis = Input.GetAxis("MoveHorizontal");
            if (_horAxis == 0) return;
            if (_isJumping)
            {
                int translateIndex = (_horAxis > 0) ? 1 : -1;
                _rbody.AddForce(Vector2.right * (translateIndex * _speed * 25f), ForceMode2D.Impulse);
                _isJumping = false;
            }
            else
            {
                _rbody.velocity = Vector2.right * (_speed * _horAxis);
            }
        }


        public void SetHealth(int health)
        {
            Debug.Log("Game over!");
        }


        public void AddAmmo(WeaponName nameOfWeapon, int amountToAdd)
        {
            var weaponAmmo = GetWeaponBack(nameOfWeapon) as ILimitedAmmo;
            if (weaponAmmo == null) throw new InvalidCastException($"Cant cast {nameOfWeapon} to ILimitedAmmo");
            weaponAmmo.AvailableAmmo += amountToAdd;
        }

        public string Ammo =>
            _selectedWeapon is ILimitedAmmo ammoedWeapon ? ammoedWeapon.AvailableAmmo.ToString() : "-1";

        public Vector3 CursorPosistion => _playerCursor.transform.position;

        private static readonly Dictionary<WeaponName, Weapon> Weapons = new()
        {
            { WeaponName.MachineGun, Weapon.CreateInstance(WeaponName.MachineGun) },
            { WeaponName.Grenade, Weapon.CreateInstance(WeaponName.Grenade) }
        };

        private Weapon _selectedWeapon;

        public Weapon GetWeaponBack(WeaponName nameOfWeapon)
        {
            if (!Weapons.ContainsKey(nameOfWeapon))
                throw new KeyNotFoundException($"No weapon with this name {nameOfWeapon}");
            return Weapons[nameOfWeapon];
        }

        public void GetNext()
        {
            _weaponSelected = _weaponSelected.Next ?? _namesOfWeapons.First;
            SetWeapon(_weaponSelected.Value);
        }

        private void SetWeapon(WeaponName weaponSelected)
        {
            _selectedWeapon = Weapons[weaponSelected];
            _playerCursor.gameObject.name = name + ' ' + weaponSelected;
            //_playerCursor.CursorCollider.enabled = _selectedWeapon is not ILimitedAmmo;
        }

        public void GetPrevious()
        {
            _weaponSelected = _weaponSelected.Previous ?? _namesOfWeapons.Last;
            SetWeapon(_weaponSelected.Value);
        }

        private static Dictionary<string, Player> _playersAvailable = new(4);

        public static Player GetPlayer(string playerID)
        {
            if (!_playersAvailable.ContainsKey(playerID)) throw new KeyNotFoundException($"no {playerID} in players");
            return _playersAvailable[playerID];
        } 

        public static Weapon GetDamagingWeapon(string colName)
        {
            var keys = colName.Split(' ');
            var playerName = keys[0];
            var player = _playersAvailable[playerName];
            return player.GetWeaponBack(Enum.Parse<WeaponName>(keys[1]));
        }

        public void TryFiring(bool isFiring)
        {
            _canMovePlayer = !isFiring;
            var weapon=Weapons[WeaponSelected];
            weapon.TryFiring(isFiring);
            if (weapon is not IProjectile projectileWeapon) return;
            _projectileManager.InstantiateProjectile(projectileWeapon);
        }

        public void AddHealth(int health)
        {
            _destructible.CurrentHealth = _destructible.CurrentHealth + health;
        }
    }
} 