namespace LaninCode
{
    public class WeaponBack
    {
        public string Name { get; }
        public int Damage { get; }
        public bool IsInstant { get; } = false;
        
        public WeaponBack(string name, int damage, bool isInstant = false)
        {
            Name = name;
            Damage = damage;
            IsInstant = isInstant;
        }
        public static WeaponBack CreateInstance(string name)
        {
            WeaponBack weapon;
            switch (name)
            {
                case "MachineGun":  weapon=new WeaponBack("MachineGun", 1);
                    break;
                default: weapon=new WeaponBack("Test", 5,true);
                    break;
            }
            return weapon;
        }


    }
}