using UnityEngine;
using UnityEngine.Serialization;

namespace Banchy
{
    public class _World : MonoBehaviour
    {
        public ItemsBootstrap itemsBootstrap;
        public BuildingsBootstrap buildingsBootstrap;
        
        public void Init()
        {
            itemsBootstrap.Init();
            buildingsBootstrap.Init();
        }
    }
}