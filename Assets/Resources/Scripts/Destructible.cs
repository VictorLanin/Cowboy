using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    public class Destructible :MonoBehaviour
    {
        [SerializeField] private string _tagOfWeapon = "PlayerWeapon";
        private SpriteRenderer _renderer;
        private Collider2D _collider;
        [SerializeField] private float _maxHealth=100;
        private float _currentHealth;
        private Weapon _weapon;
        
        public float HealthRelatToMaxHealth
        {
            get => _currentHealth/_maxHealth;
        }

        public CollisionChecker Checker => _checker;

        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                if (value <= 0)
                {
                    _currentHealth = 0;
                    return;
                }

                if (value >= _maxHealth)
                {
                    _currentHealth = _maxHealth;
                    return;
                }
                _currentHealth = value;
            }
        }

        private IOnDamage _destructee = null;
        private CollisionChecker _checker;
        public void Awake()
        {
            _checker=CollisionChecker.CreateInstance(new List<string>(){_tagOfWeapon});
            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            _currentHealth = _maxHealth;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!_checker.CheckForAppropriateTag(col.tag)) return;
            _weapon = Player.GetDamagingWeapon(col.name);
             StartCoroutine(CheckForDamage());
        }

        //todo потом удалить изменение цветов
        public IEnumerator CheckForDamage()
        {
            while (HealthRelatToMaxHealth>0)
            {
                if (_weapon.CanDamage)
                {
                    _weapon.ApplyDamage(this);
                    _renderer.color=Color.red;
                    yield return (_weapon is IDamageDelay delayable)? new WaitForSeconds(delayable.DamageDelay) : null;
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
            _currentHealth =_maxHealth;
            _collider.enabled = onOff == OnOff.On;
            _renderer.enabled = onOff == OnOff.On;
        }

        public void SetDestructee(IOnDamage destr)
        {
            _destructee = destr;
        }

        public void GetDamage(float damage)
        {
            _destructee.SetHealth( CurrentHealth -= damage);
        }

        public void SetSprite(Sprite spr)
        {
            _renderer.sprite = spr;
        }
    }
    
}