using System;
using System.Collections.Generic;
using Banchy;
using UnityEngine;

namespace Banchy
{
    public class AffectsDataInjector : MonoBehaviour
    {
        void Start()
        {
        
        }

        void Update()
        {
        
        }
    }

    public class AffectData
    {
        
    }

    public class Affect
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        public Func<string> Description { get; set; }

        public int ExtInventorySlots { get; set; }
        public float ExtDamage { get; set; }
        public float ReloadSpeedReduce { get; set; }
        public float ExtPlayerSpeed { get; set; }

        public int BonfiresRequired { get; set; }

        public virtual bool ShouldEnable()
        {
            return Vars.Instance.systems.BuildingSystem.Bonfires.Count >= BonfiresRequired;
            // return false;
        }
    }

    public static class Affects
    {
        public static Affect Bonfire1 { get; set; }
        public static Affect Bonfire2 { get; set; }
        public static Affect Bonfire3 { get; set; }
        
        public static List<Affect> All { get; set; }

        public static void Init()
        {
            Bonfire1 = new Affect()
            {
                BonfiresRequired = 1,
                
                ExtInventorySlots = 5,
                ExtDamage = 1f,
                ReloadSpeedReduce = 0.05f,
                ExtPlayerSpeed = 0.5f,
            };
            Bonfire2 = new Affect()
            {
                BonfiresRequired = 2,
                
                ExtInventorySlots = 5,
                ExtDamage = 1f,
                ReloadSpeedReduce = 0.05f,
                ExtPlayerSpeed = 0.5f,
            };
            Bonfire3 = new Affect()
            {
                BonfiresRequired = 3,
                
                ExtInventorySlots = 6,
                ExtDamage = 1f,
                ReloadSpeedReduce = 0.05f,
                ExtPlayerSpeed = 0.5f,
            };

            All = new List<Affect>()
            {
                Bonfire1,
                Bonfire2,
                Bonfire3,
            };
        }
    }
}
