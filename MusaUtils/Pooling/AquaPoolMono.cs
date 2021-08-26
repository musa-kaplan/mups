using System;
using System.Collections.Generic;
using UnityEngine;
using MusaUtils.Attributes;

namespace MusaUtils.Pooling
{
    public class AquaPoolMono : MonoBehaviour
    {
        public AquaPoolManager _poolManager;
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
