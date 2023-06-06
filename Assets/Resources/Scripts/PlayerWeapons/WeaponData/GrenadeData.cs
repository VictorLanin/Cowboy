using System.ComponentModel;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LaninCode
{
    public class GrenadeData : PlayerWeaponData,ILimitedAmmo,IProjectileData
    {
        private Sprite _sprite;
        public override PlayerWeaponName Name => PlayerWeaponName.Grenade;
        public override int Damage => 5;
        public int MaxAmmo => 10;
        public int ReduceAmmoRate => 1;
        public int InitialAmmo => 2;
        public float SpeedOfProjectile => 5f;
        public float DelayToInstantiate => 2f;
        public int HowManyInstances => 1;
        public Sprite SpriteOfProjectile => _sprite;

        private async void Awake()
        {
             var handle = Addressables.LoadAssetAsync<Sprite>("Grenade");
             await handle.Task;
             _sprite = handle.Task.Result;
        }
    }
}