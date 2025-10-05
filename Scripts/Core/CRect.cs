using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Blaze
{
    [System.Serializable]
    public struct CRect : IFormattable, IEquatable<CRect>
    {
        [SerializeField] private float m_X;
        [SerializeField] private float m_Y;
        [SerializeField] private float m_Width;
        [SerializeField] private float m_Height;


        public float X
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return m_X;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                m_X = value;
            }
        }
        public float Y
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return m_Y;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                m_Y = value;
            }
        }
        public float Width
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return m_Width;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                m_Width = value;
            }
        }
        public float Height
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return m_Height;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                m_Height = value;
            }
        }

        public Vector2 Position
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Vector2(X, Y);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                X = value.x;
                Y = value.y;
            }
        }
        public Vector2 Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Vector2(Width, Height);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Width = value.x;
                Height = value.y;
            }
        }


        public float MinX
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return X - (Width / 2f);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                float min = value;
                float max = MaxX;
                Width = max - min;
                X = min + (Width / 2f);
            }
        }
        public float MaxX
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return X + (Width / 2f);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                float min = MinX;
                float max = value;
                Width = max - min;
                X = min + (Width / 2f);
            }
        }
        public float MinY
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Y - (Height / 2f);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                float min = value;
                float max = MaxY;
                Height = max - min;
                Y = min + (Height / 2f);
            }
        }
        public float MaxY
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Y + (Height / 2f);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                float min = MinY;
                float max = value;
                Height = max - min;
                Y = min + (Height / 2f);
            }
        }

        public Vector2 Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Vector2(MinX, MinY);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                MinX = value.x;
                MinY = value.y;
            }
        }
        public Vector2 Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Vector2(MaxX, MaxY);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                MaxX = value.x;
                MaxY = value.y;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRect(float x, float y, float width, float height)
        {
            m_X = x;
            m_Y = y;
            m_Width = width;
            m_Height = height;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRect(Vector2 position, Vector2 size) : this(position.x, position.y, size.x, size.y) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRect(CRect source)
        {
            m_X = source.X;
            m_Y = source.Y;
            m_Width = source.Width;
            m_Height = source.Height;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(CRect source)
        {
            m_X = source.X;
            m_Y = source.Y;
            m_Width = source.Width;
            m_Height = source.Height;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Vector2 point)
        {
            return point.x >= MinX && point.x < MaxX && point.y >= MinY && point.y < MaxY;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(CRect other)
        {
            return other.MaxX > MinX && other.MinX < MaxX && other.MaxY > MinY && other.MinY < MaxY;
        }

        public Vector2 ClampPosition(Vector2 position)
        {
            position.x = Mathf.Clamp(position.x, MinX, MaxX);
            position.y = Mathf.Clamp(position.y, MinY, MaxY);
            return position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(CRect lhs, CRect rhs)
        {
            return !(lhs == rhs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(CRect lhs, CRect rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Width == rhs.Width && lhs.Height == rhs.Height;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ (Width.GetHashCode() << 2) ^ (Y.GetHashCode() >> 2) ^ (Height.GetHashCode() >> 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other)
        {
            if (other is Rect other2)
            {
                return Equals(other2);
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(CRect other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return ToString(null, null);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format)
        {
            return ToString(format, null);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "F2";
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.InvariantCulture.NumberFormat;
            }

            return $"(X: {X.ToString(format, formatProvider)}, Y: {Y.ToString(format, formatProvider)}, Width: {Width.ToString(format, formatProvider)}, Height: {Height.ToString(format, formatProvider)}\nMinX: {MinX.ToString(format, formatProvider)}, MinY: {MinY.ToString(format, formatProvider)}, MaxX: {MaxX.ToString(format, formatProvider)}, MaxY: {MaxY.ToString(format, formatProvider)})";
        }


        public static CRect Zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new CRect(0, 0, 0, 0);
            }
        }
    }
}