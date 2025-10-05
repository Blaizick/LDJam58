using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Banchy
{
    public class AmmoDataInjector : MonoBehaviour
    {
        public AmmoData arrow;
        public AmmoData explosie;
        public AmmoData basicBullet;
        
        public void Inject()
        {
            InjectAmmo(Ammos.Arrow, arrow);
            InjectAmmo(Ammos.Explosie, explosie);
            InjectAmmo(Ammos.BasicBullet, basicBullet);
        }

        public void InjectAmmo(AmmoType ammoType, AmmoData ammoData)
        {
            ammoType.Sprite = ammoData.sprite;
            ammoType.Prefab = ammoData.prefab;
        }
    }
    [System.Serializable]
    public class AmmoData
    {
        public Sprite sprite;

        public GameObject prefab;
    }

    public class AmmoType
    {
        public Sprite Sprite { get; set; }
        public float Damage { get; set; }
        
        public GameObject Prefab { get; set; }

        public virtual bool CanDamage(GameObject target)
        {
            return true;
        }
        public virtual Ammo ToAmmo(LayerMask enemyMask)
        {
            GameObject instance = Vars.Instance.sceneInjection._Instantiate(Prefab);
            Ammo script = instance.GetComponent<Ammo>();
            script.enemyMask = enemyMask;
            script.Type = this;
            return script;
        }
    }

    public static class Ammos
    {
        public static BulletAmmoType Arrow { get;private set; }
        public static RaycastAmmoType Explosie { get; private set; }
        public static BulletAmmoType BasicBullet { get; private set; }
        
        public static List<AmmoType> All { get; private set; }
        
        public static void Init()
        {
            Arrow = new BulletAmmoType()
            {
                Damage = 5f,
                Speed = 20f,
                Size = 0.5f,
                LifeTime = 5f,
            };
            Explosie = new RaycastAmmoType()
            {
                Damage = 5f,
                RaycastRadius = 2f,
                DestroyDelay = 0.5f,
            };
            BasicBullet = new BulletAmmoType()
            {
                Damage = 5f,
                Speed = 10f,
                Size = 0.5f,
                LifeTime = 20f,
            };
            
            
            All = new List<AmmoType>(new []
            {
                Arrow,
            });
        }
    }
}