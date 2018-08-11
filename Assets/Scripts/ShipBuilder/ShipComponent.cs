using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LD42.Scripts.ShipBuilder {
	public class ShipComponent : MonoBehaviour {
		public int health { get; protected set; }
		[SerializeField] private ShipComponentType type;
		public ShipComponentType Type {
			get {
				return type;
			}
		}				

		void Start() {
			health = type.maxHealth;
		}
		
		public void TakeDamage() {
			if(--health < 0) {
				BecomeWreckage();
			}
		}

		public void BecomeWreckage() {

		}
	}
}
