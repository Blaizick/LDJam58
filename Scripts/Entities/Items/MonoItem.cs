using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class MonoItem : MonoBehaviour
    {
        public string itemId;
        public SpriteRenderer spriteRenderer;
        public GameObject _gameObject;
        
        public ItemType Type { get; set; }
        
        public void Init()
        {
            Type = Items.All.Find(i => i.Id == itemId);

            spriteRenderer.sprite = Type.Sprite;

            Vars.Instance.systems.ItemSystem.Add(this);
        }
        public void _Update()
        {
            
        }
        public void _Destroy()
        {
            Vars.Instance.systems.ItemSystem.Remove(this);
            Destroy(_gameObject);
        }
    }

    public class ItemSystem
    {
        public List<MonoItem> Items { get; set; } = new();

        public void Add(MonoItem item)
        {
            Items.Add(item);
        }
        public void Remove(MonoItem item)
        {
            Items.Remove(item);
        }
        
        public void _Update()
        {
            foreach (var item in Items)
            {
                item._Update();
            }
        }

        public List<MonoItem> GetItemsWithin(Vector2 position, float radius)
        {
            List<MonoItem> items = new();

            foreach (var item in Items)
            {
                if (Vector2.Distance(item._gameObject.transform.position, position) <= radius)
                {
                    items.Add(item);
                }
            }

            return items;
        }
    }
}