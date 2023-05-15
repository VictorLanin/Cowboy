using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(ProjectileManager))]
    public class Player : MonoBehaviour, IOnDamage
    {
        private float _speed = 10f;
        private Rigidbody2D _rbody;
        private float _horAxis;
        private Destructible _destructible;
        private bool _isJumping;
        private PlayerCursor _playerCursor;
        private IMech _equippedMech;
        public PlayerWeaponName PlayerWeaponSelected => _equippedMech?.SelectedPlayerWeapon ?? PlayerWeaponName.MachineGun;

        private ProjectileManager _projectileManager;

        private bool _canMovePlayer=true;
        public void Awake()
        {
            _equippedMech = new BasicMech();
            _playerCursor = GetComponentInChildren<PlayerCursor>(true);
            _projectileManager = GetComponent<ProjectileManager>();
            _projectileManager.GetPosition += () => CursorPosistion;
            AddToPlayers();
            _rbody = GetComponent<Rigidbody2D>();
            _destructible = GetComponent<Destructible>();
            _destructible.SetDestructee(this);
            SetWeapon(_equippedMech.SelectedPlayerWeapon);

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

            if (_canMovePlayer)
            {
                _horAxis = Input.GetAxis("Shoot Hor");
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
            else
            {
                _rbody.velocity = Vector2.zero;
            }
        }
        
        public void SetHealth(int health)
        {
            Debug.Log("Game over!");
        }

        public int MaxHealth => _equippedMech.MaxHealth;


        public void AddAmmo(PlayerWeaponName nameOfPlayerWeapon, int amountToAdd)
        {
            var weaponAmmo = GetWeaponBack(nameOfPlayerWeapon) as ILimitedAmmo;
            if (weaponAmmo == null) throw new InvalidCastException($"Cant cast {nameOfPlayerWeapon} to ILimitedAmmo");
            weaponAmmo.AvailableAmmo += amountToAdd;
        }

        public string Ammo
        {
            get
            {
                if (_equippedMech == null) return "-1";
                var selectedWeapon = _weapons[_equippedMech.SelectedPlayerWeapon];
                return selectedWeapon is ILimitedAmmo ammoedWeapon ? ammoedWeapon.AvailableAmmo.ToString() : "-1";
            }
        }
        
        public Vector3 CursorPosistion => _playerCursor.transform.position;

        private readonly Dictionary<PlayerWeaponName, PlayerWeapon> _weapons = new()
        {
            { PlayerWeaponName.MachineGun, PlayerWeapon.CreateInstance(PlayerWeaponName.MachineGun) },
            { PlayerWeaponName.Grenade, PlayerWeapon.CreateInstance(PlayerWeaponName.Grenade) }
        };


        public PlayerWeapon GetWeaponBack(PlayerWeaponName nameOfPlayerWeapon)
        {
            if (!_weapons.ContainsKey(nameOfPlayerWeapon))
                throw new KeyNotFoundException($"No weapon with this name {nameOfPlayerWeapon}");
            return _weapons[nameOfPlayerWeapon];
        }

        public void GetNext()
        {
            var weaponName = _equippedMech.GetNextWeapon();
            SetWeapon(weaponName);
        }

        private void SetWeapon(PlayerWeaponName playerWeaponSelected)
        {
            var selectedWeapon = _weapons[playerWeaponSelected];
            _playerCursor.gameObject.name = name + ' ' + playerWeaponSelected;
            _playerCursor.CursorCollider.enabled = selectedWeapon is not ILimitedAmmo;
        }

        public void GetPrevious()
        {
            var weaponName = _equippedMech.GetPreviousWeapon();
            SetWeapon(weaponName);
        }

        private static Dictionary<string, Player> _playersAvailable = new(4);

        public static Player GetPlayer(string playerID)
        {
            if (!_playersAvailable.ContainsKey(playerID)) throw new KeyNotFoundException($"no {playerID} in players");
            return _playersAvailable[playerID];
        } 

        public static PlayerWeapon GetDamagingWeapon(string colName)
        {
            var keys = colName.Split(' ');
            var playerName = keys[0];
            if (!_playersAvailable.ContainsKey(playerName)) return null;
            var player = _playersAvailable[playerName];
            return player.GetWeaponBack(Enum.Parse<PlayerWeaponName>(keys[1]));
        }

        public void TryFiring(bool isFiring)
        {
            _canMovePlayer = !isFiring;
            var weapon=_weapons[PlayerWeaponSelected];
            weapon.TryFiring(isFiring);
            if (weapon is not IProjectile projectileWeapon) return;
            _projectileManager.InstantiateProjectile(projectileWeapon);
        }

        public void AddHealth(int health)
        {
            _destructible.CurrentHealth =+ health;
        }
    }
} 