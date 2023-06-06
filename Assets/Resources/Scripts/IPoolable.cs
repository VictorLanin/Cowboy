
using UnityEngine;
using UnityEngine.Pool;

namespace LaninCode
{ /// <summary>
  /// base class for object going into ObjectPools
  /// </summary>
    public interface IPoolable
    {
        public void Activate(OnOff onOff);
        public string Name { get; }
        public void Destroy();
        public void SetPool(IObjectPool<IPoolable> pool);
    }
}