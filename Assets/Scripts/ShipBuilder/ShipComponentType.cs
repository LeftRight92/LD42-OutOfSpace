using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD42.Scripts.ShipBuilder {
	[Serializable]
	public class ShipComponentType {
		public IntPair[] shape;
		public string identifier;
		public int maxHealth;
		public ComponentAdjacencyBonus[] adjacencyBonuses;
		public ComponentProperty[] properties;

		public ShipComponentType WreckedType() {
			ShipComponentType t = new ShipComponentType();
			t.shape = shape;
			t.maxHealth = 0;
			t.adjacencyBonuses = new ComponentAdjacencyBonus[0];
			t.properties = new ComponentProperty[0];
			return t;
		}
	}
}
