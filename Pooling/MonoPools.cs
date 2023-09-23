using System;
using System.Collections.Generic;
using UnityEngine;

namespace MusaUtils.Pooling
{
    [Serializable]
    [CreateAssetMenu(menuName = "MU/AquaPool", fileName = "NewPool")]
    public class MonoPools : ScriptableObject
    {
        public string _name;
        public GameObject _object;
        public int _startCount;

        public bool _autoDestruct;

        public float _countDown;

        [HideInInspector] public List<GameObject> _objectList;
        [HideInInspector] public int _activatedCount;
        
        public MonoPools(MonoPools a)
        {
            _name = a._name;
            _object = a._object;
            _startCount = a._startCount;
            
            _autoDestruct = a._autoDestruct;
            _countDown = a._countDown;
            
            _objectList ??= new List<GameObject>();
            _activatedCount = 0;
            PoolCreation.LetsCreate(_objectList, _object, _startCount);
        }
    }
}
