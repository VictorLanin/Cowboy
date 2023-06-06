using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    public class PlayerProjectileInstancer : MonoBehaviour
    {
        [SerializeField] private PlayerWeaponName playerWeaponName;
        private ProjectileInstancer _instancer;
        private Player _player;
        public ProjectileInstancer Instancer => _instancer;
        public PlayerWeaponName WeaponName => playerWeaponName;


        private void Awake()
        {
            _instancer = GetComponent<ProjectileInstancer>();
            _player = GetComponentInParent<Player>();
            Instancer.GetProjectileData += GetProjectileData;
            Instancer.GetPositions += () => new List<Vector3>{_player.CursorPosition};
            StartCoroutine(Instancer.SetProjectilesReady(_player.name));
        }

        private IProjectileData GetProjectileData()
        {
            return WeaponManager.GetPlayerWeapon(WeaponName) as IProjectileData;
        }
    }
}