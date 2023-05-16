using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LaninCode
{
    //todo подумать о poolable 
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class DamageOnCollisionGameObject : MonoBehaviour
    {
        [SerializeField] private string nameOfDamage="Barrel";
        private Rigidbody2D _rbody;
        private Collider2D _collider2D;
        private void Awake()
        {
            name = nameOfDamage;
            _rbody = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
        }
        
        private void OnBecameInvisible()
        {
            _rbody.simulated = false;
            _collider2D.enabled = false;
        }

        private void OnBecameVisible()
        {
            _rbody.simulated = true;
            _collider2D.enabled = true;
        }
    }
}