using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Banchy
{
    public class Bonfire : BuildingType
    {
        /// <summary>
        /// In seconds 
        /// </summary>
        public float StartRemainingTime { get; set; }
    }
    
    public class BonfireBuilding : MonoBuilding
    {
        /// <summary>
        /// In seconds 
        /// </summary>
        public float RemainingTime { get; set; }

        public Bonfire Bonfire
        {
            get
            {
                return (Bonfire)Type;
            }
        }
        
        public override void Init()
        {
            base.Init();
            
            RemainingTime = Bonfire.StartRemainingTime;
        }

        public override void _Update()
        {
            RemainingTime -= Time.deltaTime;
        }
    }
}