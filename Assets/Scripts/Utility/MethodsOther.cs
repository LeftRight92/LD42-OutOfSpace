using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42.Scripts.Utility
{
    public static class MethodsOther
    {
		public static T CastTo<T>(this object t)
        {
            return (T)t;
		}

        public static IList<T> AddTo<T>(this T t, IList<T> ts)
        {
            ts[ts.Count] = t;
            return ts;
        }

		public static Vector2 NewVec(this float f1, float f2, bool flip = false)
        {
			return flip==false? new Vector2(f1, f2) : new Vector2(f2, f1);
		}

        public static Vector3 NewVec(this float f1, float f2, float f3)
        {
            return new Vector3(f1, f2, f3);
        }
    }   
}