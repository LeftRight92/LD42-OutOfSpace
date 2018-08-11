using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD42.Scripts.ShipBuilder {
	class GridTester : MonoBehaviour {
		public ShipComponent component1, component2;
		public ShipGrid grid;

		public void Start() {
			grid = new ShipGrid();
			grid.AddComponent(component1, new IntPair(0, 0));
			grid.AddComponent(component2, new IntPair(1, 0));
			Debug.Log("Adjacent to Component 1: " + grid.GetAdjacentComponents(component1)[0].Type.identifier);
		}
	}
}
