using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class BuildingsBootstrap : MonoBehaviour
    {
        public List<BuildingBoostrap> buildings = new();
        
        public void Init()
        {
            foreach (BuildingBoostrap build in buildings)
            {
                build.Init();
            }
        }
    }
}