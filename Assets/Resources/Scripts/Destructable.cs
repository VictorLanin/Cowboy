using System;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class Destructable : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private Collider2D _collider;
        [SerializeField] private float _maxHealth=100;
        private float _curHealth;
        public float Health
        {
            get => _curHealth/_maxHealth;
        }
        protected IOnDamage _destructee = null;
        protected CollisionChecker _checker;
        protected virtual void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            _curHealth = _maxHealth;
        }

        public void Activate(OnOff onOff)
        {
            _curHealth =_maxHealth;
            _collider.enabled = onOff == OnOff.On;
            _renderer.enabled = onOff == OnOff.On;
        }

        public void SetDestructee(IOnDamage destr)
        {
            _destructee = destr;
        }

        protected void GetDamage(int damage)
        {
            if (_curHealth - damage < 0)
            {
                _curHealth = 0;
                return;
            }
            _curHealth -= damage;
        }
    }
}