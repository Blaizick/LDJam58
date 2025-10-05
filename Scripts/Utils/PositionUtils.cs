using UnityEngine;

namespace Banchy
{
    public static class PositionUtils
    {
        public static Vector2 FindEscapePosition(Vector2 a, Vector2 b, float length, float deviation)
        {
            Vector2 dir = a - b;
            Vector2 normalizedDir = dir.normalized;
            
            float deviationAngle = Random.Range(-deviation / 2f, deviation / 2f);;

            float rad = deviationAngle * Mathf.Deg2Rad;
            Vector2 rotatedDir = new Vector2(
                (normalizedDir.x * Mathf.Cos(rad)) - (normalizedDir.y * Mathf.Sin(rad)),
                (normalizedDir.x * Mathf.Sin(rad)) + (normalizedDir.y * Mathf.Cos(rad))
            );
            
            return a + (rotatedDir * length);
        }
    }
}