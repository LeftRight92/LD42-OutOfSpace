using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD42.Scripts.Configuration {
	public class ShipConfigReader : ShipConfig {
		private Dictionary<string, int> config;

		private Dictionary<WeaponType, WeaponConfig> weaponConfigs;
		private Dictionary<Facing, ArmourConfig> armourConfigs;
		private float? speed;

		public override WeaponConfig this[WeaponType type] {
			get {
				if (weaponConfigs.ContainsKey(type)) return weaponConfigs[type];
				return ReadWeaponConfig(type);
			}
		}

		public const string WEAPON_AVAILABLE_PROPERTY = "ship.weapon.%.available";
		public const string WEAPON_QUANTITY_PROPERTY = "ship.weapon.%.quantity";
		public const string WEAPON_SIZE_PROPERTY = "ship.weapon.%.size";
		public const string WEAPON_DAMAGE_PROPERTY = "ship.weapon.%.damage";
		private WeaponConfig ReadWeaponConfig(WeaponType type) {
			weaponConfigs.Add(type, new WeaponConfigImpl(
				GetConfigPropertyOrZero(SubstitutePropertyString(WEAPON_AVAILABLE_PROPERTY, type)),
				GetConfigPropertyOrZero(SubstitutePropertyString(WEAPON_QUANTITY_PROPERTY, type)),
				GetConfigPropertyOrZero(SubstitutePropertyString(WEAPON_SIZE_PROPERTY, type)),
				GetConfigPropertyOrZero(SubstitutePropertyString(WEAPON_DAMAGE_PROPERTY, type))
			));
			return weaponConfigs[type];
		}

		private string SubstitutePropertyString<T>(string property, T type) where T : struct, IConvertible {
			return property.Replace("%", type.GetPropertyString());
		}

		private int GetConfigPropertyOrOne(string property) {
			return config.ContainsKey(property) ? config[property] : 1;
		}

		private int GetConfigPropertyOrZero(string property) {
			return config.ContainsKey(property) ? config[property] : 0;
		}

		public override ArmourConfig this[Facing facing] {
			get {
				if (armourConfigs.ContainsKey(facing)) return armourConfigs[facing];
				return ReadArmourConfig(facing);
			}
		}

		public const string HULL_PROPERTY = "ship.hull.%";
		public const string SHIELD_CAP_PROPERTY = "ship.shield.capacity.%";
		public const string SHIELD_CUR_PROPERTY = "ship.shield.current.%";
		public const string SHIELD_REC_PROPERTY = "ship.shield.recharge.%";
		private ArmourConfig ReadArmourConfig(Facing facing) {
			armourConfigs.Add(facing, new ArmourConfigImpl(
				GetConfigPropertyOrZero(SubstitutePropertyString(SHIELD_CAP_PROPERTY, facing)),
				GetConfigPropertyOrZero(SubstitutePropertyString(SHIELD_CUR_PROPERTY, facing)),
				GetConfigPropertyOrOne(SubstitutePropertyString(SHIELD_REC_PROPERTY, facing)),
				GetConfigPropertyOrZero(SubstitutePropertyString(HULL_PROPERTY, facing))
			));
			return armourConfigs[facing];
		}

		public override float Speed {
			get {
				if (speed.HasValue) return speed.Value;
				return ReadSpeed();
			}
		}

		public const string SPEED_PROPERTY = "ship.engines.speed";
		private float ReadSpeed() {
			speed = GetConfigPropertyOrOne(SPEED_PROPERTY);
			return speed.Value;
		}

		public override bool TakeDamage(Facing facing) {
			throw new NotImplementedException();
		}

		public void UpdateConfig(Dictionary<string, int> newConfig, bool armourOnly) {
			config = newConfig;
			armourConfigs = new Dictionary<Facing, ArmourConfig>();
#if DEBUG
			string s = "";
			foreach (Facing f in new Facing[]{ Facing.UP, Facing.DOWN, Facing.LEFT, Facing.RIGHT}) {
				ArmourConfig a = this[f];
				s += f.GetPropertyString().ToUpper() + " ARMOUR: hul: " + a.hullCurrent + " sMx: "
					+ a.shieldMax + " sCu: " + a.shieldCurrent + " sRe: " + a.rechargeTime + "\n";
			}
#endif
			if(!armourOnly) {
				speed = null;
				weaponConfigs = new Dictionary<WeaponType, WeaponConfig>();
#if DEBUG
				s += "SPEED: " + Speed + "\n";
				foreach (WeaponType t in new WeaponType[] { WeaponType.CANNON, WeaponType.BEAM, WeaponType.MISSILE, WeaponType.BOMB }) {
					WeaponConfig w = this[t];
					s += t.GetPropertyString().ToUpper() + " WEAPON: ava: " + w.available + " qnt: "
						+ w.quantity + " siz: " + w.size + " dmg: " + w.damage + "\n";
				}

			}
			Debug.Log(s);
#endif
			EventShipConfigHasChanged(armourOnly);
		}

		public class ArmourConfigImpl : ArmourConfig {
			public ArmourConfigImpl(int shieldMax, int shieldCurrent, int rechargeTime, int hullCurrent) {
				this.shieldMax = shieldMax;
				this.shieldCurrent = shieldCurrent;
				this.rechargeTime = rechargeTime;
				this.hullCurrent = hullCurrent;
			}
		}
		
		public class WeaponConfigImpl : WeaponConfig {
			public WeaponConfigImpl(int available, int quantity, int size, int damage) {
				this.available = available > 0;
				this.quantity = quantity;
				this.size = size;
				this.damage = damage;
			}
		}
	}
}
