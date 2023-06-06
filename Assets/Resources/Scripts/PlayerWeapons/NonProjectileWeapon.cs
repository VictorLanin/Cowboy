using System;
using System.Collections;
using UnityEngine;

namespace LaninCode
{
    public class NonProjectileWeapon : MonoBehaviour, IWeapon
    {
    private LineRenderer _lineRenderer;
    private Player _player;
    private IEnumerator _cor;
    private bool _corStarted = false;
    private Collider2D _collider;
    public Collider2D CursorCollider => _collider;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _collider = GetComponent<Collider2D>();
        _player = GetComponentInParent<Player>();
        _cor = CheckForDamage();
    }
    public bool CanDamage { get; set; }
    public void ApplyDamage(Destructible destructible)
    {
        destructible.GetDamage(WeaponData.Damage);
    }
    public string NameofWeapon => "NonProjectile";

    public IWeaponData WeaponData { get; set; }

    public void TryFiring()
    {
        if (_corStarted) return;
        _corStarted = true;
        StartCoroutine(_cor);
    }

    private IEnumerator CheckForDamage()
    {
        while (_corStarted)
        {
            CanDamage = Input.GetButton("Fire1");
            _lineRenderer.enabled = CanDamage;
            UpdateLineRenderer();
            if (!CanDamage)
            {
                _corStarted = false;
                StopCoroutine(_cor);
            }
            yield return null;
        }

        void UpdateLineRenderer()
        {
            _lineRenderer.SetPosition(0, _player.transform.position);
            _lineRenderer.SetPosition(1, transform.position);
        }

    }
    }
}