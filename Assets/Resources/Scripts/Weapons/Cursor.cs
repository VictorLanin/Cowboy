using System.Collections.Generic;
using UnityEngine;

namespace LaninCode
{
    public class Cursor : MonoBehaviour
    {
        private static  Dictionary<TypeOfCursor, Dictionary<string,WeaponInGameObject>>_allCursors=new()
        {
            { TypeOfCursor.Player, new Dictionary<string,WeaponInGameObject>() },
            { TypeOfCursor.Enemy, new Dictionary<string,WeaponInGameObject>() }
        };
        
    }
}