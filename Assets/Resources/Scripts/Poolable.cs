
using UnityEngine;

namespace LaninCode
{
    //base class for object goind into ObjectPools
    public abstract class Poolable:MonoBehaviour
    {
        public abstract void Activate(OnOff onOff);
        public abstract string Name { get; }
    }
}