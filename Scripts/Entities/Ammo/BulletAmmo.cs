using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Banchy
{
    public class BulletAmmoType : AmmoType
    {
        public float Speed { get; set; }
        public float LifeTime { get; set; }
        public float Size { get; set; }
    }    
    
    public class BulletAmmo : Ammo
    {
        public float CurLifeTime { get; set; }

        public CircleCollider2D _collider;
        
        public BulletAmmoType BulletAmmoType
        {
            get
            {
                return (BulletAmmoType)Type;
            }
        }

        public override void Init()
        {
            base.Init();
            
            _collider.radius = BulletAmmoType.Size;
        }
        
        public override void _Update()
        {
            base._Update();
            
            _gameObject.transform.Translate(_gameObject.transform.right * BulletAmmoType.Speed * Time.deltaTime, Space.World);

            if (CurLifeTime > BulletAmmoType.LifeTime)
            {
                _Destroy();
            }
            CurLifeTime += Time.deltaTime;

            Collider2D[] collisions = Physics2D.OverlapCircleAll(_gameObject.transform.position, BulletAmmoType.Size, enemyMask);
            foreach (Collider2D collision in collisions)
            {
                if (collision.gameObject.TryGetComponent(out Unit unit))
                {
                    unit.RemoveHealth(Damage);
                    _Destroy();
                    return;
                }
            }
        }
    }
}