namespace LaninCode
{
    public abstract class WeaponBack
    {
        public readonly string Name;
        public readonly int Damage;
        
        public WeaponBack(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }
        
        public static WeaponBack CreateInstance(string name)
        {
            WeaponBack weapon;
            switch (name)
            {
                default: weapon=new MachineGunBack("MachineGun", 5,0.2f);
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