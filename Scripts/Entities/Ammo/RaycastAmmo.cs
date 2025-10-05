using System;
using UnityEngine;

namespace Banchy
{
    public class RaycastAmmoType : AmmoType
    {
        public float RaycastRadius { get; set; }
        public float DestroyDelay { get; set; }
    }
    public class RaycastAmmo : Ammo
    {
        public RaycastAmmoType RaycastAmmoType
        {
            get
            {
                return (RaycastAmmoType)Type;
            }
        }

        public Transform raycastAnchor;
        
        public override void Init()
        {
            base.Init();
            
            Collider2D[] collisions = Physics2D.OverlapCircleAll(raycastAnchor.position, RaycastAmmoType.RaycastRadius, enemyMask);
            foreach (var collision in collisions)
            {
                if (collision.gameObject.TryGetComponent(out Unit unit))
                {
                    unit.RemoveHealth(Damage);
                }
            }
            _Destroy(RaycastAmmoType.DestroyDelay);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color =  Color.red;
            Gizmos.DrawWireSphere(raycastAnchor.position, RaycastAmmoType.RaycastRadius);
        }
    }
}