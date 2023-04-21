using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;

namespace LaninCode
{
    public class GameManager : MonoBehaviour
    {
        public static Player MainPlayer { get; private set; }
        public static List<WeaponCursor> UsedCursors { get; } = new List<WeaponCursor>(10);

        private  void Awake()
        {
            MainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            
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