using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Configuration;
using LD42.Scripts.Utility;

namespace LD42.Scripts.Weapons
{
	[System.Serializable]
	public class Weapon : MonoBehaviour
	{
		[SerializeField] public GameObject weaponFire;
		[SerializeField, HideInInspector] public WeaponType type;
		[SerializeField] public float cooldown = 0.5f;
		//[SerializeField] public Vector3 location;

		[SerializeField] private bool available = true;
		[SerializeField] private int quantity = 1;
		[SerializeField] private int size = 1;
		[SerializeField] private int damage = 1;

		private float prevTime = float.NegativeInfinity;

		public void Start()
		{
			//if(weaponFire.GetComponent<ControllerCannon>()) type=WeaponType.CANNON;
			//if(weaponFire.GetComponent<ControllerBeam>()) type=WeaponType.BEAM;
            
			type = weaponFire.GetComponent<ControllerCannon>() ? WeaponType.CANNON : 
			          weaponFire.GetComponent<ControllerBeam>() ? WeaponType.BEAM : WeaponType.CANNON;
	}

		public bool AttemptToFire(GameObject spawner)
		{
			if (Time.time - prevTime >= cooldown)
			{
				Fire(spawner);
				prevTime = Time.time;
				return true;
			}
			else
			{
				return false;
			}
		}

		private void Fire(GameObject spawner)
		{
			GameObject projectile = Instantiate(weaponFire, transform);

			//if (spawner.transform.root.gameObject.tag == "Player")
			//      {
			//          projectile.layer = 10;
			//      }
			//      else
			//{
			//          projectile.layer = 11;
			//}

			projectile.layer = (spawner.transform.root.gameObject.tag == "Player") ? 10 : 11;

			projectile.transform.parent = gameObject.transform;
		}
	}
}