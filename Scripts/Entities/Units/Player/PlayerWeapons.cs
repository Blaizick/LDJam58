using UnityEngine;

namespace Banchy
{
    public class PlayerWeapons : MonoBehaviour
    {
        public Bow bow;
        public Vector2 CursorWorldPos { get; set; }

        public float RotationOffset { get; set; } = 90f;
        
        public void Init()
        {
            bow.Init();
        }
        public void _Update()
        {
            Vector2 direction = CursorWorldPos - (Vector2)bow.bowAnchor.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            targetRotation.eulerAngles = new Vector3(0, 0, targetRotation.eulerAngles.z + RotationOffset);
            bow.bowAnchor.rotation = targetRotation;

            bow.ExtDamage = Vars.Instance.systems.AffectSystem.ExtDamage;
            bow.ReloadReduce = Vars.Instance.systems.AffectSystem.ReloadReduce;
        }

        public void _Destroy()
        {
            bow._Destroy();
            Destroy(bow._gameObject);
        }

        public bool TryShoot()
        {
            if (CanShoot())
            {
                Shoot();
            }
            return false;
        }
        
        public bool CanShoot()
        {
            return bow.CanUse();
        }
        public void Shoot()
        {
            bow.Use(Ammos.Arrow);
        }
    }
}