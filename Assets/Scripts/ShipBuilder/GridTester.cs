using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD42.Scripts.Configuration;
using UnityEngine;

namespace LD42.Scripts.ShipBuilder {
	class GridTester : MonoBehaviour {
		public ShipComponent component1;
		public ShipGrid grid;
		public ShipConfigurationBuilder builder;
		public ShipConfigReader reader;

		public void Start() {
			grid = new ShipGrid();
			grid.AddComponent(component1, new IntPair(0, 0));
			//builder = new ShipConfigurationBuilder(grid, reader);
			//builder.DeployShipConfiguration(builder.BuildShipConfiguration(), false);
			Debug.Log("Pew");
			grid.TakeDamage(Facing.LEFT);
			Debug.Log("Pew");
			grid.TakeDamage(Facing.LEFT);
			Debug.Log("Pew");
			grid.TakeDamage(Facing.LEFT);
			//Debug.Log("Adjacent to Component 1: " + grid.GetAdjacentComponents(component1)[0].Type.identifier);
		}
	}
}
