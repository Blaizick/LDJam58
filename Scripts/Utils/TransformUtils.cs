using UnityEngine;

namespace Banchy
{
    public static class TransformUtils
    {
        public static void RotateTo(Transform transform, Vector2 target, float speed, float offset)
        {
            Vector2 curPos = transform.position;
            Vector2 dir = target - curPos;
            
            RotateAtDirection(transform, dir, speed, offset);
        }
        public static void RotateAtDirection(Transform transform, Vector2 direction, float speed, float offset)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            targetRotation.eulerAngles = new Vector3(0, 0, targetRotation.eulerAngles.z + offset);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }
}