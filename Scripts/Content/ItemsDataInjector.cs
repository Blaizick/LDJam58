using System;
using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class ItemsDataInjector : MonoBehaviour
    {
        public ItemData smallBone;
        
        public void Inject()
        {
            InjectItem(Items.SmallBone, smallBone);
        }
        
        private void InjectItem(ItemType itemType, ItemData itemData)
        {
            itemType.Sprite = itemData.sprite;
            itemType.Icon = itemData.icon;
        }
    }
    [System.Serializable]
    public class ItemData
    {
        public Sprite sprite;
        public Sprite icon;
    }
    
    public class ItemType
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        public Func<string> Description { get; set; }
        
        public Sprite Sprite { get; set; }
        public Sprite Icon { get; set; }
    }

    public class BurningItemType : ItemType
    {
        public float BurningTime { get; set; } 
    }
    public static class Items
    {
        public static ItemType SmallBone { get; set; }
        
        public static List<ItemType> All { get; private set; }
        
        public static void Init()
        {
            SmallBone = new BurningItemType()
            {
                Id = "SmallBone",
                
                Name = "Small Bone",
                Description = () => "Small Bone",
                
                BurningTime = 10
            };
            
            All = new List<ItemType>(new  ItemType[]
            { 
                SmallBone,
            });
        }
    }
}