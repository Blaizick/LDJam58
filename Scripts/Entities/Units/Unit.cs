using UnityEngine;

namespace Banchy
{
    public class Unit : MonoBehaviour, IUpdate
    {
        public UnitType Type { get; set; }
        public float Health { get; private set; }
        
        public GameObject _gameObject;
        public CircleCollider2D _collider;
        public Rigidbody2D _rigidbody;
        public SpriteRenderer spriteRenderer;
        
        public bool Dead { get; set; } = false;
        
        public virtual void Init()
        {
            Register();

            _collider.radius = Type.Size;
            Health = Type.MaxHealth;
            _gameObject.layer = LayerMask.NameToLayer(Type.LayerMaskName);
            spriteRenderer.sprite = Type.Sprite;
        }
        public virtual void _Update()
        {
            
        }
        public virtual void _Destroy()
        {
            Dead = true;
            Destroy(_gameObject);
            Unregister();
        }

        public virtual void Register()
        {
            Vars.Instance.systems.UnitSystem.Add(this);            
        }
        public virtual void Unregister()
        {
            Vars.Instance.systems.UnitSystem.RemoveDelayed(this);            
        }

        public virtual void AddHealth(float health)
        {
            Health += GetAcceptedHealth(health);
        }
        public virtual void RemoveHealth(float health)
        {
            if (Health - health <= 0)
            {
                _Destroy();
            }
            Health -= GetRemovedHealth(health);
        }
        
        public float GetAcceptedHealth(float health)
        {
            float pred = Health + health;
            if (pred >= Type.MaxHealth)
            {
                return health - (pred - Type.MaxHealth);
            }
            return health;
        }
        public float GetRemovedHealth(float health)
        {
            if (Health - health < 0)
            {
                return Health;
            }
            return health;
        }
        
        public void MoveAndRotateTo(Vector2 target)
        {
            Vector2 normalizedMoveDir = (target - (Vector2)_gameObject.transform.position).normalized;
            _rigidbody.linearVelocity = normalizedMoveDir * Type.MoveSpeed;
            
            TransformUtils.RotateAtDirection(_gameObject.transform, normalizedMoveDir, Type.RotationSpeed, Type.RotationOffset);
        }
        public void MoveTo(Vector2 target)
        {
            Vector2 normalizedMoveDir = (target - (Vector2)_gameObject.transform.position).normalized;
            _rigidbody.linearVelocity = normalizedMoveDir * Type.MoveSpeed;
        }
        public void RotateTo(Vector2 target)
        {
            TransformUtils.RotateTo(_gameObject.transform, target, Type.RotationSpeed, Type.RotationOffset);
        }
    }

    public class UnitSystem : CSystem<Unit> { }
}