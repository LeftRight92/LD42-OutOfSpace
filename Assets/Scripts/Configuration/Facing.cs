using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD42.Scripts.Configuration {
	public enum Facing {
		[FacingString("up")]
		UP,
		[FacingString("down")]
		DOWN,
		[FacingString("left")]
		LEFT,
		[FacingString("right")]
		RIGHT
	}

	public class FacingString : Attribute {
		public string name;

		internal FacingString(string name) {
			this.name = name;
		}
	}

	public static class FacingExtensions {
		public static string GetFacingString(this Facing facing) {
			return ((FacingString)Attribute.GetCustomAttribute(
				typeof(Facing).GetField(Enum.GetName(typeof(Facing), facing)),
				typeof(FacingString))).name;
		}
	}
}
