using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD42.Scripts.ShipBuilder {
	public enum Rotation {
		[RotationMatrix(1, 0)]
		UP,
		[RotationMatrix(-1, 0)]
		DOWN,
		[RotationMatrix(0, -1)]
		LEFT,
		[RotationMatrix(0, 1)]
		RIGHT
	}

	public class RotationMatrix : Attribute {
		public int cosTheta { get; private set; }
		public int sinTheta { get; private set; }

		internal RotationMatrix(int cosTheta, int sinTheta) {
			this.cosTheta = cosTheta;
			this.sinTheta = sinTheta;
		}
	}
}
