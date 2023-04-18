using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Resources.Scripts;
using Tests;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace LaninCode
{
    public class GameManager : MonoBehaviour
    {
        public static Player MainPlayer { get; private set; }
        private static Dictionary<string, WeaponBack> _weapons;
        public static List<WeaponCursor> UsedCursors { get; } = new List<WeaponCursor>(10);

        private  void Awake()
        {
            _weapons = CreateDict();
            MainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            
            Dictionary<string, WeaponBack> CreateDict()
            {
                var dict = new Dictionary<string, WeaponBack>();
                var weapon=WeaponBack.CreateInstance("MachineGun");
                dict.Add(weapon.Name,weapon);
                weapon = WeaponBack.CreateInstance("Test");
                dict.Add(weapon.Name,weapon);
                return dict;
            }

        }

        public static WeaponBack GetWeapon(string nameOfWeapon)
        {
            return _weapons[nameOfWeapon];
        }
        
        public static WeaponCursor GetCursor(int idOfCursor)
        {
            for (int i = 0; i < UsedCursors.Count; i++)
            {
                if(idOfCursor!=UsedCursors[i].gameObject.GetInstanceID()) continue;
                return UsedCursors[i];
            }
            throw new NullReferenceException("no cursor with that id");
        }

        private void OnDestroy()
        {
            UsedCursors.Clear();
        }
    }
}