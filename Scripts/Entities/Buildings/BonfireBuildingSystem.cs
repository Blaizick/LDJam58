using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Banchy
{
    public class BonfireBuildingSystem
    {
        public BuildingType CurBonfire { get; set; } = Buildings.Bonfire2;

        public List<BuildingType> BonfireBuildings { get; set; }

        public void Init()
        {
            BonfireBuildings = new()
            {
                Buildings.Bonfire2,
                Buildings.Bonfire3,
                Buildings.Bonfire4,
            };
            
            Refresh();
        }
        
        public void Refresh()
        {
            CurBonfire = Buildings.Bonfire2;
            
            foreach (var bonfire in BonfireBuildings)
            {
                if (bonfire.BonfiresRequired <= CurBonfire.BonfiresRequired)
                {
                    continue;
                }
                if (bonfire.BonfiresRequired > Vars.Instance.systems.BuildingSystem.Bonfires.Count)
                {
                    continue;
                }
                
                CurBonfire = bonfire;
            }
        }
    }
}