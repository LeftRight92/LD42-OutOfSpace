using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD42.Scripts.Configuration;
using UnityEngine;

namespace LD42.Scripts.ShipBuilder {
	[Serializable]
	public class ShipGrid {
		[SerializeField]
		private int width = 9, height = 7, baySize = 3;
		[SerializeField]
		private IntPair entry = new IntPair(4, -2), exit = new IntPair(4, 8);

		private readonly string[] DAMAGE_PRIORITY_QUEUE =  new string[] {
			"component.systems.shield_large",
			"component.systems.shield",
			"component.systems.armour"
		};

		private Dictionary<ShipComponent, LocationInformation> locations;
		public List<ShipComponent> components { get {
				return locations.Keys.ToList<ShipComponent>();
			}
		}
		public event Action<ShipComponent, LocationInformation> OnComponentMove;
		public event Action<ShipComponent> OnComponentDestroyed;
		public event Action OnDamageAndShield;
		public event Action OnDeath;

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

		public void TakeDamage(Facing face) {
			List<ShipComponent> componentsInZone = components.Where(component => component.health > 0 && GetZone(component) == face).ToList();
			if (componentsInZone.Count == 0) {
				Die();
				return;
			}
			bool damageDone = false;
			foreach(string typeName in DAMAGE_PRIORITY_QUEUE) {
				List<ShipComponent> priorityComponents = componentsInZone.Where(component => component.Type.identifier == typeName).ToList();
				if(priorityComponents.Count > 0) {
					ReportDamage(priorityComponents[UnityEngine.Random.Range(0, priorityComponents.Count)].TakeDamage());
					damageDone = true;
					break;
				}
			}
			if (!damageDone) {
				ReportDamage(componentsInZone[UnityEngine.Random.Range(0, componentsInZone.Count)].TakeDamage());
			}
		}

		public void ReportDamage(ShipComponent c) {
			if(c == null) {
				OnDamageAndShield();
			} else {
				OnComponentDestroyed(c);
			}
		}

		public void Die() { Debug.Log("Oh no! We died!"); }

		public Facing GetZone(ShipComponent component) {
			IntPair p = locations[component].location;
			int mid = (int) Math.Floor(width / 2f);
			bool upOrLeft = (p.x >= mid ? p.x - 1 : p.x) + p.y < height;
			if((p.x > mid ? p.x -1 : p.x) > p.y) {
				return upOrLeft ? Facing.UP : Facing.RIGHT;
			} else {
				return upOrLeft ? Facing.LEFT : Facing.DOWN;
			}
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
