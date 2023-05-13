using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LaninCode
{
    public class Destructible :MonoBehaviour
    {
        [SerializeField] private string _tagOfWeapon = "PlayerWeapon";
        private SpriteRenderer _renderer;
        private Collider2D _collider;
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;
        private IWeapon _weapon;
        
        public float HealthRelatToMaxHealth
        {
            get => currentHealth/maxHealth;
        }

        public CollisionChecker Checker => _checker;

        public int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value;
                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    return;
                }
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }

        public int MaxHealth => maxHealth;

        private IOnDamage _destructee = null;
        private CollisionChecker _checker;
        public void Awake()
        {
            CurrentHealth = maxHealth;
            _checker=CollisionChecker.CreateInstance(new List<string>(){_tagOfWeapon});
            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!_checker.CheckForAppropriateTag(col.tag)) return;
            _weapon = WeaponDataManager.GetWeapon(col.name);
             StartCoroutine(CheckForDamage());
        }

        //todo потом удалить изменение цветов
        public IEnumerator CheckForDamage()
        {
            while (currentHealth>0)
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
            currentHealth =maxHealth;
            _collider.enabled = onOff == OnOff.On;
            _renderer.enabled = onOff == OnOff.On;
        }

        public void SetDestructee(IOnDamage destr)
        {
            _destructee = destr;
            maxHealth = destr.MaxHealth;
        }

        public void GetDamage(int damage)
        {
            _destructee.SetHealth( currentHealth -= damage);
        }

        public void SetSprite(Sprite spr)
        {
            _renderer.sprite = spr;
        }
    }
    
}