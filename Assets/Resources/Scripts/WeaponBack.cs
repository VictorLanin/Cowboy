namespace LaninCode
{
    public class WeaponBack
    {
        public readonly string Name;
        public readonly int Damage;
        public readonly bool IsInstant;
        public readonly int MaxAmmo;
        public readonly float AmmoDelay;
        
        
        
        public WeaponBack(string name, int damage, int maxAmmo, float ammoDelay, bool isInstant = false)
        {
            Name = name;
            Damage = damage;
            IsInstant = isInstant;
            MaxAmmo = maxAmmo;
            AmmoDelay = ammoDelay;
        }
        public static WeaponBack CreateInstance(string name)
        {
            WeaponBack weapon;
            switch (name)
            {
                case "MachineGun":  weapon=new WeaponBack("MachineGun", 1,100,7f);
                    break;
                default: weapon=new WeaponBack("Pistol", 5,-1,2f);
                    break;
            }
            return weapon;
        }


    }
}