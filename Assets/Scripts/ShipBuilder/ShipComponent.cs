using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD42.Scripts.ShipBuilder {
	public class ShipComponent : MonoBehaviour {
		public int health { get; protected set; }
		[SerializeField] private ComponentProperty[] properties;
		public ComponentProperty[] Properties {
			get {
				List<ComponentProperty> p = properties.ToList();
				p.Add(new ComponentProperty(Configuration.ShipConfigReader.HULL_PROPERTY, health));
				return p.ToArray();
			}
		}
		public string[] damageBeforeHealth;
		[SerializeField] private ShipComponentType type;
		public ShipComponentType Type {
			get {
				return type;
			}
		}				

		void Start() {
			health = type.maxHealth;
		}
		
		public ShipComponent TakeDamage() {
			bool damageDone = false;
			if (damageBeforeHealth.Length > 0) {
				ComponentProperty prop = properties.Where(p => damageBeforeHealth.Contains(p.propertyIdentifier) && p.amount > 0).FirstOrDefault();
				if(prop != null) {
					prop.amount--;
					damageDone = true;
				}
			}
			if (!damageDone && --health <= 0) {
				BecomeWreckage();
				return this;
			}
			return null;
		}

		public void BecomeWreckage() {
			health = 0;
			type = type.WreckedType();
			properties = new ComponentProperty[0];
			damageBeforeHealth = new string[0];
			//Change sprite
		}
	}
}
