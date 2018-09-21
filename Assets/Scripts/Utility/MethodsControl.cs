using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42.Scripts.Utility
{
	public static class MethodsControl
	{
        public static void Foreach<T>(this IEnumerable<T> ts, Action<T> action)
        {
            foreach (T t in ts)
            {
                action.Invoke(t);
            }
        }

        public static void Foreach<T>(Action<T> action, params T[] ts)
        {
            foreach (T t in ts)
            {
                action.Invoke(t);
            }
        }

		public static void If(this bool b, Action action){
			if (b) action.Invoke();
		}

        public static void If(this Action action, bool b)
        {
            if (b) action.Invoke();
        }
	}
}