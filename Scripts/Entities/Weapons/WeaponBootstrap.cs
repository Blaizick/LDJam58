using UnityEngine;

namespace Banchy
{
    public class WeaponBootstrap : MonoBehaviour
    {
        public string id;
        public LayerMask enemyMask;
        public Weapon weapon;
        
        public void Init()
        {
            weapon.EnemyMask = enemyMask;
            weapon.Type = Weapons.All.Find(w => w.Id == id);
        }
    }
}