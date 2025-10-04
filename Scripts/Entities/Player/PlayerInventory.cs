using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class PlayerInventory : MonoBehaviour
    {
        public List<MonoItem> Items { get; set; } = new List<MonoItem>();
        public int MaxItems { get; private set; }

        public Transform anchor;

        public void Init()
        {
            MaxItems = 5;
        }

        public bool CanTake(MonoItem item)
        {
            return !Items.Contains(item) && Items.Count < MaxItems;
        }
        
        public void Take(MonoItem item)
        {
            Transform itemTransform = item._gameObject.transform;
            itemTransform.SetParent(anchor);
            itemTransform.localPosition = Vector3.zero;
            
            Items.Add(item);
        }
        public void Leave(MonoItem item)
        {
            Transform itemTransform = item._gameObject.transform;
            itemTransform.SetParent(null);
            
            Items.Remove(item);
        }
    }
}