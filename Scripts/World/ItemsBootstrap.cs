using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class ItemsBootstrap : MonoBehaviour
    {
        public List<MonoItem> items = new();
        
        public void Init()
        {
            foreach (MonoItem item in items)
            {
                item.Init();
            }
        }
    }
}