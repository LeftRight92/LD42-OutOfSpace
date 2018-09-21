using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42.Scripts.Utility
{
	public static class MethodsCall
	{
        public static void Call<T>(this T t, Action<T> action)
        {
            action.Invoke(t);
        }

        public static void Call<T, U>(this T t, Action<T, U> action, U u)
        {
            action.Invoke(t, u);
        }

        public static void Call<T, U>(this T t, Action<T, U[]> action, params U[] us)
        {
            action.Invoke(t, us);
        }

        public static U Call<T, U>(this T t, Func<T, U> func)
        {
            return func.Invoke(t);
        }

        public static V Call<T, U, V>(this T t, Func<T, U, V> func, U u)
        {
            return func.Invoke(t, u);
        }

        public static V Call<T, U, V>(this T t, Func<T, U[], V> func, params U[] us)
        {
            return func.Invoke(t, us);
        }

		public static void Call(Action<Vector2> action, params float[] vs){
			action.Invoke(new Vector2(vs[0], vs[1]));
		}

        public static W Call<T, U, V, W>(this T t, Func<T, U, V, W> func, U u, V v)
        {
            return func.Invoke(t, u, v);
        }

        public static T Call<T>(Func<Vector2, T> func, params float[] vs)
        {
            return func.Invoke(new Vector2(vs[0], vs[1]));
        }
	}
}