using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LD42.Scripts.ShipBuilder;
using UnityEngine;

namespace LD42.Scripts.Configuration {
	class ShipConfigurationFacade : ShipConfig {
		private ShipConfigReader configReader;
		[SerializeField]
		private ShipGrid grid;

		public override event Action<bool> ShipConfigHasChanged {
			add {
				configReader.ShipConfigHasChanged += value;
			}
			remove {
				configReader.ShipConfigHasChanged -= value;
			}
		}

		public override event Action OnDeath {
			add {
				grid.OnDeath += value;
			}
			remove {
				grid.OnDeath -= value;
			}
		}

		public override float Speed {
			get {
				return configReader.Speed;
			}
		}

		public override WeaponConfig this[WeaponType type] {
			get {
				return configReader[type];
			}
		}

		public override ArmourConfig this[Facing facing] {
			get {
				return configReader[facing];
			}
		}

		private void Awake() {
			grid = new ShipGrid();
			ShipConfigurationBuilder configBuilder = new ShipConfigurationBuilder(grid);
			configReader = new ShipConfigReader(configBuilder);
		}

		public override void TakeDamage(Facing facing) {
			grid.TakeDamage(facing);
		}

		public override void AddComponent() {
			throw new NotImplementedException();
		}
	}
}
