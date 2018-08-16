using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.World;

namespace LD42.Scripts.Weapons
{
	public class ControllerBeam : MonoBehaviour
	{
		public float duration = 1;

		void Start(){
			GameObject weapon = transform.parent.gameObject;
			GameObject spawner = weapon.transform.parent.gameObject;
			//transform.parent = null;

			//velocity = spawner.GetComponent<Rigidbody2D>().velocity;
		}

		void Update(){
			if (duration <= 0)
			{
				gameObject.FullDestroy();
			}
			duration -= Time.deltaTime;
		}
	}
}