using System.Linq;
using UnityEngine;

namespace Banchy
{
    public class ExploserType : WeaponType
    {
        // public float AttackRange { get; set; }
        public RaycastAmmoType ExplosionAmmo { get; set; }
    }

    public class Exploser : Weapon
    {
        public Transform attackAnchor;

        public ExploserType ExploserType
        {
            get
            {
                return (ExploserType)Type;
            }
        }
        
        public override void Init()
        {
            base.Init();
        }

        public override void Use()
        {
            Use(ExploserType.ExplosionAmmo);
        }
        public void Use(RaycastAmmoType ammoType)
        {
            RaycastAmmo ammo = (RaycastAmmo)ammoType.ToAmmo(EnemyMask);
            
            Transform ammoTransform = ammo._gameObject.transform;
            ammoTransform.position = attackAnchor.position;

            ammo.raycastAnchor = attackAnchor;
            
            ammo.Init();
            
            base.Use();
        }
    }
}