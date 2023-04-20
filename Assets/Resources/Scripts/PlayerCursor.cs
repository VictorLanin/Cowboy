﻿
using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(WeaponCursor))]
    public class PlayerCursor : MonoBehaviour
    {
        private Player _player;
        private float _horAxis;
        private float _verAxis;
        private LineRenderer _lineRenderer;
        private Rigidbody2D _rbody;
        public bool IsShooting => _cursor.IsShooting;
        private WeaponCursor _cursor;
        public string NameOfWeapon => _cursor.NameOfWeapon;
        private readonly List<string> _namesOfWeapons = new List<string>()
        {
            "MachineGun"
        };
        private void Awake()
        {
            _player = GetComponentInParent<Player>();
            _lineRenderer = GetComponent<LineRenderer>();
            _rbody = GetComponent<Rigidbody2D>();
            _cursor = GetComponent<WeaponCursor>();
            _cursor.NameOfWeapon = _namesOfWeapons[0];
        }

        private void Update()
        {
            _horAxis = Input.GetAxis("Shoot Hor");
            _verAxis = Input.GetAxis("Shoot Ver");
            _rbody.velocity = new Vector2(_horAxis,_verAxis);
            _cursor.IsShooting = Input.GetButton("Fire1");
            _lineRenderer.enabled = _cursor.IsShooting;
            if (!_cursor.IsShooting) return;
            UpdateLineRenderer();
        }

        private void UpdateLineRenderer()
        {
            _lineRenderer.SetPosition(0,_player.transform.position);
            _lineRenderer.SetPosition(1,transform.position);
        }
        
    }
}   