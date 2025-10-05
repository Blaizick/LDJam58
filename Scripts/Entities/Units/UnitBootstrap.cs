using UnityEngine;

namespace Banchy
{
    public class UnitBootstrap : MonoBehaviour
    {
        public Unit unit;
        public string unitType;
        
        public void Init()
        {
            unit.Type = Units.All.Find(u => u.Id == unitType);
            unit.Init();
            
        }
    }
}