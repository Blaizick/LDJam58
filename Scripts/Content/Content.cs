using UnityEngine;

namespace Banchy
{
    public class Content : MonoBehaviour
    {
        public ItemsDataInjector itemsInjector;
        public BuildingsDataInjector buildingsInjector;
        
        public void Init()
        {
            Items.Init();
            Buildings.Init();
            
            itemsInjector.Inject();
            buildingsInjector.Inject();
        }
    }
}