using System;
using System.Collections.Generic;
using System.Reflection;
using MusaUtils.Pooling;
using MusaUtils.RigidBody;
using UnityEngine;
using UnityEngine.UI;

namespace MusaUtils.DEMO.Scripts
{
    public class DemoPooling : MonoBehaviour
    {
        #region DEMO SCENE

        [SerializeField] private Text[] _activeText;
        [SerializeField] private Text[] _pooledText;
        private List<MonoPools> _pools;

        private void Start()
        {
            _pools = FindObjectOfType<AquaPoolMono>()._pools;
        }

        private void LateUpdate()
        {
            _activeText[0].text = "Activated: " + AquaPoolManager.PoolInit().GetPool(_pools[0])._activatedCount;
            _activeText[1].text = "Activated: " + AquaPoolManager.PoolInit().GetPool(_pools[1])._activatedCount;
            
            _pooledText[0].text = "Pooled: " + AquaPoolManager.PoolInit().GetPool(_pools[0])._objectList.Count;
            _pooledText[1].text = "Pooled: " + AquaPoolManager.PoolInit().GetPool(_pools[1])._objectList.Count;
        }

        public bool NamespaceExists()
        {
            foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach(Type type in assembly.GetTypes())
                {
                    if (type.Namespace == "MusaUtils.RigidBody")
                        return true;
                }
            }
            return false;
        }
        public void ThrowButton(MonoPools a)
        {
            ThrowObject(a);
        }
        
        private void ThrowObject(MonoPools a)
        {
            if (NamespaceExists())
            {
                QuickBody.GetRigid(AquaPoolManager.PoolInit().GetObject(a)
                    .GetComponent<Rigidbody>()).GoForward(Vector3.forward);
            }
            else
            {
                AquaPoolManager.PoolInit().GetObject(a).transform.Translate(Vector3.forward * 10f);
            }
        }

        #endregion
    }
}
