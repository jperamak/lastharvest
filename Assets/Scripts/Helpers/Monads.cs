using System;

namespace Assets.Scripts.Helpers
{
    public static class Monads
    {
        public static TSource Do<TSource>(this TSource obj, Action<TSource> action)
            where TSource : class
        {
            if (obj != default(TSource))
            {
                action(obj);
            }
            return obj;
        }
    }
}
