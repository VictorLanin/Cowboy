using System.Collections.Generic;
using UnityEngine;

namespace  LaninCode
{
    public  class PlayerDestructable : Destructable
    {
        private WeaponCursor _weaponCursor;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!_checker.CheckForAppropriateTag(col.tag)) return;
            if(_weaponCursor==null) _weaponCursor = GameManager.GetCursor(col.gameObject.GetInstanceID());
            var chosenWeapon = _weaponCursor.CurrentWeapon;
            if (!chosenWeapon.IsInstant) return;
            GetDamage(chosenWeapon.Damage);
        }
        
    }
}