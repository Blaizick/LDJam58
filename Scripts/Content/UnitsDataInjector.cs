using System;
using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class UnitsDataInjector : MonoBehaviour
    {
        public UnitData seal;
        public UnitData cubidium;
        public UnitData egy;
        public PlayerData player;
        
        public void Inject()
        {
            seal.Inject(Units.Seal);
            cubidium.Inject(Units.Cubidum);
            egy.Inject(Units.Egy);
            player.Inject(Units.Player);
        }
        
    }
    [System.Serializable]
    public class UnitData
    {
        public Sprite sprite;
        public Sprite icon;

        public GameObject prefab;
        
        public virtual void Inject(UnitType unitType)
        {
            unitType.Sprite = sprite;
            unitType.Icon = icon;
            
            unitType.Prefab = prefab;
        }
    }

    public class UnitType
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        public Func<string> Description { get; set; }
        
        public float Size { get; set; }
        
        public float MaxHealth { get; set; }
        public float MoveSpeed { get; set; }
        public float RotationSpeed { get; set; }
        public float RotationOffset { get; set; }
        public Sprite Sprite { get; set; }
        public Sprite Icon { get; set; }
        
        public string LayerMaskName { get; set; }
        public LayerMask EnemyMask { get; set; }
        
        public GameObject Prefab { get; set; }
        
        public virtual Unit ToUnit()
        {
            GameObject instance = Vars.Instance.sceneInjection._Instantiate(Prefab);
            Unit script = instance.GetComponent<Unit>();
            instance.layer = LayerMask.NameToLayer(LayerMaskName);
            script.Type = this;
            return script;
        }
    }
    public static class Units
    {
        public static UnitType Seal { get; private set; }
        public static UnitType Cubidum { get; private set; }
        public static UnitType Egy { get; private set; }
        public static UnitType BonfireGod { get; private set; }
        public static UnitType Player { get; private set; }
        
        public static List<UnitType> All { get; private set; }

        public static void Init()
        {
            Seal = new SealType()
            {
                Id = "Seal",
                
                MaxHealth = 20f,
                MoveSpeed = 2.5f,
                
                LayerMaskName = LayerMasks.Enemy,
                EnemyMask = 1 << LayerMask.NameToLayer(LayerMasks.Player),
                
                Size = 0.5f,
                
                EscapeDistance = 15f,
                EscapeDuration = 1f,
                EscapeLength = 50f,
                EscapeDeviation = 45f,
                
                RotationSpeed = 480f,
                RotationOffset = 90f,
                
                DropItems = new()
                {
                    new ItemStack(Items.SmallBone, 1)
                }
            };
            Cubidum = new CubidumType()
            {
                Id = "Cubidium",
                
                MaxHealth = 20f,
                MoveSpeed = 2.5f,
                
                LayerMaskName = LayerMasks.Enemy,
                EnemyMask = 1 << LayerMask.NameToLayer(LayerMasks.Player),
                
                Size = 0.3f,
                
                TeleportDelay = 3f,
                TeleportLength = 10f,
                TeleportPlayerRange = 0.75f,
                ResettedReload = 0.5f,
                
                RotationSpeed = 480f,
                RotationOffset = 90f,
                
                DropItems = new()
                {
                    new ItemStack(Items.SmallBone, 2)
                }
            };
            Egy = new EgyType()
            {
                Id = "Egy",
                
                MaxHealth = 20f,
                MoveSpeed = 2.5f,
                
                LayerMaskName = LayerMasks.Enemy,
                EnemyMask = 1 << LayerMask.NameToLayer(LayerMasks.Player),
                
                Size = 0.3f,
                
                DashDelay = 3f,
                DashDuration = 2f,
                DashLength = 16f,
                DashDeviation = 45f,
                DashSpeed = 10,
                
                RotationSpeed = 480f,
                RotationOffset = 90f,
                
                DropItems = new()
                {
                    new ItemStack(Items.SmallBone, 2)
                }
            };
            BonfireGod = new BonfireGodType()
            {
                Id = "BonfireGod",
                
                MaxHealth = 20f,
                MoveSpeed = 2.5f,
                
                LayerMaskName = LayerMasks.Enemy,
                EnemyMask = 1 << LayerMask.NameToLayer(LayerMasks.Player),
                
                Size = 0.3f,
                
                RotationSpeed = 480f,
                RotationOffset = 90f,
            };
            Player = new PlayerType()
            {
                Id = "Player",
                
                MaxHealth = 20f,
                MoveSpeed = 6f,
                
                LayerMaskName = LayerMasks.Player,
                EnemyMask = 1 << LayerMask.NameToLayer(LayerMasks.Enemy),
                
                TakeRange = 1.5f,
                ThrowDistance = 10f,
                PlaceRange = 6f,
 
                Size = 0.1f,
                
                RotationSpeed = 720f,
                RotationOffset = 90f,
                
                WalkingAnimationDelay = 0.5f,
            };
            
            All = new (new []
            {
                Seal,
                Cubidum,
                Egy,
                BonfireGod,
                Player
            });
        }
    }
}