using System;
using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class BuildingsDataInjector : MonoBehaviour
    {
        public BuildingData bonfire;
        
        public void Inject()
        {
            InjectItem(Buildings.Bonfire, bonfire);
        }
        
        private void InjectItem(BuildingType itemType, BuildingData itemData)
        {
            itemType.Sprite = itemData.sprite;
            itemType.Icon = itemData.icon;
        }
    }
    [System.Serializable]
    public class BuildingData
    {
        public Sprite sprite;
        public Sprite icon;
    }
    
    public class BuildingType
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        public Func<string> Description { get; set; }
        
        public Sprite Sprite { get; set; }
        public Sprite Icon { get; set; }
    }
    public static class Buildings
    {
        public static Bonfire Bonfire { get; private set; }
        
        public static List<BuildingType> All { get; private set; }

        public static void Init()
        {
            Bonfire = new Bonfire()
            {
                Name = "Bonfire",
                Description = () => "Bonfire",
                
                Id = "Bonfire",
                
                StartRemainingTime = 30f
            };
            
            All = new List<BuildingType>(new BuildingType[]
            {
                Bonfire,
            });
        }
    }
}