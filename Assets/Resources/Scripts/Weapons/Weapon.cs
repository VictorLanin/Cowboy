namespace LaninCode
{
    public abstract class Weapon
    {
        public abstract WeaponName Name { get; }
        public abstract int Damage { get; }

        public abstract bool CanDamage { get;  }
        
        public static Weapon CreateInstance(WeaponName name)
        {
            Weapon weapon;
            switch (name)
            {
                case WeaponName.Grenade:
                {
                    weapon = new GrenadeInstance();
                    break;
                }
                default: weapon=new MachineGunInstance();
                    break;
            }
            return weapon;
        }

        public virtual void ApplyDamage(Destructable destructable)
        {
            destructable.GetDamage(Damage);
        }
        public abstract void TryFiring(bool isFiring);
    }
}