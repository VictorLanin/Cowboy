
using System;
using System.Collections.Generic;
using Codice.Client.Common;
using UnityEngine;
using Time = UnityEngine.Time;

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
                transform.Translate(new Vector2(_horAxis, _verAxis) * (_speed * Time.deltaTime));
            }
            var cam = Camera.main;
            var posOfPlayerOnScreen = cam.WorldToScreenPoint(transform.position);
            float x = GetX();
            float y=GetY();
            Vector3 posPlayerPosition = new Vector3(x, y, cam.nearClipPlane);
            var pos = cam.ScreenToWorldPoint(posPlayerPosition);
            transform.position = new Vector3(pos.x, pos.y, 0);
                
         
            UpdateLineRenderer();

            
            void UpdateLineRenderer()
            {
                _lineRenderer.SetPosition(0,_player.transform.position);
                _lineRenderer.SetPosition(1,transform.position);
            }
            
            float GetX()
            {
                if (posOfPlayerOnScreen.x < 0) return 0;
                if (posOfPlayerOnScreen.x > cam.pixelWidth) return cam.pixelWidth;
                return posOfPlayerOnScreen.x;
            }           
            float GetY()
            {
                if (posOfPlayerOnScreen.y < 0) return 0;
                if (posOfPlayerOnScreen.y > cam.pixelHeight) return cam.pixelHeight;
                return posOfPlayerOnScreen.y;
            }

        }
        
        
    }
}   