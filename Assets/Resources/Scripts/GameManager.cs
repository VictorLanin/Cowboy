using System;
using System.Collections.Generic;
using Resources.Scripts;
using UnityEngine;
using UnityEngine.Pool;

namespace LaninCode
{
    public class GameManager : MonoBehaviour
    {
        public static Player MainPlayer { get; private set; }

        public static Dictionary<TypeOfCursor, List<WeaponInGameObject>> AvailableWeapons =
            new Dictionary<TypeOfCursor, List<WeaponInGameObject>>()
            {

                { TypeOfCursor.Player, new List<WeaponInGameObject>(4) },
                { TypeOfCursor.Enemy, new List<WeaponInGameObject>(50) }
            };
        
        private void Awake()
        {
            MainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        
        public static WeaponInGameObject GetWeaponGameObject(TypeOfCursor cursorType,int idOfCursor)
        {
            var cursors = AvailableWeapons[cursorType];
            for (int i = 0; i < cursors.Count; i++)
            {
                if(idOfCursor!=cursors[i].gameObject.GetInstanceID()) continue;
                return cursors[i];
            }
            throw new NullReferenceException("no cursor with that id");
        }
        
        private void OnDestroy()
        {
            AvailableWeapons.Clear();
        }
    }
}