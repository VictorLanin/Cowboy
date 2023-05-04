
using System;
using UnityEngine;

namespace LaninCode
{
    public class GrenadeAmmoPowerUp : PowerUp
    {
        private const int AmountOfAmmo = 1;
        private static Sprite _sprite;
        
        public override void AddPowerUp()
        {
            PlayerToGetPowerUp.AddAmmo(WeaponName.Grenade,AmountOfAmmo);
        }
        
        
    }
}