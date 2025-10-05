using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Banchy
{
    public class BonfireTimeFragment : MonoBehaviour
    {
        public TMP_Text text;

        public void Init()
        {
            
        }

        public void _Update()
        {
            List<BonfireBuilding> bonfires = Vars.Instance.systems.BuildingSystem.Bonfires;
            
            if (bonfires.Count == 0)
            {
                text.text = "No Bonfires Burning";
            }
            else
            {
                BonfireBuilding dieSoon = bonfires.First();            
                foreach (BonfireBuilding b in bonfires)
                {
                    if (dieSoon.RemainingTime >= b.RemainingTime)
                    {
                        dieSoon = b;
                    }
                }
                
                text.text = $"Bonfire Burning Time: {dieSoon.RemainingTime}";
            }
        }
    }
}