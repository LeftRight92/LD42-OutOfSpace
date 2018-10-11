using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Utility;
using LD42.Scripts.Configuration;

namespace LD42.Scripts.Weapons
{
	public class WeaponsControl : MonoBehaviour
	{
		[SerializeField] public List<LocalisedWeapon> weapons;

		void Start()
		{
			for (int i = 0; i < weapons.Count; i++)
			{
				LocalisedWeapon container = weapons[i];

				container.weapon = Instantiate(
					container.weapon, 
					transform.TransformPoint(container.location), 
					transform.rotation, 
					transform
				);

				weapons[i] = container;
			}
		}

		public void AttemptToFire()
		{
            for (int i = 0; i < weapons.Count; i++)
			{
				weapons[i].weapon.AttemptToFire(gameObject);
            }
		}

		public void AttemptToFire(int index)
        {
			weapons[index].weapon.AttemptToFire(gameObject);
        }

        public void AttempToFire(WeaponType type)
        {
            foreach (LocalisedWeapon l in weapons)
            {
				if(l.weapon.type==type) l.weapon.AttemptToFire(gameObject);
            }
        }
	}

    [Serializable]
	public struct LocalisedWeapon{
		[SerializeField] public Weapon weapon;
		[SerializeField] public Vector3 location;
	}
}