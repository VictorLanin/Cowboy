using System;
using UnityEngine;

namespace LaninCode
{
    public class CollidedWeaponGameObject : MonoBehaviour
    {
        [SerializeField] private CollidedWeapon collidedWeapon;
        private void Awake()
        {
            name = collidedWeapon.Name.ToString();
        }
    }
}