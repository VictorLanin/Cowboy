
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
        private Collider2D _collider;

        public void Awake()
        {
            _player = GetComponentInParent<Player>();
            _player.Awake();
            _lineRenderer = GetComponent<LineRenderer>();
            _rbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }
        
        private void Update()
        {
            _horAxis = Input.GetAxis("Shoot Hor");
            _verAxis = Input.GetAxis("Shoot Ver");
            _rbody.velocity = new Vector2(_horAxis,_verAxis)*_speed;
            var isFiring = Input.GetButton("Fire1");
            _player.TryFiring(isFiring);
            _lineRenderer.enabled = isFiring;
            if (!isFiring) return;
            UpdateLineRenderer();
            void UpdateLineRenderer()
            {
                _lineRenderer.SetPosition(0,_player.transform.position);
                _lineRenderer.SetPosition(1,transform.position);
            }
        }
        
    }
}   