using System;
using UnityEngine;

namespace LaninCode
{
    public class HealthBarUpdate : MonoBehaviour
    {
        [SerializeField] private UISprite _sprite;
        private int _spriteWidth=-1;

        private void Awake()
        {
            _spriteWidth = _sprite.width;
        }

        public float WidthAndColor
        {
            set
            {
                if(_spriteWidth<0) return;
                _sprite.width =(int)(_spriteWidth*value); 
                _sprite.color=(value < 0.2f) ? Color.red:Color.green;
            }
        }
        
    }
}
