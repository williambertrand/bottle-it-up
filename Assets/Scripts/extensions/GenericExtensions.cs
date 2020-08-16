using System;

namespace extensions
{
    public static class GenericExtensions
    {
        public static void Let<T>(this T self, Action<T> block) 
        {
            block(self);
        }
        
        public static TV Let<T, TV>(this T self, Func<T, TV> block) 
        {
            return block(self);
        }
    }
}