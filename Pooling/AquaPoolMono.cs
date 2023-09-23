using System.Collections.Generic;
using UnityEngine;

namespace MusaUtils.Pooling
{
    public class AquaPoolMono : MonoBehaviour
    {
        public List<MonoPools> _pools;

        private void Awake()
        {
            foreach (var p in _pools)
            {
                AquaPoolManager.PoolInit().Create(p);
            }
        }
    }
}
