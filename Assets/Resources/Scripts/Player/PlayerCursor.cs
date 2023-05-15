
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

        public Collider2D CursorCollider => _collider;

        public void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _player = GetComponentInParent<Player>();
            _lineRenderer = GetComponent<LineRenderer>();
            _player.Awake();
        }
        
        private void Update()
        {
            var isFiring = Input.GetButton("Fire1");
            _lineRenderer.enabled = isFiring;
            _player.TryFiring(isFiring);
            if (isFiring)
            {
                _horAxis = Input.GetAxis("Shoot Hor");
                _verAxis = Input.GetAxis("Shoot Ver");
                _rbody.velocity = new Vector2(_horAxis, _verAxis) * _speed;
            }
            else
            {
                _rbody.velocity = Vector2.zero;
            }

            UpdateLineRenderer();

            void UpdateLineRenderer()
            {
                _lineRenderer.SetPosition(0,_player.transform.position);
                _lineRenderer.SetPosition(1,transform.position);
            }
        }
        
        
    }
}   