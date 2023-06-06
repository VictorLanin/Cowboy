using System;
using System.Collections;
using UnityEngine;

namespace LaninCode
{
    public class ProjectileGameObject : MonoBehaviour
    {
        private Collider2D _collider2D;
        private SpriteRenderer _renderer;
        private Animator _anim;
        private static readonly int BlowUp = Animator.StringToHash("BlowUp");
        
        public void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
        }

        /// <summary>
        /// Coroutine for projectile movement
        /// </summary>
        /// <param name="destination"> location go to </param>
        /// <param name="speed"></param>
        public void MoveToTarget(Vector3 destination, float speed)
        {
            StartCoroutine(MoveTo(destination,speed));
            
            IEnumerator MoveTo(Vector3 destinationOfProj, float speedOfProj)
            {
                Activate(OnOff.On);
                while (!transform.position.Equals(destinationOfProj))
                {
                    transform.position = Vector3.MoveTowards(transform.position, destinationOfProj,  speedOfProj* Time.deltaTime);
                    yield return null;
                }
                _anim.SetTrigger(BlowUp);
            }
        }
        /// <summary>
        /// "Turns" On-Off specific gameobject without disabling it off completely 
        /// </summary>
        /// <param name="onOff"></param>
        public void Activate(OnOff onOff)
        {
            _renderer.enabled = onOff == OnOff.On;
            _collider2D.enabled = onOff == OnOff.On;
            _anim.StopPlayback();

        }

        public void Destroy()
        {
            GameObject.Destroy(this);
        }

        public void SetData(IProjectileData projData)
        {
            _renderer.sprite = projData.SpriteOfProjectile;
        }
    }
}