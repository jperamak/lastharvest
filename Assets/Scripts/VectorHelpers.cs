using UnityEngine;

namespace Assets.Scripts
{
    public static class VectorHelpers
    {
        public static float AngleAtan(this Vector2 vec2)
        {
            if (vec2.x < 0)
            {
                return 360 - (Mathf.Atan2(vec2.x, vec2.y) * Mathf.Rad2Deg * -1);
            }
            return Mathf.Atan2(vec2.x, vec2.y) * Mathf.Rad2Deg;
        }
    }
}
