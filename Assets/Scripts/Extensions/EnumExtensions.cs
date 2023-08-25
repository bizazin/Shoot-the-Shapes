using System;

namespace Extensions
{
    public static class EnumExtensions
    {
        public static T GetRandomValue<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        }
    }
}