using System;
using System.Collections.Generic;
using System.Text;

namespace Cyclone.MciWrapper
{

    public static class MathHelper
    {

        public static int FixRange(int value, int min, int max)
        {
            if (max <= min) throw new ArgumentException("max ,min.");
            if (value > max) return max;
            if (value < min) return min;
            return value;
        }


        public static T FixRange<T>(T value, T min, T max) where T : struct, IComparable<T>
        {
            if (max.CompareTo(min) <= 0) throw new ArgumentException("max ,min.");
            if (value.CompareTo(max) > 0) return max;
            if (value.CompareTo(min) <= 0) return min;
            return value;
        }

        public static bool IsRangeIn(int value, int min, int max)
        {
            return (value >= min) && (value <= max);
        }

        public static bool IsRangeIn<T>(T value, T min, T max) where T : struct,IComparable
        {
            var val = (IComparable)value;
            return ((val.CompareTo(min) >= 0) && (val.CompareTo(max) <= 0));
        }


    }


}
