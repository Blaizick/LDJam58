using UnityEngine;

namespace Banchy
{
    public class BuildingBoostrap : MonoBehaviour
    {
        public string id;
        public Building building;
        
        public void Init()
        {
            building.Type = Buildings.All.Find(b => b.Id == id);

            building.Init();
        }
    }
}