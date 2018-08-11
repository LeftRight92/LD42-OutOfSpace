using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD42.Scripts.ShipBuilder {
	[Serializable]
	public class ComponentProperty {
		public string propertyIdentifier;
		public int amount;

		public ComponentProperty(string propertyIdentifier, int amount) {
			this.propertyIdentifier = propertyIdentifier;
			this.amount = amount;
		}
	}
}
