using System.Collections.Generic;
using UnityEngine;

namespace Blaze
{
    public class CGizmos : MonoBehaviour
    {
        public Queue<GizmosRequest> InstantRequests { get; private set; } = new();

        public void OnDrawGizmos()
        {
            if (InstantRequests != null)
            {
                while (InstantRequests.Count > 0)
                {
                    GizmosRequest request = InstantRequests.Dequeue();
                    Gizmos.color = request.Color;
                    request.Execute();
                }
            }
        }

        public void Draw(GizmosRequest request)
        {
            InstantRequests.Enqueue(request);
        }
    }

    public abstract class GizmosRequest
    {
        public Color Color { get; set; }

        public GizmosRequest(Color color)
        {
            Color = color;
        }

        public abstract void Execute();
    }
    public class DrawRectangleRequest : GizmosRequest
    {
        public Vector2 Center { get; set; }
        public Vector2 Size { get; set; }

        public DrawRectangleRequest(Vector2 center, Vector2 size, Color color) : base(color)
        {
            Center = center;
            Size = size;
        }

        public override void Execute()
        {
            Gizmos.DrawWireCube(Center, Size);
        }
    }

    public class DrawCircleRequest : GizmosRequest
    {
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        public DrawCircleRequest(Vector2 position, float radius, Color color) : base(color)
        {
            Position = position;
            Radius = radius;
        }

        public override void Execute()
        {
            Gizmos.DrawWireSphere(Position, Radius);
        }
    }
}