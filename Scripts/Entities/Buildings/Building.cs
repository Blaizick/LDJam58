using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class Building : MonoBehaviour, IUpdate
    {
        public SpriteRenderer spriteRenderer;
        public GameObject _gameObject;
        public BuildingType Type { get; set; }
        public bool Dead { get; set; } = false;
        
        public virtual void Init()
        {
            RefreshSprite();
            Register();
        }

        public virtual void _Update()
        {
            
        }

        public virtual void _Destroy()
        {
            Unregister();
            Destroy(gameObject);
            Dead = true;
        }
        
        public virtual void RefreshSprite()
        {
            spriteRenderer.sprite = Type.Sprite;
        }

        public virtual void Register()
        {
            Vars.Instance.systems.BuildingSystem.Add(this);
        }
        public virtual void Unregister()
        {
            Vars.Instance.systems.BuildingSystem.RemoveDelayed(this);
        }
    }

    public class BuildingSystem : CSystem<Building>
    {
        public List<BonfireBuilding> Bonfires { get; private set; } = new List<BonfireBuilding>();
        
        public override void Add(Building build)
        {
            if (build is BonfireBuilding)
            {
                Bonfires.Add((BonfireBuilding)build);
            }
            
            base.Add(build);
        }
        public override void Remove(Building build)
        {
            if (build is BonfireBuilding)
            {
                Bonfires.Remove((BonfireBuilding)build);
            }
            
            base.Remove(build);
        }
    }
}