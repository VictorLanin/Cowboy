using System;
using System.Collections.Generic;
using Resources.Scripts;
using Tests;
using Unity.VisualScripting;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(WeaponCursor))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : Poolable, IOnAttackFinished
    {
        [SerializeField] private string nameOfProjectile;
        private WeaponCursor _cursor;
        private Collider2D _collider2D;
        private SpriteRenderer _renderer;
        private float _speed = 5f;
        private Vector3 _playerPosition=Vector3.positiveInfinity;
        private Vector3 _positionToComeBack;
        private void Awake()
        {
            _cursor = GetComponent<WeaponCursor>();
            _collider2D = GetComponent<Collider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _cursor = GetComponent<WeaponCursor>();
            _cursor.Attacker = this;
            _cursor.IsShooting = true;
            Attack();
        }
        

        public void Attack()
        {
            _playerPosition = GameManager.MainPlayer.gameObject.transform.position;
            _positionToComeBack = transform.position;
        }

        private void Update()
        {
            if (_playerPosition.Equals(Vector3.positiveInfinity)) return;
            transform.position=Vector3.MoveTowards(transform.position, _playerPosition, _speed);
        }
        

        public override void Activate(OnOff onOff)
        {
            _playerPosition = Vector3.positiveInfinity;
            _collider2D.enabled = onOff == OnOff.On;
            _renderer.enabled=onOff == OnOff.On;
        }

        public override string Name { get; }
        public void OnAttackFinished()
        {
            Activate(OnOff.Off);
            _cursor.IsShooting = false;
        }
    }
}