using UnityEngine;

namespace Banchy
{
    public class Player : MonoBehaviour
    {
        public Vector2 InputMovement { get; set; }
        public float MovementSpeed { get; set; }
        public float TakeRange { get; set; }
        public float ThrowDistance { get; set; }
        
        public Rigidbody2D rb;
        public PlayerInventory inventory;
        public GameObject _gameObject;
        
        public void Init()
        {
            MovementSpeed = 8f;
            TakeRange = 1.5f;
            ThrowDistance = 3f;

            inventory.Init();
        }
        public void _Update()
        {
            rb.linearVelocity = InputMovement.normalized * MovementSpeed;
        }
    }
}
