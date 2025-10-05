using System.Linq;
using UnityEngine;

namespace Banchy
{
    [System.Serializable]
    public class ShootingWeaponData : WeaponData
    {

    }
    
    [System.Serializable]
    public class ShootingWeaponType : WeaponType
    {
        public AmmoType Ammo { get; set; }
    }

    public class ShootingWeapon : Weapon
    {
        public ShootingWeaponType ShootingWeaponType
        {
            get
            {
                return (ShootingWeaponType)Type;
            }
        }
        
        public float ExtDamage { get; set; } = 0;
        public Transform bulletAnchor;

        public override void Init()
        {
            base.Init();
            
            ResetReload();
        }

        public override void Use()
        {
            Use(ShootingWeaponType.Ammo);
        }
        public virtual void Use(AmmoType ammoType)
        {
            Ammo ammo = ammoType.ToAmmo(EnemyMask);
            ammo.ExtDamage = ExtDamage;
            
            Transform ammoTransform = ammo._gameObject.transform;
            
            ammoTransform.position = bulletAnchor.position;
            ammoTransform.rotation = bulletAnchor.rotation;

            ammo.Init();
            
            base.Use();
        }
    }
}