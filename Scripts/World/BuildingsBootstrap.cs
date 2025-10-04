using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class BuildingsBootstrap : MonoBehaviour
    {
        public List<MonoBuilding> items = new();
        
        public void Init()
        {
            foreach (MonoBuilding build in items)
            {
                build.Init();
            }
        }
    }
}