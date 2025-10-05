using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class CubidumType : UnitType, IDropItemsTypeComp
    {
        public float TeleportDelay { get; set; }
        public float TeleportLength { get; set; }
        public float TeleportPlayerRange { get; set; }
        public List<ItemStack> DropItems { get; set; }
        
        public float ResettedReload { get; set; } 
    }
    
    public class Cubidum  : Unit, IDropItemsComp
    {
        private float TeleportReload { get; set; }
        private Vector2 TeleportPosition { get; set; }
        
        public CubidumType CubidumType
        {
            get
            {
                return (CubidumType)Type;
            }
        }

        public bool TeleportReady
        {
            get
            {
                return TeleportReload >= CubidumType.TeleportDelay;
            }
        }

        public Exploser exploser;
        
        public override void Init()
        {
            base.Init();
            
            InitExploser();
            StartLifecycle();
        }

        public override void _Destroy()
        {
            ((IDropItemsComp)this).DropItems(_gameObject.transform.position);
            DestroyExploser();
            
            base._Destroy();
        }

        public void StartLifecycle()
        {
            Vars.Instance.sceneInjection.StartCoroutine(LifecycleCoroutine());
        }
        public IEnumerator LifecycleCoroutine()
        {
            while (!Dead)
            {
                while (!TeleportReady)
                {
                    UpdateTeleportTime();
                    TryUseWeapon();
                    UpdateRotation();
                    yield return null;
                    if (Dead)
                    {
                        yield break;
                    }
                }
                FindTeleportPosition();
                Teleport();
                ResetTeleportReload();
                ResetWeaponReload();
            }
        }

        private void FindTeleportPosition()
        {
            Vector2 playerPos = Vars.Instance.player._gameObject.transform.position;
            Vector2 cursorPos = Vars.Instance.input.CursorWorldPosition;
            
            
            Vector2 lookDir = (cursorPos - playerPos).normalized;
            Vector2 backDir = -lookDir;
            Vector2 behindPos = playerPos + backDir * CubidumType.TeleportPlayerRange;
            
            Vector2 teleportOffset = behindPos - playerPos;
            teleportOffset = Vector2.ClampMagnitude(teleportOffset, CubidumType.TeleportLength);
            
            TeleportPosition = playerPos + teleportOffset;
        }

        private void InitExploser()
        {
            exploser.Type = Weapons.Exploser;
            exploser.EnemyMask = Type.EnemyMask;
            exploser.Init();
        }
        private void DestroyExploser()
        {
            exploser._Destroy();
        }
        
        private void Teleport()
        {
            _gameObject.transform.position = TeleportPosition;
        }
        
        private void ResetTeleportReload()
        {
            TeleportReload = 0;
        }

        private void UpdateTeleportTime()
        {
            TeleportReload += Time.deltaTime;
        }

        private void UpdateRotation()
        {
            RotateTo(Vars.Instance.player._gameObject.transform.position);
        }

        private void TryUseWeapon()
        {
            exploser.TryUse();
        }

        private void ResetWeaponReload()
        {
            exploser.CurReloadTime = exploser.ReloadTime - CubidumType.ResettedReload;
            // exploser.ResetReload();
        }

        public IDropItemsTypeComp DropItemsType
        {
            get
            {
                return (IDropItemsTypeComp)Type;
            }
        }
    }
}