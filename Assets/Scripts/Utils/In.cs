using System.Collections;

namespace Utils
{
    public  static class InExt
    {
            public static bool In<T>(this T val, params T[] values) where T : struct
            {
                return  ((IList) values).Contains(val);
            }
    }
}