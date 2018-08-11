using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD42.Scripts.Configuration {
	public abstract class WeaponConfig {
		public bool available { get; protected set; }
		public int quantity { get; protected set; }
		public int size { get; protected set; }
		public int damage { get; protected set; }
	}
}
