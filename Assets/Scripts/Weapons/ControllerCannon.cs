using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.World;
using LD42.Scripts.Utility;

namespace LD42.Scripts.Weapons
{
	public class ControllerCannon : MonoBehaviour
	{
		[SerializeField] public float speed = 5;

		void Start(){
			GameObject weapon = transform.parent.gameObject;
            GameObject spawner = weapon.transform.parent.gameObject;
			transform.parent = null;
            
			//velocity = spawner.GetComponent<Rigidbody2D>().velocity;

			GameObject.FindWithTag("WorldManager").GetComponent<WorldManager>().update += _Update;
		}
        
		void _Update(){
            if (gameObject.CheckOffScreen()) _Destroy();

			transform.position = Vector3.LerpUnclamped(
				transform.position, 
				transform.position + gameObject.transform.up, 
				speed * Time.deltaTime
			);
		}

		void _Destroy(){
			GameObject.FindWithTag("WorldManager").GetComponent<WorldManager>().update -= _Update;         
			gameObject.Call(Destroy);
		}
	}
}