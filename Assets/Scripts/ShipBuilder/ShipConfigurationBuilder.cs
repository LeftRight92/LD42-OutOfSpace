using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD42.Scripts.Configuration;
using UnityEngine;

namespace LD42.Scripts.ShipBuilder {
	public class ShipConfigurationBuilder {
		private const string SHIP_PROPERTY_TAG = "ship";

		private const string zoneBasedStringMarker = "%";

		[SerializeField]
		private ShipGrid grid;

		[SerializeField]
		private ShipConfigReader configReader;

		public ShipConfigurationBuilder(ShipGrid grid, ShipConfigReader reader) {
			this.grid = grid;
			this.configReader = reader;
			grid.OnComponentDestroyed += Grid_OnComponentDestroyed;
			grid.OnComponentMove += Grid_OnComponentMove;
			grid.OnDamageAndShield += Grid_OnDamageAndShield;
		}

		private void Grid_OnComponentMove(ShipComponent component, ShipGrid.LocationInformation location) {
			DeployShipConfiguration(BuildShipConfiguration(), false);
		}

		private void Grid_OnComponentDestroyed(ShipComponent component) {
			DeployShipConfiguration(BuildShipConfiguration(), false);
		}

		private void Grid_OnDamageAndShield() {
			DeployShipConfiguration(BuildShipConfiguration(), true);
		}

		public void DeployShipConfiguration(Dictionary<string, int> config, bool armourOnly) {
			configReader.UpdateConfig(config, armourOnly);
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
#if DEBUG
			String s = "";
			foreach (KeyValuePair<string, int> pair in shipConfig) s = String.Concat(s, pair.Key, ": ", pair.Value, "\n");
			Debug.Log(s);
#endif
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
			Facing facing = grid.GetZone(component);
			List<ComponentAdjacencyBonus> adjacencyBonuses = new List<ComponentAdjacencyBonus>();
			foreach(ComponentAdjacencyBonus bonus in component.Type.adjacencyBonuses) {
				if(bonus.propertyIdentifier.EndsWith(zoneBasedStringMarker)) {
					adjacencyBonuses.Add(new ComponentAdjacencyBonus(
						bonus.componentIdentifier,
						bonus.propertyIdentifier.Replace(zoneBasedStringMarker, facing.GetPropertyString()),
						bonus.amount));
				} else {
					adjacencyBonuses.Add(bonus);
				}
			}
			return adjacencyBonuses.ToArray();
		}

		private ComponentProperty[] ResolveLocations(ShipComponent component) {
			List<ComponentProperty> properties = new List<ComponentProperty>();
			Facing facing = grid.GetZone(component);
			properties.AddRange(ResolveLocations(facing, component.Type.properties));
			properties.AddRange(ResolveLocations(facing, component.Properties));
			return properties.ToArray();
		}

		private List<ComponentProperty> ResolveLocations(Facing f, ComponentProperty[] components) {
			List<ComponentProperty> properties = new List<ComponentProperty>();
			foreach (ComponentProperty bonus in components) {
				if (bonus.propertyIdentifier.EndsWith(zoneBasedStringMarker)) {
					properties.Add(new ComponentProperty(
						bonus.propertyIdentifier.Replace(zoneBasedStringMarker, f.GetPropertyString()),
						bonus.amount));
				} else {
					properties.Add(bonus);
				}
			}
			return properties.ToList();
		}
	}
}
