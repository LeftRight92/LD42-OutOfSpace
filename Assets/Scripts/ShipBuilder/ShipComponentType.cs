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
		public bool zoneBased;
		public ComponentAdjacencyBonus[] adjacencyBonuses;
		public ComponentProperty[] properties;
		//public Sprite component, destroyedComponent;
	}
}
