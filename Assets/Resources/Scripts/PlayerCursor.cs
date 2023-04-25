
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(WeaponInGameObject))]
    public class PlayerCursor : MonoBehaviour
    {
        private Player _player;
        private float _horAxis;
        private float _verAxis;
        private LineRenderer _lineRenderer;
        private Rigidbody2D _rbody;

        private LinkedList<string> _namesOfWeapons = new(new string[] { "MachineGun","Pistol"});
        private LinkedListNode<string> _weaponSelected;

        public WeaponInGameObject WeapInGameObject { get; private set; }

        private void Awake()
        {
            _weaponSelected = _namesOfWeapons.First;
            _player = GetComponentInParent<Player>();
            _lineRenderer = GetComponent<LineRenderer>();
            _rbody = GetComponent<Rigidbody2D>();
            WeapInGameObject = GetComponent<WeaponInGameObject>();
            WeapInGameObject.SetWeapon(_weaponSelected.Value);
            GameManager.AvailableCursors[TypeOfCursor.Player].Add(WeapInGameObject);
        }

        public void GetNext()
        {
            _weaponSelected = _weaponSelected.Next ?? _namesOfWeapons.First;
            WeapInGameObject.SetWeapon(_weaponSelected.Value);
        }

        public void GetPrevious()
        {
            _weaponSelected = _weaponSelected.Previous ?? _namesOfWeapons.Last;
            WeapInGameObject.SetWeapon(_weaponSelected.Value);
        }
        private void Update()
        {
            _horAxis = Input.GetAxis("Shoot Hor");
            _verAxis = Input.GetAxis("Shoot Ver");
            _rbody.velocity = new Vector2(_horAxis,_verAxis);
            var clicked = Input.GetButton("Fire1");
            WeapInGameObject.SetCanDamage(clicked);
            _lineRenderer.enabled = WeapInGameObject.CanDamage;
            if (!WeapInGameObject.CanDamage) return;
            UpdateLineRenderer();
            void UpdateLineRenderer()
            {
                _lineRenderer.SetPosition(0,_player.transform.position);
                _lineRenderer.SetPosition(1,transform.position);
            }
        }
    }
}   