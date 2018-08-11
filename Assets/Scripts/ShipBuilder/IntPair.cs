using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD42.Scripts.ShipBuilder {
	[Serializable]
	public class IntPair {
		public int x, y;
		
		public IntPair(int x, int y) {
			this.x = x;
			this.y = y;
		}

		public IntPair Rotate(Rotation rotation) {
			RotationMatrix matrix = (RotationMatrix)Attribute.GetCustomAttribute(
				typeof(Rotation).GetField(Enum.GetName(typeof(Rotation), rotation)),
				typeof(RotationMatrix)
			);
			return new IntPair(
				(x * matrix.cosTheta) - (y * matrix.sinTheta),
				(x * matrix.sinTheta) + (y * matrix.cosTheta)
			);
		}

		public override bool Equals(object obj) {
			if (obj.GetType() != typeof(IntPair)) return false;
			IntPair o = (IntPair)obj;
			return o.x == x && o.y == y;
		}
	}
}
