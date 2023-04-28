namespace LaninCode
{
    public abstract class WeaponBack
    {

        public readonly WeaponName Name;
        public readonly int Damage;
        
        public WeaponBack(WeaponName name, int damage)
        {
            Name = name;
            Damage = damage;
        }
        
        public static WeaponBack CreateInstance(WeaponName name)
        {
            WeaponBack weapon;
            switch (name)
            {
                case WeaponName.Grenade:
                {
                    weapon = new GrenadeBack(WeaponName.Grenade, 20, 10, 1, 5f);
                    break;
                }
                default: weapon=new MachineGunBack(WeaponName.MachineGun, 5,0.2f);
                    break;
            }
            return weapon;
        }

        public virtual void ApplyDamage(Destructable destructable)
        {
            destructable.GetDamage(Damage);
        }

    }
}