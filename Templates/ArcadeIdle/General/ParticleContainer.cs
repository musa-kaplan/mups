using System;
using System.Collections.Generic;
using UnityEngine;

namespace MusaUtils.Templates.ArcadeIdle.General
{
    public class ParticleContainer : MonoBehaviour
    {
        public static event Action<InfoClasses.ParticleType, Vector3> onParticlePlay;

        public static void ParticlePlay(InfoClasses.ParticleType pType, Vector3 pos) =>
            onParticlePlay?.Invoke(pType, pos);

        [SerializeField] private List<InfoClasses.ParticlesByType> particlesByType;

        private void PlayParticle(InfoClasses.ParticleType pType, Vector3 position)
        {
            var p = GetParticle(pType);
            p.transform.position = position;
            p.Play();
        }
        
        private ParticleSystem GetParticle(InfoClasses.ParticleType pType)
        {
            for (var i = 0; i < particlesByType.Count; i++)
            {
                if (particlesByType[i].particleType.Equals(pType))
                {
                    return particlesByType[i].particle;
                }
            }

            return null;
        }
        
        private void OnEnable()
        {
            onParticlePlay += PlayParticle;
        }

        private void OnDisable()
        {
            onParticlePlay -= PlayParticle;
        }
    }
}
