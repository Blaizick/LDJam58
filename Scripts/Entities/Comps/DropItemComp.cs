using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public interface IDropItemsTypeComp
    {
        public List<ItemStack> DropItems { get; set; }
    }
    
    public interface IDropItemsComp
    {
        public IDropItemsTypeComp DropItemsType { get; }
        
        public void DropItems(Vector2 position, float itemsOffset = 0.33f, int itemsWidth = 3)
        {
            foreach (var stack in DropItemsType.DropItems)
            {
                ItemType itemType = stack.ItemType;
                for (int i = 0; i < stack.Amount; i++)
                {
                    int x = i % itemsWidth;
                    int y = i / itemsWidth;
                    Vector2 offset = new Vector2(x, y) * itemsOffset;
                    Vector2 pos = position + offset;
                    
                    MonoItem item = itemType.ToItem();
                    item._gameObject.transform.position = pos;
                    item.Init();
                }
            }
        }
    }
}