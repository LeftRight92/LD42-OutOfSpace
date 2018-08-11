using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42.Scripts.Configuration {
	public abstract class ShipConfig {

		public float speed { get; protected set; }

		public Dictionary<Facing, ArmourConfig> armourConfig { get; protected set; }

		public Dictionary<WeaponType, WeaponConfig> weapon { get; protected set; }

		public abstract bool TakeDamage(Facing facing);
	}
}


