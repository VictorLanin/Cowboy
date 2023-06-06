using UnityEngine;

namespace LaninCode
{
    public interface IProjectileData:IWeaponData
    {
        public float SpeedOfProjectile { get; }
        public float DelayToInstantiate { get; }
        int HowManyInstances { get; }
        Sprite SpriteOfProjectile { get; }
    }
}