using UnityEngine;

namespace Banchy
{
    public class Effects : MonoBehaviour
    {
        public SnowParticleSystem snowParticleSystem;

        public void Init()
        {
            snowParticleSystem.Init();
        }
    }
}