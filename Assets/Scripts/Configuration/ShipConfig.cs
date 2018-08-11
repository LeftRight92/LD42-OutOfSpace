using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42.Scripts.Configuration {
	public abstract class ShipConfig : MonoBehaviour {

		public abstract float Speed { get; }

		public abstract ArmourConfig this[Facing facing] { get; }
		public abstract WeaponConfig this[WeaponType type] { get; }

		public abstract bool TakeDamage(Facing facing);
	}
}


