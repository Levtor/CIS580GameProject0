using System;
using System.Collections.Generic;
using System.Text;

namespace CIS580GameProject0
{
    /// <summary>
    /// A bounding square for collision detection
    /// </summary>
    public struct BoundingSquare
    {
        public float X;

        public float Y;

        public float Length;

        public float Left => X;

        public float Right => X + Length;

        public float Top => Y;

        public float Bottom => Y + Length;

        public BoundingSquare(float x, float y, float length)
        {
            X = x;
            Y = y;
            Length = length;
        }

        /// <summary>
        /// Tests for collision between this and another BoundingRectangle
        /// </summary>
        /// <param name="other">The other BoundingRectangle</param>
        /// <returns>true for collision, false otherwise</returns>
        public bool CollidesWith(BoundingSquare other)
        {
            return !(this.Right < other.Left ||
                other.Right < this.Left ||
                this.Bottom < other.Top ||
                other.Bottom < this.Top);
        }
    }
}

