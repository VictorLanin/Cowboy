using Codice.Client.Commands;

namespace LaninCode
{
    public static class Grenade
    {
        static Grenade()
        {
            MaxAmmo = 10;
            ReduceAmmoRate = 1;
            SpeedOfProjectile = 5f;
            DelayToInstantiate = 2f;
            InitialAmmo = 2;
            Damage = 20;
        }
        public static readonly int MaxAmmo;
        public static readonly int InitialAmmo;
        public static readonly int ReduceAmmoRate;
        public static readonly float SpeedOfProjectile;
        public static readonly float DelayToInstantiate;
        public static readonly int Damage;
        
    }
}