﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Utility;
using LD42.Scripts.World;

namespace LD42.Scripts.World
{
	public static class ManageInstance
	{
		public static bool CheckOffScreen(this GameObject gameObject){
            Vector2 pos = gameObject.transform.position;
            
            return pos.x < ScreenTools.Left ||
                             pos.x > ScreenTools.Right ||
                             pos.y < ScreenTools.Bottom ||
                             pos.y > ScreenTools.Top;
		}

		public static void FullDestroy(this GameObject gameObject){
			//List<GameObject> children = new List<GameObject>();
   //         foreach (Transform child in gameObject.transform) children.Add(child.gameObject);

			//children.ForEach(FullDestroy);

            gameObject.Call(GameObject.Destroy);
		}

		public static Vector3 RoundXY(this Vector3 pos, float stepSize)
        {
            pos.x = Mathf.Round(pos.x / stepSize) * stepSize - pos.x;
            pos.y = Mathf.Round(pos.y / stepSize) * stepSize - pos.y;
            
            return pos;
        }
	}
}