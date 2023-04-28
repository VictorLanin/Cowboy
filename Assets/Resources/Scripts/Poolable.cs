
using UnityEngine;

namespace LaninCode
{ /// <summary>
  /// base class for object going into ObjectPools
  /// </summary>
    public abstract class Poolable:MonoBehaviour
    {
        public abstract void Activate(OnOff onOff);
        public abstract string Name { get; }
    }
}