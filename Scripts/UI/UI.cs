using UnityEngine;

namespace Banchy
{
    public class UI : MonoBehaviour
    {
        public BonfireTimeFragment  bonfireTimeFragment;
    
        public void Init()
        {
        
        }

        public void _Update()
        {
            bonfireTimeFragment._Update();
        }
    }
}
