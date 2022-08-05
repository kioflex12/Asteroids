using UnityEngine;

namespace Utils
{
    public static class RandomUtils
    {
        public static bool GetRandomBool() =>
            Random.Range(0, 2) == 0;

        public static bool GetRandomBool(float chance) =>
            Random.Range(0.0f, 1.0f) < chance;
    }
}
