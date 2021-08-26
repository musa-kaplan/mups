using System.Collections.Generic;
using UnityEngine;

namespace MusaUtils
{
    public static class GetRandom
    {
        public static Vector3 NewRotation(float maxX = 360f, float maxY = 360f, float maxZ = 360f)
        {
            var vec = new Vector3();
            vec.x = Random.Range(0, maxX);
            vec.y = Random.Range(0, maxY);
            vec.z = Random.Range(0, maxZ);
            return vec;
        }
        
        public static Quaternion NewQuaternion(float maxX = 1f, float maxY = 1f, float maxZ = 1f, float maxW = 1f)
        {
            var qua = new Quaternion();
            qua.x = Random.Range(0, maxX);
            qua.y = Random.Range(0, maxY);
            qua.z = Random.Range(0, maxZ);
            qua.w = Random.Range(0, maxW);
            return qua;
        }
        
        public static Color NewColor()
        {
            var clr = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
            return clr;
        }

        public static T FromArray<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        public static T FromList<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}
