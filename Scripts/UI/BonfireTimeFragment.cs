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
            BonfireBuilding bonfire = (BonfireBuilding)Vars.Instance.systems.buildingSystem.Buildings.FirstOrDefault(b => b is BonfireBuilding);
            if (bonfire == null)
            {
                text.text = "No Bonfires Burning";
            }
            else
            {
                text.text = $"Bonfire Burning Time: {bonfire.RemainingTime}";
            }
        }
    }
}