using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42.Scripts.Configuration {
	public abstract class ShipConfig : MonoBehaviour {

		//parameter is true if only armour/shield change has occurred
		public event Action<bool> ShipConfigHasChanged;

		public abstract float Speed { get; }

		public abstract ArmourConfig this[Facing facing] { get; }
		public abstract WeaponConfig this[WeaponType type] { get; }

		public abstract bool TakeDamage(Facing facing);

		protected void EventShipConfigHasChanged(bool armourOnly) {
			if(ShipConfigHasChanged != null)
				ShipConfigHasChanged(armourOnly);
		}
	}
}


