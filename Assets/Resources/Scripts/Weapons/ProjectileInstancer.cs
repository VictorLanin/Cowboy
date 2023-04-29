using System.Collections;
using UnityEngine;

namespace LaninCode
{
    public class ProjectileInstancer
        {
            public bool CanInstantiate { get; private set; }
        
            private ProjectileInstancer(float delayToInstantiate)
            {
                DelayToInstantiate = delayToInstantiate;
                CanInstantiate = true;
            }

            public static ProjectileInstancer CreateInstance(float delayToInstantiate)
            {
                return new ProjectileInstancer(delayToInstantiate);
            }
        
            public float DelayToInstantiate { get; }

            public IEnumerator Delay()
            {
                CanInstantiate = false;
                for (float i = DelayToInstantiate; i > 0; i--)
                {
                    yield return new WaitForSeconds(1f);
                }
                CanInstantiate = true;
            } 
        }
    }
