using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42.Scripts.World
{
	public class WorldManager : MonoBehaviour {
		[SerializeField] public Camera mainCamera;
		[SerializeField] public float cameraSize = 270f;
		[SerializeField] private float cameraVolume;
        [SerializeField] public int worldScale = 1;

        [SerializeField, HideInInspector] public Action update;
		//[SerializeField, HideInInspector] public Action fixedUpdate;
        private bool paused = false;

		//[HideInInspector] public bool Paused{
		//	get{return paused;}
		//	set
		//	{
		//		paused = value;
		//		Time.timeScale = value==true ? 0 : 1;
		//	}
		//}      

		void Start(){
			Physics2D.IgnoreLayerCollision(8, 10);
            Physics2D.IgnoreLayerCollision(9, 10);
            Physics2D.IgnoreLayerCollision(9, 11);
            Physics2D.IgnoreLayerCollision(10, 10);
            Physics2D.IgnoreLayerCollision(11, 11);
            Physics2D.IgnoreLayerCollision(11, 12);

			cameraSize = mainCamera.pixelHeight / 4;
			mainCamera.orthographicSize = cameraSize;
			float cameraWidth = cameraSize * mainCamera.pixelWidth / mainCamera.pixelHeight;
			cameraVolume = cameraSize * cameraWidth;

			ScreenTools.CalculateBounds();
			mainCamera.GetComponent<ScreenCollider>().UpdateCollider();         
		}

		void Update(){
			if (Input.GetButtonDown("Cancel")) PauseUnpause();         
			if(!paused) update.Invoke();
		}

		//void FixedUpdate(){
		//	if(!paused) fixedUpdate.Invoke();
		//}

		public void PauseUnpause(){
			paused = !paused;
			Time.timeScale = paused==true ? 0 : 1;
		}
	}
}