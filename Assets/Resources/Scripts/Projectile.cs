using System.Collections;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(WeaponCursor))]
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : Poolable
    {
        [SerializeField] private string nameOfProjectile;
        private WeaponCursor _cursor;
        private Collider2D _collider2D;
        private SpriteRenderer _renderer;
        private float _speed = 3f;
        private void Awake()
        {
            _cursor = GetComponent<WeaponCursor>();
            _collider2D = GetComponent<Collider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _cursor = GetComponent<WeaponCursor>();
        }
        
        public void Attack()
        {
            var playerPos = GameManager.MainPlayer.gameObject.transform.position;
            StartCoroutine(MoveTo(playerPos));
        }

        private IEnumerator MoveTo(Vector3 playerPos)
        {
            while (!transform.position.Equals(playerPos))
            {
                transform.position=Vector3.MoveTowards(transform.position, playerPos, _speed*Time.deltaTime);
                yield return null;
            }
            
        }
        
        public override void Activate(OnOff onOff)
        {
            _collider2D.enabled = onOff == OnOff.On;
            _renderer.enabled=onOff == OnOff.On;
        }
        public override string Name => nameOfProjectile;
        public void OnAttackFinished()
        {
            Activate(OnOff.Off);
            _cursor.IsShooting = false;
        }
    }
}