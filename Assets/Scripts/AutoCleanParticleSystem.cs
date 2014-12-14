using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(ParticleSystem))]
    class AutoCleanParticleSystem : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        public void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void Update()
        {
            if (!_particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
