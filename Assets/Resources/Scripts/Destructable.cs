using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using UnityEngine;

namespace LaninCode
{
    public class Destructable :MonoBehaviour
    {
        [SerializeField] private string _tagOfWeapon = "PlayerWeapon";
        private WeaponInGameObject _weaponInGameObject;
        protected SpriteRenderer _renderer;
        private Collider2D _collider;
        [SerializeField] private float _maxHealth=100;
        private float _curHealth;
        
        public float Health
        {
            get => _curHealth/_maxHealth;
        }
        private IOnDamage _destructee = null;
        private CollisionChecker _checker;
        private void Awake()
        {
            _checker=CollisionChecker.CreateInstance(new List<string>(){_tagOfWeapon});
            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            _curHealth = _maxHealth;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!_checker.CheckForAppropriateTag(col.tag)) return;
             _weaponInGameObject = GameManager.GetCursor(TypeOfCursor.Player,col.gameObject.GetInstanceID());
             StartCoroutine(CheckForDamage());
        }

        //todo потом удалить изменение цветов
        public IEnumerator CheckForDamage()
        {
            var weapon=_weaponInGameObject.CurrentWeapon;
            Debug.Log("dasdasda");
            while (Health>0)
            {
                if (_weaponInGameObject.CanDamage)
                {
                    weapon.ApplyDamage(this);
                    _renderer.color=Color.red;
                    yield return (weapon is IDelay delayable)? new WaitForSeconds(delayable.Delay) : null;
                }
                _renderer.color = Color.white;
                yield return null;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(!_checker.CheckForAppropriateTag(other.tag) ||  !_checker.IsObjectOutOfCollider(other)) return;
            StopCoroutine(CheckForDamage());
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

        public void GetDamage(int damage)
        {
            var posDamage = _curHealth - damage;
            _curHealth = posDamage <= 0 ? 0 : posDamage;
            _destructee.SetHealth(_curHealth);
        }
    }
    
}