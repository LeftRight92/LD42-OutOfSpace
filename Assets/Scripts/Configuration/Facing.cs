using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD42.Scripts.Configuration {
	public enum Facing {
		[PropertyName("up")]
		UP,
		[PropertyName("down")]
		DOWN,
		[PropertyName("left")]
		LEFT,
		[PropertyName("right")]
		RIGHT
	}

	public class PropertyName : Attribute {
		public string name;

		internal PropertyName(string name) {
			this.name = name;
		}
	}

	public static class FacingExtensions {
		public static string GetPropertyString<T>(this T e) where T : struct, IConvertible {
			return ((PropertyName)Attribute.GetCustomAttribute(
				typeof(T).GetField(Enum.GetName(typeof(T), e)),
				typeof(PropertyName))).name;
		}
	}
}