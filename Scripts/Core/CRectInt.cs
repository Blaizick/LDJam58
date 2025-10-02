using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace Blaze
{
    public struct CRectInt : IFormattable, IEquatable<CRectInt>
    {
        #region CenterDefinition
        // * Represents a center

        // Size == 4
        // # # # #
        // # # * #
        // # # # #
        // # # # #

        // Size == 5
        // # # # # #
        // # # # # #
        // # # * # #
        // # # # # #
        // # # # # #
        #endregion

        [SerializeField] private int m_Width;
        [SerializeField] private int m_Height;
        [SerializeField] private int m_X;
        [SerializeField] private int m_Y;

        public int X
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
        public int Y
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

        public int Width
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return m_Width;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                m_Width = Mathf.Abs(value);
            }
        }
        public int Height
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return m_Height;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                m_Height = Mathf.Abs(value);
            }
        }

        public Vector2Int Position
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Vector2Int(X, Y);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                X = value.x;
                Y = value.y;
            }
        }
        public Vector2Int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Vector2Int(Width, Height);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                Width = value.x;
                Height = value.y;
            }
        }


        public int MinX
        {
            get
            {
                return GetMin(X, Width);
            }
            set
            {
                int minX = value;
                int maxX = MaxX;

                Width = Mathf.Abs(maxX - minX) + 1;
                X = GetCenter(minX, Width);
            }
        }
        public int MinY
        {
            get
            {
                return GetMin(Y, Height);
            }
            set
            {
                int minY = value;
                int maxY = MaxY;

                Height = Mathf.Abs(maxY - minY) + 1;
                Y = GetCenter(minY, Height);
            }
        }
        public int MaxX
        {
            get
            {
                return GetMax(X, Width);
            }
            set
            {
                int minX = MinX;
                int maxX = value;

                Width = Mathf.Abs(maxX - minX) + 1;
                X = GetCenter(minX, Width);
            }
        }
        public int MaxY
        {
            get
            {
                return GetMax(Y, Height);
            }
            set
            {
                int minY = MinY;
                int maxY = value;

                Height = Mathf.Abs(maxY - minY) + 1;
                Y = GetCenter(minY, Height);
            }
        }

        public Vector2Int Min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Vector2Int(MinX, MinY);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                MinX = value.x;
                MinY = value.y;
            }
        }
        public Vector2Int Max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Vector2Int(MaxX, MaxY);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                MaxX = value.x;
                MaxY = value.y;
            }
        }


        public Vector2 GetCenter()
        {
            return (Vector2)Min + ((Vector2)Size / 2f);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetMin(int pos, int size)
        {
            return pos - Mathf.FloorToInt(size / 2f);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetMax(int pos, int size)
        {
            pos += Mathf.FloorToInt(size / 2f);
            if (size % 2 == 0)
            {
                pos -= 1;
            }
            return pos;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetCenter(int min, int size)
        {
            return min + Mathf.FloorToInt(size / 2f);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRectInt(int x, int y, int width, int height)
        {
            m_X = x;
            m_Y = y;
            m_Width = width;
            m_Height = height;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CRectInt(Vector2Int position, Vector2Int size)
        {
            m_X = position.x;
            m_Y = position.y;
            m_Width = size.x;
            m_Height = size.y;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetArea()
        {
            return Width * Height;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Int[] GetAllPositionsWithin()
        {
            if (Width == 0 || Height == 0)
            {
                return Array.Empty<Vector2Int>();
            }

            Vector2Int[] positions = new Vector2Int[GetArea()];

            int i = 0;
            for (int x = MinX; x <= MaxX; x++)
            {
                for (int y = MinY; y <= MaxY; y++)
                {
                    positions[i++] = new Vector2Int(x, y);
                }
            }

            return positions;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(CRectInt rect)
        {
            return rect.MinX <= MaxX && rect.MaxX >= MinX && rect.MinY <= MaxY && rect.MaxY >= MinY;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Vector2Int position)
        {
            return position.x >= MinX && position.x <= MaxX && position.y >= MinY && position.y <= MaxY;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(CRectInt rect)
        {
            return rect.MinX >= MinX && rect.MaxX <= MaxX && rect.MinY >= MinY && rect.MaxY <= MaxY;
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
            if (formatProvider == null)
            {
                formatProvider = CultureInfo.InvariantCulture.NumberFormat;
            }

            return $"(X:{X.ToString(format, formatProvider)}, Y:{Y.ToString(format, formatProvider)}, Width:{Width.ToString(format, formatProvider)}, Height:{Height.ToString(format, formatProvider)}) \n MinX: {MinX.ToString(format, formatProvider)}, MinY: {MinY.ToString(format, formatProvider)}, MaxX: {MaxX.ToString(format, formatProvider)}, MaxY: {MaxY.ToString(format, formatProvider)}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other)
        {
            if (!(other is CRectInt))
            {
                return false;
            }

            return Equals((CRectInt)other);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(CRectInt other)
        {
            return other.X == X && other.Y == Y && other.Width == Width && other.Height == Height;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            int hashCode = X.GetHashCode();
            int hashCode2 = Y.GetHashCode();
            int hashCode3 = Width.GetHashCode();
            int hashCode4 = Height.GetHashCode();
            return hashCode ^ (hashCode2 << 4) ^ (hashCode2 >> 28) ^ (hashCode3 >> 4) ^ (hashCode3 << 28) ^ (hashCode4 >> 4) ^ (hashCode4 << 28);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(CRectInt lhs, CRectInt rhs)
        {
            return !(lhs == rhs);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(CRectInt lhs, CRectInt rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Width == rhs.Width && lhs.Height == rhs.Height;
        }

        public static CRectInt Zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new CRectInt(0, 0, 0, 0);
            }
        }
        public static CRectInt One
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new CRectInt(0, 0, 1, 1);
            }
        }
    }
}