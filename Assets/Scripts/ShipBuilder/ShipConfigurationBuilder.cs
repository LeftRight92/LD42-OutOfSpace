using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD42.Scripts.Configuration;
using UnityEngine;

namespace LD42.Scripts.ShipBuilder {
	public class ShipConfigurationBuilder {

		private const string zoneBasedStringMarker = "zoned";

		[SerializeField]
		private ShipGrid grid;

		public ShipConfigurationBuilder(ShipGrid grid) {
			this.grid = grid;
			grid.OnComponentDestroyed += Grid_OnComponentDestroyed;
			grid.OnComponentMove += Grid_OnComponentMove;
		}

		private void Grid_OnComponentMove(ShipComponent component, ShipGrid.LocationInformation location) {
			BuildShipConfiguration();
		}

		private void Grid_OnComponentDestroyed(ShipComponent component) {
			BuildShipConfiguration();
		}

		public Dictionary<string, int> BuildShipConfiguration() {
			List<ShipComponent> completedSet = new List<ShipComponent>();
			Dictionary<string, int> shipConfig = new Dictionary<string, int>();
			foreach(ShipComponent component in grid.components) {
				completedSet.Add(component);
				Aggregate(shipConfig, grid.GetAdjacentComponents(component)
					.Except(completedSet)
					.SelectMany(adj => ResolveLocationAdjacencies(adj))
					.Where(bonus => bonus.componentIdentifier == component.Type.identifier)
					.Select(adj => (ComponentProperty)adj)
					.ToArray()
				);
				Aggregate(shipConfig, ResolveLocations(component));
			}

			///
			String s = "";
			foreach (KeyValuePair<string, int> pair in shipConfig) s = String.Concat(s, pair.Key, ": ", pair.Value, "\n");
			Debug.Log(s);
			////

			return shipConfig;
		}

		private void Aggregate(Dictionary<string, int> shipConfig, ComponentProperty[] properties) {
			foreach(ComponentProperty c in properties) {
				if (shipConfig.ContainsKey(c.propertyIdentifier)) {
					shipConfig[c.propertyIdentifier] += c.amount;
				} else {
					shipConfig.Add(c.propertyIdentifier, c.amount);
				}
			}
		}

		private ComponentAdjacencyBonus[] ResolveLocationAdjacencies(ShipComponent component) {
			if (component.Type.zoneBased) {
				Facing facing = grid.GetZone(component);
				List<ComponentAdjacencyBonus> adjacencyBonuses = new List<ComponentAdjacencyBonus>();
				foreach(ComponentAdjacencyBonus bonus in component.Type.adjacencyBonuses) {
					if(bonus.propertyIdentifier.EndsWith(zoneBasedStringMarker)) {
						adjacencyBonuses.Add(new ComponentAdjacencyBonus(
							bonus.componentIdentifier,
							bonus.propertyIdentifier.Replace(zoneBasedStringMarker, facing.GetFacingString()),
							bonus.amount));
					} else {
						adjacencyBonuses.Add(bonus);
					}
				}
				return adjacencyBonuses.ToArray();
			} else {
				return component.Type.adjacencyBonuses;
			}
		}

		private ComponentProperty[] ResolveLocations(ShipComponent component) {
			if (component.Type.zoneBased) {
				Facing facing = grid.GetZone(component);
				List<ComponentProperty> properties = new List<ComponentProperty>();
				foreach (ComponentProperty bonus in component.Type.properties) {
					if (bonus.propertyIdentifier.EndsWith(zoneBasedStringMarker)) {
						properties.Add(new ComponentProperty(
							bonus.propertyIdentifier.Replace(zoneBasedStringMarker, facing.GetFacingString()),
							bonus.amount));
					} else {
						properties.Add(bonus);
					}
				}
				return properties.ToArray();
			} else {
				return component.Type.properties;
			}
		}
	}
}
