namespace LaninCode
{
    public static class Revolver
    {
        static Revolver()
        {
            DamageDelay = 1f;
            MaxAmmo = 60;
            ReduceAmmoRate = 1;
            Damage = 20;
        }

        public static readonly float DamageDelay;
        public static readonly int MaxAmmo;
        public static readonly int ReduceAmmoRate;
        public static readonly int Damage;
    }
}