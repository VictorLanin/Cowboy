using System.Collections.Generic;

namespace LaninCode
{
    public class BasicMech : IMech
    {
        private const int MechMaxHealth = 100;
        private static readonly List<PlayerWeaponName> MechAllowedWeapons =
            new() { PlayerWeaponName.MachineGun, PlayerWeaponName.Grenade };
        public int MaxHealth => MechMaxHealth;
        public List<PlayerWeaponName> AllowedWeapons => MechAllowedWeapons;
        private LinkedList<PlayerWeaponName> _equippedWeapons = new LinkedList<PlayerWeaponName>(new [] { PlayerWeaponName.MachineGun,PlayerWeaponName.Grenade });
        private const int MaxWeaponsNumber=3;
        private LinkedListNode<PlayerWeaponName> _currentNode;

        public BasicMech()
        {
            _currentNode = _equippedWeapons.First;
        }
        
        public void AddToWeapons(PlayerWeaponName playerWeaponName)
        {
            if (_equippedWeapons.Count >= MaxWeaponsNumber) return;
            _equippedWeapons.AddLast(playerWeaponName);
        }

        public PlayerWeaponName GetNextWeapon()
        {
            _currentNode = _currentNode.Next ?? _equippedWeapons.First;
            return _currentNode.Value;
        }
        
        public PlayerWeaponName GetPreviousWeapon()
        {
            _currentNode = _currentNode.Previous ?? _equippedWeapons.Last;
            return _currentNode.Value;
        }
        public PlayerWeaponName SelectedPlayerWeapon => _currentNode.Value;
    }
}