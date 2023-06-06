
using System;
using System.Collections.Generic;
using Codice.Client.Common;
using UnityEngine;
using Time = UnityEngine.Time;

namespace LaninCode
{
    [RequireComponent(typeof(LineRenderer))]
    public class PlayerCursor : MonoBehaviour
    {
        private float _horAxis;
        private float _verAxis;
        private float _speed = 5f; 
        private bool _isFiring;

        private void Update()
        {
            _isFiring = Input.GetButton("Fire1");
            if (_isFiring)
            {
                _horAxis = Input.GetAxis("Shoot Hor");
                _verAxis = Input.GetAxis("Shoot Ver");
                transform.Translate(new Vector2(_horAxis, _verAxis) * (_speed * Time.deltaTime));
            }
            var cam = Camera.main;
            var posOfPlayerOnScreen = cam.WorldToScreenPoint(transform.position);
            float x = GetX();
            float y = GetY();
            Vector3 posPlayerPosition = new Vector3(x, y, cam.nearClipPlane);
            var pos = cam.ScreenToWorldPoint(posPlayerPosition);
            transform.position = new Vector3(pos.x, pos.y, 0);
            
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