using System;
using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class BuildingsDataInjector : MonoBehaviour
    {
        public BonfireData bonfire;
        
        public void Inject()
        {
            bonfire.Inject(Buildings.Bonfire1);
            bonfire.Inject(Buildings.Bonfire2);
            bonfire.Inject(Buildings.Bonfire3);
            bonfire.Inject(Buildings.Bonfire4);
        }
    }
    [System.Serializable]
    public class BuildingData
    {
        public Sprite sprite;
        public Sprite icon;
        public GameObject prefab;

        public virtual void Inject(BuildingType buildingType)
        {
            buildingType.Sprite = sprite;
            buildingType.Icon = icon;
            buildingType.Prefab = prefab;
        }
    }
    
    public class BuildingType
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        public Func<string> Description { get; set; }
        
        public Sprite Sprite { get; set; }
        public Sprite Icon { get; set; }

        public GameObject Prefab { get; set; }
        
        // public ItemStack[] BuildCost { get; set; }
        public Recipe Recipe { get; set; }
        
        /// <summary>
        /// Radius in which other buildings cant be placed
        /// </summary>
        public float Size { get; set; }
        
        public int BonfiresRequired { get; set; }
        
        public virtual bool CanPlace(Vector2 position)
        {
            List<Building> buildings = Vars.Instance.systems.BuildingSystem.All;
            foreach (Building build in buildings)
            {
                float minDst = Size + build.Type.Size;
                if (Vector2.Distance(position, build.transform.position) < minDst)
                {
                    return false;
                }
            }
            
            return Vars.Instance.player.inventory.HasAll(Recipe.Cost);
        }
        
        public virtual Building ToBuilding()
        {
            GameObject instance = Vars.Instance.sceneInjection._Instantiate(Prefab);
            Building script = instance.GetComponent<Building>();
            script.Type = this;
            // script.Init();
            
            return script;
        }
    }
    public static class Buildings
    {
        public static Bonfire Bonfire1 { get; private set; }
        public static Bonfire Bonfire2 { get; private set; }
        public static Bonfire Bonfire3 { get; private set; }
        public static Bonfire Bonfire4 { get; private set; }
        
        public static List<BuildingType> All { get; private set; }

        public static void Init()
        {
            Bonfire1 = new Bonfire()
            {
                Name = "Bonfire",
                Description = () => "Bonfire",
                
                Id = "Bonfire1",
                
                StartRemainingTime = 30f,
                MaxRemainingTime = 50f,
                
                Size = 1f,
                BonfiresRequired = 0,
            };
            Bonfire2 = new Bonfire()
            {
                Name = "Bonfire",
                Description = () => "Bonfire",
                
                Id = "Bonfire2",
                
                StartRemainingTime = 30f,
                MaxRemainingTime = 50f,
                
                Recipe = Recipes.Bonfire2,
                Size = 1f,
                BonfiresRequired = 1,
            };
            Bonfire3 = new Bonfire()
            {
                Name = "Bonfire",
                Description = () => "Bonfire",
                
                Id = "Bonfire3",
                
                StartRemainingTime = 30f,
                MaxRemainingTime = 50f,
                
                Recipe = Recipes.Bonfire3,
                Size = 1f,
                
                BonfiresRequired = 2,
            };
            Bonfire4 = new Bonfire()
            {
                Name = "Bonfire",
                Description = () => "Bonfire",
                
                Id = "Bonfire4",
                
                StartRemainingTime = 30f,
                MaxRemainingTime = 50f,
                
                Recipe = Recipes.Bonfire4,
                Size = 1f,
                
                BonfiresRequired = 3,
            };
            
            All = new List<BuildingType>(new BuildingType[]
            {
                Bonfire1,
                Bonfire2,
                Bonfire3,
                Bonfire4
            });
        }
    }
}