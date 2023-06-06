using System;

namespace LaninCode
{
    public class MachineGunData : PlayerWeaponData, IDamageDelay
    {
        public float DamageDelay =>0.5f;
        public override PlayerWeaponName Name => PlayerWeaponName.MachineGun;
        public override int Damage=>5;
    }
}