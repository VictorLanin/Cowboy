
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerCursor : MonoBehaviour
    {
        private Player _player;
        private float _horAxis;
        private float _verAxis;
        private LineRenderer _lineRenderer;
        private Rigidbody2D _rbody;
        private float _speed = 5f;
        private LinkedList<WeaponName> _namesOfWeapons = new(new [] { WeaponName.MachineGun,WeaponName.Grenade});
        private LinkedListNode<WeaponName> _weaponSelected;
        private static Dictionary<WeaponInGameObject.TypeOfWeapon, WeaponInGameObject> _availableWeapons=new ();
        private Collider2D _collider;
        public WeaponInGameObject SelectedWeaponGameObject { get; private set; }
        public WeaponName WeaponSelected => _weaponSelected?.Value ?? WeaponName.MachineGun;

        public void Awake()
        {
            _weaponSelected = _namesOfWeapons.First;
            _player = GetComponentInParent<Player>();
            _player.Awake();
            _lineRenderer = GetComponent<LineRenderer>();
            _rbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            var weaponInGameObjects  = GetComponentsInChildren<WeaponInGameObject>(true);
            for (int i = 0; i < weaponInGameObjects.Length; i++)
            {
                _availableWeapons.Add(weaponInGameObjects[i].WeaponType,weaponInGameObjects[i]);
            }
            SetWeapon(_weaponSelected.Value);
        }

        public void GetNext()
        {
            _weaponSelected = _weaponSelected.Next ?? _namesOfWeapons.First;
            SetWeapon(_weaponSelected.Value);
        }

        public void GetPrevious()
        {
            _weaponSelected = _weaponSelected.Previous ?? _namesOfWeapons.Last;
            SetWeapon(_weaponSelected.Value);
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
            _horAxis = Input.GetAxis("Shoot Hor");
            _verAxis = Input.GetAxis("Shoot Ver");
            _rbody.velocity = new Vector2(_horAxis,_verAxis)*_speed;
            var isFiring = Input.GetButton("Fire1");
            SelectedWeaponGameObject.EquippedWeapon.TryFiring(isFiring);
            //SelectedWeaponGameObject.TryFiring(fired);
            _lineRenderer.enabled = isFiring;
            if (!isFiring) return;
            UpdateLineRenderer();
            void UpdateLineRenderer()
            {
                _lineRenderer.SetPosition(0,_player.transform.position);
                _lineRenderer.SetPosition(1,transform.position);
            }
        }

        //todo надо будет поменять имена на стейт паттерн
        private void SetWeapon(WeaponName weaponName)
        {
            switch (weaponName)
            {
                case WeaponName.Grenade:
                    SelectedWeaponGameObject = _availableWeapons[WeaponInGameObject.TypeOfWeapon.ProjectileBased];
                    _collider.enabled = false;
                    break;
                case WeaponName.MachineGun:
                    SelectedWeaponGameObject = _availableWeapons[WeaponInGameObject.TypeOfWeapon.CursorBased];
                    _collider.enabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(weaponName), weaponName, null);
            }
            SelectedWeaponGameObject.SetWeapon(weaponName);
        }

        //todo удалить 
        public static WeaponInGameObject GetWeaponInGameObject(string nameOfCursor)
        {
            return nameOfCursor == "Cursor"
                ? _availableWeapons[WeaponInGameObject.TypeOfWeapon.CursorBased]
                : _availableWeapons[WeaponInGameObject.TypeOfWeapon.ProjectileBased];
        }
    }
}   