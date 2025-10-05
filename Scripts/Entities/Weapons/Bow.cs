using UnityEngine;
using UnityEngine.Serialization;

namespace Banchy
{
    [System.Serializable]
    public class BowWeaponData : ShootingWeaponData
    {
        public Sprite readySprite;
        public Sprite reloadSprite;

        public override void Inject(WeaponType weaponType)
        {
            ((BowWeaponType)weaponType).ReadySprite = readySprite;
            ((BowWeaponType)weaponType).ReloadSprite = reloadSprite;
        }
    }
    
    public class BowWeaponType : ShootingWeaponType
    {
        public Sprite ReadySprite { get; set; }
        public Sprite ReloadSprite { get; set; }
    }
    
    public class Bow : ShootingWeapon
    {
        public Transform bowAnchor;
        
        public BowWeaponType BowType
        {
            get
            {
                return (BowWeaponType)Type;
            }
        }

        public override void _Update()
        {
            base._Update();
            
            RefreshSprite();
        }

        public virtual void RefreshSprite()
        {
            spriteRenderer.sprite = Ready ? BowType.ReadySprite : BowType.ReloadSprite;
        }
    }
}