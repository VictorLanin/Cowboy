using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    public class NonPlayerDestructable :Destructable
    {
        protected override void Awake()
        {
            base.Awake();
            _checker=CollisionChecker.CreateInstance(new List<string>(){"PlayerWeapon"});
        }
        

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!_checker.CheckForAppropriateTag(col.tag)) return;
            var weapon = PlayerCursor.WeapCursor.CurrentWeapon;
            if (!weapon.IsInstant)
            {
                GetDamage(weapon.Damage);
            }
            else
            {
                StartCoroutine(CheckForDamage());
            }
        }

        public IEnumerator CheckForDamage()
        {
            var weaponCursor = PlayerCursor.WeapCursor;
            while (Health>0)
            {
                
                if (!weaponCursor.IsShooting) yield return null;
                var weapon=weaponCursor.CurrentWeapon;
                GetDamage(weapon.Damage);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(!_checker.CheckForAppropriateTag(other.tag) ||  !_checker.IsObjectOutOfCollider(other)) return;
            StopCoroutine(CheckForDamage());
        }



    }
    
}