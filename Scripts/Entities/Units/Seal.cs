using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Banchy
{
    public class SealType : UnitType, IDropItemsTypeComp
    {
        /// <summary>
        /// If player is in this radius seal will start escape.
        /// </summary>
        public float EscapeDistance { get; set; }
        /// <summary>
        /// How much can the escape angle deviate from the original.
        /// </summary>
        public float EscapeDeviation { get; set; }
        /// <summary>
        /// If seal will escape more than this value, he will stop to escape.
        /// </summary>
        public float EscapeDuration { get; set; }
        /// <summary>
        /// How long is seals escape way.
        /// </summary>
        public float EscapeLength { get; set; }

        public List<ItemStack> DropItems { get; set; }
    }

    public class Seal : Unit, IDropItemsComp
    {
        public SealType SealType
        {
            get
            {
                return (SealType)Type;
            }
        }

        public IDropItemsTypeComp DropItemsType
        {
            get
            {
                return (IDropItemsTypeComp)Type;
            }
        }

        public Vector2 EscapeTarget { get; set; }

        public bool Escaping { get; set; }
        public float CurEscapeTime { get; set; }
        
        public override void Init()
        {
            base.Init();
            
            StartLifecycle();
        }

        public void StartLifecycle()
        {
            Vars.Instance.sceneInjection.StartCoroutine(LifecycleCoroutine());
        }

        public override void _Destroy()
        {
            ((IDropItemsComp)this).DropItems(_gameObject.transform.position);
            
            base._Destroy();
        }

        public IEnumerator LifecycleCoroutine()
        {
            while (!Dead)
            {
                while (Vector2.Distance(Vars.Instance.player._gameObject.transform.position, _gameObject.transform.position) > SealType.EscapeDistance)
                {
                    // Debug.Log("Seal is waiting");
                    yield return null;
                    if (Dead)
                    {
                        yield break;
                    }
                }

                FindEscapePos();
                Escaping = true;
                // Debug.Log($"Seal escape pos is {EscapeTarget}");
            
                while (CurEscapeTime < SealType.EscapeDuration)
                {
                    // Debug.Log($"Seal is moving");
                    UpdateMovement();
                    CurEscapeTime += Time.deltaTime;
                    yield return null;
                    if (Dead)
                    {
                        yield break;
                    }
                }
                
                ResetTime();
                Escaping = false;
            }
        }

        public void FindEscapePos()
        {
            Vector2 curPos = _gameObject.transform.position;
            Vector2 playerPos = Vars.Instance.player._gameObject.transform.position;
            
            EscapeTarget = PositionUtils.FindEscapePosition(curPos, playerPos, SealType.EscapeDistance, SealType.EscapeDeviation);
        }

        public void UpdateMovement()
        {
            MoveAndRotateTo(EscapeTarget);
        }
        
        private void ResetTime()
        {
            CurEscapeTime = 0f;
        }

    }
}