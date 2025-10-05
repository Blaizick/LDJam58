using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class Ammo : MonoBehaviour, IUpdate
    {
        public GameObject _gameObject;
        public SpriteRenderer spriteRenderer;
        
        public AmmoType Type { get; set; }
        public float ExtDamage { get; set; }

        public float Damage
        {
            get
            {
                return Type.Damage + ExtDamage;
            }
        }

        public LayerMask enemyMask;
        
        public virtual void Init()
        {
            spriteRenderer.sprite = Type.Sprite;
            Register();
        }
        
        public virtual void _Update()
        {
            
        }

        public virtual void _Destroy(float delay = 0f)
        {
            Unregister();
            Destroy(_gameObject, delay);
        }

        public virtual void Register()
        {
            Vars.Instance.systems.AmmoSystem.Add(this);
        }

        public virtual void Unregister()
        {
            Vars.Instance.systems.AmmoSystem.RemoveDelayed(this);
        }
    }
    
    public class AmmoSystem : CSystem<Ammo> { }
}