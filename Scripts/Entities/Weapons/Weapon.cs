using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

namespace Banchy
{
    public class Weapon : MonoBehaviour
    {
        public GameObject _gameObject;
        public SpriteRenderer spriteRenderer;
        
        public WeaponType Type { get; set; }
        public float CurReloadTime { get; set; } = 0;

        public float ReloadReduce { get; set; } = 0;
        
        
        public float ReloadTime
        {
            get
            {
                return Type.ReloadTime - ReloadReduce;
            }
        }
        
        public LayerMask EnemyMask { get; set; }
        
        public virtual bool Ready
        {
            get
            {
                return CurReloadTime >= ReloadTime;
            }
        }
        
        public virtual void Init()
        {
            Register();
        }

        public virtual void _Update()
        {
            UpdateReloading();            
        }

        public virtual void _Destroy()
        {
            Unregister();
            Destroy(_gameObject);
        }
        
        public virtual void UpdateReloading()
        {
            if (!Ready)
            {
                CurReloadTime += Time.deltaTime;
            }
        }

        public virtual void Register()
        {
            Vars.Instance.systems.WeaponSystem.Add(this);
        }

        public virtual void Unregister()
        {
            Vars.Instance.systems.WeaponSystem.Remove(this);
        }
        
        public virtual bool TryUse()
        {
            if (CanUse())
            {
                Use();
                return true;
            }
            return false;
        }
        public virtual bool CanUse()
        {
            return Ready;
        }

        public virtual void Use()
        {
            ResetReload();
        }
        
        public void ResetReload()
        {
            CurReloadTime = 0;
        }
    }

    public class WeaponSystem
    {
        public List<Weapon> All { get; set; } = new();

        public void Add(Weapon weapon)
        {
            All.Add(weapon);
        }
        public void Remove(Weapon weapon)
        {
            All.Remove(weapon);
        }

        public void _Update()
        {
            foreach (Weapon weapon in All)
            {
                weapon._Update();
            }
        }
    }
}