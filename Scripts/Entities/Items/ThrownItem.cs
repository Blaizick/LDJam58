using DG.Tweening;
using UnityEngine;

namespace Banchy
{
    public class ThrownItem : MonoBehaviour
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
            // _gameObject.transform.DOKill();
            Destroy(_gameObject);
        }

        public void Throw(Vector2 throwPos)
        {
            _gameObject.transform.DOMove(throwPos, Type.ThrowTime).OnComplete(() =>
            {
                _Destroy();
            });
        }
    }
}