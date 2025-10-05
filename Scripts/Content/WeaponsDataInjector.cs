using System;
using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class WeaponsDataInjector : MonoBehaviour
    {
        public BowWeaponData bow;
        
        public void Inject()
        {
            bow.Inject(Weapons.Bow);
        }
    }
    [System.Serializable]
    public class WeaponData
    {
        public virtual void Inject(WeaponType weaponType)
        {
            
        }
    }

    public class WeaponType
    {
        public string Name { get; set; }
        public Func<string> Description { get; set; }

        public float ReloadTime { get; set; }
        
        public string Id { get; set; }

        public GameObject Prefab { get; set; }
        
        public virtual void Use()
        {
            
        }

        public virtual Weapon ToWeapon(LayerMask enemyMask)
        {
            GameObject instance = Vars.Instance.sceneInjection._Instantiate(Prefab);
            Weapon script = instance.GetComponent<Weapon>();
            script.Type = this;
            script.EnemyMask = enemyMask;
            return script;
        }
    }
    public static class Weapons
    {
        public static WeaponType Bow { get; private set; }
        public static WeaponType Exploser { get; private set; }
        public static WeaponType Iphuser { get; private set; }
        
        public static List<WeaponType> All { get; private set; }

        public static void Init()
        {
            Bow = new BowWeaponType()
            {
                Id = "Bow",
                
                Name = "Bow",
                Description = () => "Bow",
                
                ReloadTime = 1,
                Ammo = Ammos.Arrow
            };
            Exploser = new ExploserType()
            {
                Id = "Exploser",
                Name = "Exploser",
                Description = () => "Exploser",
                
                ReloadTime = 2f,
                ExplosionAmmo = Ammos.Explosie,
            };
            Iphuser = new IphuserType()
            {
                Id = "Iphuser",
                Name = "Iphuser",
                Description = () => "Iphuser",
                
                ReloadTime = 0.5f,
                Ammo = Ammos.BasicBullet,
            };

            All = new List<WeaponType>(new WeaponType[]
            {
                Bow
            });
        }
    }
}