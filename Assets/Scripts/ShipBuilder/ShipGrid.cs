using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD42.Scripts.Configuration;
using UnityEngine;

namespace LD42.Scripts.ShipBuilder {
	public class ShipGrid {
		private Dictionary<ShipComponent, LocationInformation> locations;
		public List<ShipComponent> components { get {
				return locations.Keys.ToList<ShipComponent>();
			}
		}
		public event Action<ShipComponent, LocationInformation> OnComponentMove;
		public event Action<ShipComponent> OnComponentDestroyed;

		public ShipGrid() {
			locations = new Dictionary<ShipComponent, LocationInformation>();
		}

		public void AddComponent(ShipComponent component, IntPair location) {
			locations.Add(component, new LocationInformation(location, Rotation.UP));
		}

		public List<ShipComponent> GetAdjacentComponents(ShipComponent component) {
			List<IntPair> adjacentTiles = GetAdjacentTiles(component);
			return components.Where(i => GetOccupiedTiles(i).Intersect(adjacentTiles).Any()).ToList();
		}

		public Facing GetZone(ShipComponent component) {
			return Facing.UP;
		}

		private List<IntPair> GetOccupiedTiles(ShipComponent component) {
			Rotation rotation = locations[component].rotation;
			return component.Type.shape.Select(i => i.Rotate(rotation) + locations[component].location).ToList<IntPair>();
		}

		private List<IntPair> GetAdjacentTiles(ShipComponent component) {
			return GetAdjacentTiles(GetOccupiedTiles(component));
		}

		private List<IntPair> GetAdjacentTiles(List<IntPair> occupiedTiles) {
			return occupiedTiles.SelectMany(i => GetAdjacentTiles(i))
				.Where(i => !occupiedTiles.Contains(i))
				.Distinct()
				.ToList<IntPair>();
		}

		private List<IntPair> GetAdjacentTiles(IntPair i) {
			return new List<IntPair> {
				new IntPair(i.x + 1, i.y),
				new IntPair(i.x - 1, i.y),
				new IntPair(i.x, i.y + 1),
				new IntPair(i.x, i.y - 1),
			};
		}

		public struct LocationInformation {
			public IntPair location;
			public Rotation rotation;

			public LocationInformation(IntPair location, Rotation rotation) {
				this.location = location;
				this.rotation = rotation;
			}
		}
	}
}
