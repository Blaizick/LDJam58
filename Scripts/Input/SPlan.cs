using UnityEngine;

namespace Banchy
{
    public class SPlan : MonoBehaviour
    {
        public GameObject _gameobject;
        public SpriteRenderer spriteRenderer;
        public BuildingType Type { get; set; }

        public bool canPlace = false;
        
        public static readonly Color validColor = new Color(0.65f,1f, 0.65f, 0.65f);
        public static readonly Color invalidColor = new Color(1f,0.65f, 0.65f, 0.65f);
        
        public bool Selected
        {
            get
            {
                return Type != null;
            }
        }
        
        public void Init()
        {
            
        }

        public void _Update()
        {
            if (Selected)
            {
                float playerDst = Vector2.Distance(_gameobject.transform.position,
                    Vars.Instance.player._gameObject.transform.position);
                canPlace = Type.CanPlace(transform.position) && playerDst <= Vars.Instance.player.PlaceRange;
                spriteRenderer.color = canPlace ? validColor : invalidColor;
            }
        }

        public void Show(BuildingType buildType)
        {
            Type = buildType;
            spriteRenderer.sprite = Type.Sprite;
            
            _gameobject.SetActive(true);            
        }

        public void Hide()
        {
            _gameobject.SetActive(false);
            Type = null;
        }
    }
}