using System;
using UnityEngine;

namespace LaninCode
{
    public class CollidedWeaponInstance : MonoBehaviour
    {
        [SerializeField] private CollidedWeapon collidedWeapon;
        private void Awake()
        {
            name = collidedWeapon.Name.ToString();
        }
    }
}