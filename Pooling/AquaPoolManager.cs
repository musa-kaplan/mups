using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace MusaUtils.Pooling
{
    public class AquaPoolManager
    {
        private static AquaPoolManager _instance;
        

        #region PRIVATE VARIABLES
        private static bool hasPool;
        private MonoPools _currentPool;
        private List<MonoPools> _pools;
        private GameObject _go;
        #endregion

        public static AquaPoolManager PoolInit()
        {
            _instance ??= new AquaPoolManager();
            return _instance;
        }


        public void Create(MonoPools a)
        {
            _pools ??= new List<MonoPools>();
            if (FindPool(a)) { Debug.LogError("You Already Have a Pool With Same Name: " + a._name); }
            else { _instance._pools.Add(new MonoPools(a)); }
        }
        
        public MonoPools GetPool(MonoPools pool)
        {
            return FindPool(pool) ? _currentPool : null;
        }

        public GameObject GetObject(MonoPools pool)
        {
            var currPool = GetPool(pool);
            if (FindPool(currPool))
            {
                if (_currentPool._objectList.Count <= 0)
                {
                    PoolCreation.LetsCreate(_currentPool._objectList, _currentPool._object, 1);
                }
                _go = GetRandom.FromList(_currentPool._objectList);
                _go.SetActive(true);
                _currentPool._objectList.Remove(_go);
                currPool._activatedCount++;
                
                if (pool._autoDestruct)
                {
                    ReturnObjectToPool(_currentPool, _go, pool._countDown);
                }
            }
            else { Debug.LogError("You Don't Have Any Pool With This Name: " + pool._name); }
            
            return FindPool(pool) ? _go : null;
        }

        private async void ReturnObjectToPool(MonoPools _pool, GameObject _object, float _countDown)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_countDown));
            _object.transform.localPosition = Vector3.zero;
            _pool._objectList.Add(_object);
            _object.SetActive(false);
            _pool._activatedCount--;
        }

        private bool FindPool(MonoPools pool)
        {
            hasPool = false;
            foreach (var p in _instance._pools)
            {
                if (p._name.Equals(pool._name))
                {
                    hasPool = true;
                    _currentPool = p;
                    break;
                }
            }

            return hasPool;
        }
    }
}
