using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LaninCode
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Destructible))]
    public class Player : MonoBehaviour, IOnDamage
    {
        private float _speed = 10f;
        private Rigidbody2D _rbody;
        private float _horAxis;
        private Destructible _destructible;
        private bool _isJumping;
        private NonProjectileWeapon _playerCursor;
        private IMech _equippedMech;
        public PlayerWeaponName PlayerWeaponSelected => _equippedMech?.SelectedPlayerWeapon ?? PlayerWeaponName.MachineGun;

        private bool _cantMovePlayer=true;
        private SortedList<string, IWeapon> _weapons = new();

        private IWeapon _selectedWeapon;
        public void Awake()
        {
            _equippedMech = new BasicMech();
            _playerCursor = GetComponentInChildren<NonProjectileWeapon>(true);
            _weapons.Add(_playerCursor.NameofWeapon,_playerCursor);
            AddToPlayers();
            AddWeapons();
            _destructible = GetComponent<Destructible>();
            _destructible.SetDestructee(this);
            _selectedWeapon=SetWeapon(PlayerWeaponSelected);
            
            void AddToPlayers()
            {
                if (_playersAvailable.ContainsKey(name)) return;
                _playersAvailable.Add(name, this);
            }

            void AddWeapons()
            {
                var weapons = GetComponentsInChildren<ProjectileInstancer>();
                for (int i = 0; i < weapons.Length; i++)
                {
                    _weapons.Add(weapons[i].NameofWeapon,weapons[i]);
                }
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
            _cantMovePlayer=Input.GetButton("Fire1");
            if (_cantMovePlayer)
            {
                _selectedWeapon.TryFiring();
            }
            else
            {
                _horAxis = Input.GetAxis("Shoot Hor");
                if (_isJumping)
                {
                    int translateIndex = (_horAxis > 0) ? 1 : -1;
                    _rbody.AddForce(Vector2.right * (translateIndex * _speed * 25f), ForceMode2D.Impulse);
                    _isJumping = false;
                    _rbody.velocity = Vector2.zero;
                }
                else
                {
                    transform.Translate(Vector2.right * (_speed * _horAxis * Time.deltaTime));
                }

                var cam = Camera.main;
                var posOfPlayerOnScreen = cam.WorldToScreenPoint(transform.position);
                float x = GetX();
                Vector3 posPlayerPosition = new Vector3(x, posOfPlayerOnScreen.y, cam.nearClipPlane);
                var pos = cam.ScreenToWorldPoint(posPlayerPosition);
                transform.position = new Vector3(pos.x, pos.y, 0);

                float GetX()
                {
                    if (posOfPlayerOnScreen.x < 0) return 0;
                    if (posOfPlayerOnScreen.x > cam.pixelWidth) return cam.pixelWidth;
                    return posOfPlayerOnScreen.x;
                }
            }
        }
        
        public void SetHealth(int health)
        {
            _destructible.CurrentHealth -= health;
            //Debug.Log("Game over!");
        }

        public int MaxHealth => _equippedMech.MaxHealth;
        
        public void AddAmmo(PlayerWeaponName nameOfPlayerWeapon, int amountToAdd)
        {
            if (!_ammos.ContainsKey(nameOfPlayerWeapon))
                throw new KeyNotFoundException($"{nameOfPlayerWeapon} not found in weaponsAmmo");
            _ammos[nameOfPlayerWeapon].AvailableAmmo += amountToAdd;
        }

        public string AmmoText
        {
            get
            {
                if (_equippedMech == null || !_ammos.ContainsKey(PlayerWeaponSelected)) return "-1";
                return _ammos[PlayerWeaponSelected].ToString();
            }
        }
        
        public Vector3 CursorPosition => _playerCursor.transform.position;
        
        public IWeapon GetWeaponData(PlayerWeaponName nameOfPlayerWeapon)
        {
            if (!_weapons.ContainsKey(nameOfPlayerWeapon.ToString()))
                throw new KeyNotFoundException($"No weapon with this name {nameOfPlayerWeapon}");
            return _weapons[nameOfPlayerWeapon.ToString()];
        }

        public void GetNext()
        {
            var weaponName = _equippedMech.GetNextWeapon();
           _selectedWeapon=SetWeapon(weaponName);
        }

        private IWeapon SetWeapon(PlayerWeaponName playerWeaponSelected)
        {
            var selectedWeapon = WeaponManager.GetPlayerWeapon(playerWeaponSelected);
            var isProjectile=selectedWeapon is IProjectileData;
            _playerCursor.gameObject.name = name + ' ' + playerWeaponSelected;
            _playerCursor.CursorCollider.enabled = !isProjectile;
            if (isProjectile) return _weapons[playerWeaponSelected.ToString()];
            _playerCursor.WeaponData = selectedWeapon;
            return _playerCursor;
        }

        public void GetPrevious()
        {
            var weaponName = _equippedMech.GetPreviousWeapon();
           _selectedWeapon=SetWeapon(weaponName);
        }

        private static Dictionary<string, Player> _playersAvailable = new(4);

        public static Player GetPlayer(string playerID)
        {
            if (!_playersAvailable.ContainsKey(playerID)) throw new KeyNotFoundException($"no {playerID} in players");
            return _playersAvailable[playerID];
        }

        public static Vector3 GetPlayerPosition()
        {
            if (_playersAvailable.Count == 0) return _playersAvailable.First().Value.transform.position;
            throw new NullReferenceException();
        }
        public static IWeapon GetDamagingWeapon(string colName)
        {
            var keys = colName.Split(' ');
            var playerName = keys[0];
            if (!_playersAvailable.ContainsKey(playerName)) return null;
            var player = _playersAvailable[playerName];
            return player.GetWeaponData(Enum.Parse<PlayerWeaponName>(keys[1]));
        }
        
        public void AddHealth(int health)
        {
            _destructible.CurrentHealth =+ health;
        }
        //private static bool _isBusyGetingPowerUp = false;

        // private static PowerUp GetHealthPowerUp(Player player)
        // {
        //     int maxRatio = 0;
        //     string nameOfItem=string.Empty;
        //     if (player._destructible.HealthRelatToMaxHealth > 0.6f) return null;
        //     if (player._destructible.HealthRelatToMaxHealth < 0.3f)
        //     {
        //         maxRatio = 50;
        //         nameOfItem = "SmallHealth";
        //     }
        //     bool wasSuccess = Random.Range(0, maxRatio)<maxRatio;
        //     return wasSuccess ? ObjectPoolsManager.Get(nameOfItem) as PowerUp : null;
        // }
        //
        // public static PowerUp GetPowerUp()
        // {
        //     PowerUp powerUp;
        //     if (_isBusyGetingPowerUp) return null;
        //     foreach (var player in _playersAvailable.Values)
        //     {
        //         powerUp = GetHealthPowerUp(player);
        //         if (powerUp == null) continue;
        //         return powerUp;
        //     }
        //     return null;
        // }
        
        private SortedList<PlayerWeaponName, IAmmoWeapon> _ammos = new();
        public void AddAmmoCounter(PlayerWeaponName playerWeaponName,IAmmoWeapon ammoWeapon)
        {
            if (_ammos.ContainsKey(playerWeaponName)) return;
            _ammos.Add(playerWeaponName,ammoWeapon);
        }
    }
} 