using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace LaninCode
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ProjectilePointer : MonoBehaviour
    {
        private Animator _anim;
        private SpriteRenderer _renderer;
        private static Sprite _sprite;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
            _sprite = _renderer.sprite;
            _renderer.sprite = null;
            _renderer.enabled = true;
        }

        public void SetPoint(Vector3 vec)
        {
            _renderer.sprite = _sprite;
            transform.position = vec;
            //_anim.
        }

        public void Back()
        {
            _renderer.sprite = null;
            //anim.StopPlayback;

        }
    }
}