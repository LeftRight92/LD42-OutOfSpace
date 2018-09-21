using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.World; 

namespace LD42.Scripts.Weapons
{
	public class ControllerCannon : MonoBehaviour
	{
        [SerializeField] public WorldParameters parameters;
		[SerializeField] public float speed = 5;

		void Start(){
			GameObject weapon = transform.parent.gameObject;
            GameObject spawner = weapon.transform.parent.gameObject;
			transform.parent = null;
            
			//velocity = spawner.GetComponent<Rigidbody2D>().velocity;
		}
        
		void Update(){
			parameters = GetComponentInParent<WorldParameters>();

			transform.position = Vector3.LerpUnclamped(
				transform.position, 
				transform.position + gameObject.transform.up, 
				speed * Time.deltaTime
			);
			
			if(gameObject.CheckOffScreen()){
				gameObject.FullDestroy();
			}
		}
	}
}