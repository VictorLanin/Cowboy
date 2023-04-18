using System;
using System.Collections.Generic;
using Tests;
using UnityEngine;

namespace  LaninCode
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class Desctructable : MonoBehaviour
    {
        [SerializeField] private int _health=100;
        private CollisionChecker _checker=CollisionChecker.CreateInstance(new List<string>(){"Weapon"});
        private SpriteRenderer _renderer;
        private Collider2D _collider;
        private WeaponCursor _weaponCursor;
        private WeaponBack _chosenWeapon;
        private IOnDamage _destructee = null;
        
        public int Health => _health;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }
        

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!_checker.CheckForAppropriateTag(col.tag)) return;
            if(_weaponCursor==null) _weaponCursor = GameManager.GetCursor(col.gameObject.GetInstanceID());
            _chosenWeapon = GameManager.GetWeapon(_weaponCursor.NameOfWeapon);
            if (!_chosenWeapon.IsInstant) return;
            _health -= _chosenWeapon.Damage;
            _weaponCursor.OnAttackFinished();
            _weaponCursor = null;
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if(!_checker.CheckForAppropriateTag(other.tag) ||  !_checker.IsObjectOutOfCollider(other)) return;
            _weaponCursor = null;

        }

        private void Update()
        {
            if (_weaponCursor is null || !_weaponCursor.IsShooting) return;
            _health -= _chosenWeapon.Damage;
            Debug.Log(_health);
            if (_destructee is null)
            {
                throw new NullReferenceException("destructee was not set");
            }
            _destructee.DestroyObject();
        }

        public void Activate(OnOff onOff)
        {
            _health =100;
            _collider.enabled = onOff == OnOff.On;
            _renderer.enabled = onOff == OnOff.On;
        }

        public void SetDestructee(IOnDamage destr)
        {
            _destructee = destr;
        }
    }
}