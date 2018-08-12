using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42.Scripts.Weapons
{
	public class CannonfireController : MonoBehaviour
	{
		public float speed = 5;

		void Start(){
			GameObject weapon = transform.parent.gameObject;
            GameObject spawner = weapon.transform.parent.gameObject;
			transform.parent = null;

			//velocity = spawner.GetComponent<Rigidbody2D>().velocity;
		}

		void Update(){
			transform.position = Vector3.Lerp(transform.position, 
			                                  transform.position + 
			                                  gameObject.transform.up, 
			                                  speed * Time.deltaTime);
		}
	}
}