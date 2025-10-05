using UnityEngine;

namespace Banchy
{
    public class InventoryItem : MonoBehaviour
    {
        public GameObject _gameObject;
        public SpriteRenderer spriteRenderer;
        
        public ItemType Type { get; set; }

        public void Init()
        {
            spriteRenderer.sprite = Type.Sprite;
        }
        
        public void _Destroy()
        {
            Destroy(_gameObject);
        }
    }
}