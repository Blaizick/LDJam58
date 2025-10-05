using UnityEngine;

namespace Banchy
{
    public class SnowParticleSystem : MonoBehaviour
    {
        public ParticleSystem _particleSystem;

        public float StartTime { get; set; } = 20f; 
    
        public void Init()
        {
            _particleSystem.Simulate(StartTime, true, true);
            _particleSystem.Play();
        }
    }
}
