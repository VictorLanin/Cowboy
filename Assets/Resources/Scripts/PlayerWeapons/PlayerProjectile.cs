using System.Collections;
using UnityEngine;

namespace LaninCode
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerProjectile : Poolable
    {
        [SerializeField] private PlayerWeaponName nameOfProjectile;
        private Collider2D _collider2D;
        private SpriteRenderer _renderer;
        private Animator _anim;
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
        }

        /// <summary>
        /// Coroutine for projectile movement
        /// </summary>
        /// <param name="destination"> location go to </param>
        /// <param name="speed">speed of projectile</param>
        public void MoveToTarget(Vector3 destination, float speed)
        {
            StartCoroutine(MoveTo(destination,speed));
            
            IEnumerator MoveTo(Vector3 destination, float speed)
            {
                while (!transform.position.Equals(destination))
                {
                    transform.position = Vector3.MoveTowards(transform.position, destination,  speed* Time.deltaTime);
                    yield return null;
                }
                _anim.SetTrigger("BlowUp");
            }
        }
        /// <summary>
        /// "Turns" On-Off specific gameobject without disabling it off completely 
        /// </summary>
        /// <param name="onOff"></param>
        public override void Activate(OnOff onOff)
        {
            _collider2D.enabled = onOff == OnOff.On;
            _renderer.enabled=onOff == OnOff.On;
            _anim.StopPlayback();
        }
        public override string Name => nameOfProjectile.ToString();

    }
}