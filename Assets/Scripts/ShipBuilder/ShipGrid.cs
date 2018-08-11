using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD42.Scripts.ShipBuilder {
	public class ShipGrid {
		private Dictionary<ShipComponent, LocationInformation> locations;
		private List<ShipComponent> components { get {
				return locations.Keys.ToList<ShipComponent>();
			}
		}

		public List<ShipComponent> GetAdjacentComponents(ShipComponent component) {
			List<ShipComponent> adjacents = new List<ShipComponent>();
			GetOccupiedTiles(component);
			return null;
		}

		public List<IntPair> GetOccupiedTiles(ShipComponent component) {
			List<IntPair> tiles = new List<IntPair>();
			foreach(IntPair i in component.Type.shape) {
				tiles.Add(i.Rotate(locations[component].rotation))
			}
		}

		public class LocationInformation {
			public IntPair location;
			public Rotation rotation;
		}

		
	}
}
