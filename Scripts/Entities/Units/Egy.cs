using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Banchy
{
    public class EgyType : UnitType, IDropItemsTypeComp
    {
        public float DashDuration { get; set; }
        public float DashLength { get; set; }
        public float DashSpeed { get; set; }
        public float DashDelay { get; set; }
        public float DashDeviation { get; set; }
        public List<ItemStack> DropItems { get; set; }
    }
    public class Egy : Unit, IDropItemsComp
    {
        public float DashReloadTime { get; private set; } = 0f;
        public float DashTime { get; private set; } = 0f;
        public Vector2 DashPosition { get; private set; }

        public Transform iphuserAnchor;
        public Iphuser iphuser;
        
        public EgyType EgyType
        {
            get
            {
                return (EgyType)Type;
            }
        }

        public bool DashReady
        {
            get
            {
                return DashReloadTime >= EgyType.DashDelay;
            }
        }

        public bool ContinueDash
        {
            get
            {
                return DashTime < EgyType.DashDuration;
            }
        }

        public override void Init()
        {
            base.Init();
            
            InitIphuser();
            StartLifecycle();
        }

        public override void _Destroy()
        {
            DestroyIphuser();
            ((IDropItemsComp)this).DropItems(_gameObject.transform.position);
            
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
                while (!DashReady)
                {
                    UpdateDashReload();
                    UpdateIphuserRotation();
                    iphuser.TryUse();
                    yield return null;
                    
                    if (Dead)
                    {
                        yield break;
                    }
                }
                
                FindDashPosition();

                while (ContinueDash)
                {
                    UpdateDashTime();
                    UpdateDashMovement();
                    yield return null;
                    
                    if (Dead)
                    {
                        yield break;
                    }
                }
                
                ResetDash();
            }
        }

        public void UpdateIphuserRotation()
        {
            TransformUtils.RotateTo(iphuserAnchor, Vars.Instance.player._gameObject.transform.position, Type.RotationSpeed, Type.RotationOffset);
        }
        
        public void UpdateDashReload()
        {
            DashReloadTime += Time.deltaTime;
        }

        public void UpdateDashTime()
        {
            DashTime += Time.deltaTime;
        }

        public void FindDashPosition()
        {
            Vector2 curPos = _gameObject.transform.position;
            Vector2 playerPos = Vars.Instance.player._gameObject.transform.position;
            
            DashPosition = PositionUtils.FindEscapePosition(curPos, playerPos, EgyType.DashLength, EgyType.DashDeviation);
        }

        public void UpdateDashMovement()
        {
            MoveTo(DashPosition);
        }

        public void ResetDash()
        {
            DashReloadTime = 0f;
            DashTime = 0f;
        }

        
        private void InitIphuser()
        {
            iphuser.Type = Weapons.Iphuser;
            iphuser.EnemyMask = Type.EnemyMask;
            
            iphuser.Init();
        }

        private void DestroyIphuser()
        {
            iphuser._Destroy();
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