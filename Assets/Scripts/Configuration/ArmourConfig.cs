using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD42.Scripts.Configuration {
	public abstract class ArmourConfig {

		public int shieldMax { get; protected set; }
		public int shieldCurrent { get; protected set; }
		public float rechargeTime { get; protected set; }
		public int hullCurrent { get; protected set; }

		public abstract void rechargeShield();
	}
}
