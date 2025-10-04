using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class MonoBuilding : MonoBehaviour
    {
        public string id;
        public SpriteRenderer spriteRenderer;
        public GameObject _gameObject;
        public BuildingType Type { get; set; }
        
        public virtual void Init()
        {
            Type = Buildings.All.Find(b => b.Id == id);

            spriteRenderer.sprite = Type.Sprite;
            
            Vars.Instance.systems.buildingSystem.Add(this);
        }

        public virtual void _Update()
        {
            
        }

        public virtual void _Destroy()
        {
            Vars.Instance.systems.buildingSystem.Remove(this);
            
            Destroy(gameObject);
        }
    }

    public class BuildingSystem
    {
        public List<MonoBuilding> Buildings { get; private set; } = new List<MonoBuilding>();
        
        public void Add(MonoBuilding build)
        {
            Buildings.Add(build);
        }
        public void Remove(MonoBuilding build)
        {
            Buildings.Remove(build);
        }
        
        public void _Update()
        {
            foreach (var build in Buildings)
            {
                build._Update();
            }
        }
    }
}