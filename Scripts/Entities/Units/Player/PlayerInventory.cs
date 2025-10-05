using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Banchy
{
    public class PlayerInventory : MonoBehaviour
    {
        public List<InventoryItem> Items { get; set; } = new();
        public int OverrideCapacity { get; private set; } = 5;

        public int Capacity
        {
            get
            {
                return OverrideCapacity + Vars.Instance.systems.AffectSystem.ExtInventorySlots;
            }
        }


        public Transform anchor;

        public void Init()
        {
            
        }

        public void _Destroy()
        {
            
        }
        
        public bool CanTake(ItemType itemType)
        {
            return Items.Count < Capacity;
        }
        
        

        public bool HasAll(ItemStack[] stacks)
        {
            foreach (var stack in stacks)
            {
                if (!Has(stack))
                {
                    return false;
                }
            }

            return true;
        }
        public bool Has(ItemStack stack)
        {
            int has = 0;
            foreach (InventoryItem item in Items)
            {
                if (item.Type == stack.ItemType)
                {
                    has++;
                }
            }
            return has >= stack.Amount;
        }


        public void TakeAll(ItemStack[] stacks)
        {
            foreach (var stack in stacks)
            {
                Take(stack);
            }
        }
        public void Take(ItemStack stack)
        {
            int left = stack.Amount;
            for (int  i = 0; i < Items.Count; i++)
            {
                InventoryItem item = Items[i];
                
                if (item.Type == stack.ItemType)
                {
                    item._Destroy();
                    Items.RemoveAt(i);
                    i--;
                    
                    if (--left <= 0)
                    {
                        return;
                    }
                }
            }
        }
        
        public void Take(MonoItem item)
        {
            Add(item.Type);
            item._Destroy();
        }
        public void Add(ItemType itemType)
        {
            InventoryItem invItem = itemType.ToInventoryItem();
            invItem.Init();

            Transform itemTransform = invItem._gameObject.transform;
            itemTransform.SetParent(anchor);
            itemTransform.localPosition = Vector3.zero;

            Items.Add(invItem);
        }

        public InventoryItem GetFirst(ItemType itemType)
        {
            return Items.FirstOrDefault(item => item.Type == itemType);
        }
        public void Remove(InventoryItem item)
        {
            // Debug.Log($"Removing: {item.Type.Name}");
            Items.Remove(item);
            item._gameObject.transform.SetParent(null);
        }
    }
}