using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD42.Scripts.ShipBuilder {
	[Serializable]
	public class ComponentAdjacencyBonus : ComponentProperty {
		public string componentIdentifier;

		public ComponentAdjacencyBonus(string componentIdentifier, string propertyIdentifier, int amount) : base(propertyIdentifier, amount) {
			this.componentIdentifier = componentIdentifier;
		}
	}
}
