using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Banchy
{
    [System.Serializable]
    public class PlayerData : UnitData
    {
        public Sprite[] walkingAnimation;

        public override void Inject(UnitType unitType)
        {
            base.Inject(unitType);
            
            ((PlayerType)unitType).WalkingAnimation = walkingAnimation;
        }
    }
    public class PlayerType : UnitType
    {
        public float TakeRange { get; set; }
        public float ThrowDistance { get; set; }
        public float PlaceRange { get; set; }
        
        public Sprite[] WalkingAnimation { get; set; }
        public float WalkingAnimationDelay { get; set; }
    }

    public class Player : Unit
    {
        public Vector2 InputMovement { get; set; }

        public PlayerType PlayerType
        {
            get
            {
                return (PlayerType)Type;
            }
        }
        
        // public float OverrideMovementSpeed { get; private set; } = 6f;
        // public float OverrideTakeRange { get; private set; } = 1.5f;
        // public float OverrideThrowDistance { get; private set; } = 10f;
        // public float OverrideRotationSpeed { get; private set; } = 720f;
        // public float OverridePlaceRange { get; private set; } = 6f;

        public float MovementSpeed
        {
            get
            {
                return PlayerType.MoveSpeed + Vars.Instance.systems.AffectSystem.ExtPlayerSpeed;
            }
        }
        public float TakeRange
        {
            get
            {
                return PlayerType.TakeRange;
            }
        }
        public float ThrowDistance
        {
            get
            {
                return PlayerType.ThrowDistance;
            }
        }
        public float RotationSpeed
        {
            get
            {
                return PlayerType.RotationSpeed;
            }
        }
        public float PlaceRange
        {
            get
            {
                return PlayerType.PlaceRange;
            }
        }


        private bool Walking { get; set; }
        private int CurSpriteId { get; set; }
        private Coroutine AnimationCoroutine { get; set; }
        
        // public Rigidbody2D rb;

        public PlayerInventory inventory;
        public PlayerWeapons weapons;
        

        public override void Init()
        {
            base.Init();
            
            inventory.Init();
            weapons.Init();
        }
        public override void _Update()
        {
            if (Dead)
            {
                return;
            }
            UpdateMovement();        
            UpdateAnimation();
            weapons._Update();
        }

        public override void _Destroy()
        {
            _gameObject.SetActive(false);
            Dead = true;            
            Vars.Instance.systems.LoseSystem.RefreshLose();
        }

        public void UpdateMovement()
        {
            Vector2 normalizedInput = InputMovement.normalized;
            
            _rigidbody.linearVelocity = normalizedInput * MovementSpeed;
            
            if (normalizedInput.sqrMagnitude >= 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, normalizedInput);
                _gameObject.transform.rotation = Quaternion.RotateTowards(_gameObject.transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            }

            Walking = InputMovement.sqrMagnitude > 0.1f;
        }

        
        public void UpdateAnimation()
        {
            if (Walking)
            {
                if (AnimationCoroutine == null)
                {
                    StartWalking();
                }
            }
            else
            {
                if (AnimationCoroutine != null)
                {
                    StopCoroutine(AnimationCoroutine);   
                }
                spriteRenderer.sprite = PlayerType.Sprite;
            }
        }

        private void StartWalking()
        {
            AnimationCoroutine = StartCoroutine(WalkingCoroutine());
        }
        private IEnumerator WalkingCoroutine()
        {
            CurSpriteId = 0;
            
            while (Walking)
            {
                if (CurSpriteId + 1 >= PlayerType.WalkingAnimation.Length)
                {
                    CurSpriteId = 0;
                }
                else
                {
                    CurSpriteId++;
                }
                
                spriteRenderer.sprite = PlayerType.WalkingAnimation[CurSpriteId];

                yield return new WaitForSeconds(PlayerType.WalkingAnimationDelay);
            }
        }
    }
}
