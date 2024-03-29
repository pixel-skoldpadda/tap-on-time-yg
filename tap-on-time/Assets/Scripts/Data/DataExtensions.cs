using System;
using UnityEngine;

namespace Data
{
    public static class DataExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public static T ToDeserialized<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static long UnixTimeStamp(this DateTime time)
        {
            return (long) time.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}