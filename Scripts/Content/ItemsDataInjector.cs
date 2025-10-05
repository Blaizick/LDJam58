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
            
            itemType.Prefab = itemData.prefab;
            itemType.ThrownPrefab = itemData.thrownPrefab;
            itemType.InventoryPrefab = itemData.inventoryPrefab;
        }
    }
    [System.Serializable]
    public class ItemData
    {
        public Sprite sprite;
        public Sprite icon;

        public GameObject prefab;
        public GameObject thrownPrefab;
        public GameObject inventoryPrefab;
    }
    
    public class ItemType
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        public Func<string> Description { get; set; }

        /// <summary>
        /// In secs
        /// </summary>
        public float ThrowTime { get; set; }
        
        public Sprite Sprite { get; set; }
        public Sprite Icon { get; set; }
        
        public GameObject Prefab { get; set; }
        public GameObject ThrownPrefab { get; set; }
        public GameObject InventoryPrefab { get; set; }


        public MonoItem ToItem()
        {
            GameObject instance = Vars.Instance.sceneInjection._Instantiate(Prefab);
            MonoItem monoItem = instance.GetComponent<MonoItem>();
            monoItem.Type = this;
            
            return monoItem;
        }

        public InventoryItem ToInventoryItem()
        {
            GameObject instance = Vars.Instance.sceneInjection._Instantiate(InventoryPrefab);
            InventoryItem invItem = instance.GetComponent<InventoryItem>();
            invItem.Type = this;
            
            return invItem;
        }

        public ThrownItem ToThrownItem()
        {
            GameObject instance = Vars.Instance.sceneInjection._Instantiate(ThrownPrefab);
            ThrownItem thrownItem = instance.GetComponent<ThrownItem>();
            thrownItem.Type = this;
            
            return thrownItem;
        }
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
                
                ThrowTime = 0.5f,
                
                BurningTime = 10
            };
            
            All = new List<ItemType>(new  ItemType[]
            { 
                SmallBone,
            });
        }
    }
}