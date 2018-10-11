using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.World;
using LD42.Scripts.Utility;

namespace LD42.Scripts.Weapons
{
	public class ControllerBeam : MonoBehaviour
	{
		public float duration = 1;

		void Start(){
			GameObject weapon = transform.parent.gameObject;
			GameObject spawner = weapon.transform.parent.gameObject;

            GameObject.FindWithTag("WorldManager").GetComponent<WorldManager>().update += _Update;
		}

		void _Update(){
			if (duration <= 0) _Destroy();

			duration -= Time.deltaTime;
		}

        void _Destroy()
        {
            GameObject.FindWithTag("WorldManager").GetComponent<WorldManager>().update -= _Update;         
            gameObject.Call(Destroy);
        }
	}
}