using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Utility;
using LD42.Scripts.World;

namespace LD42.Scripts.World
{
	public static class ScreenTools
	{
        private static Vector2? bottomLeft;
        private static Vector2? topRight;
        private static Vector2 screenDims;

		public static Vector2 BottomLeft{
			get
            {
				if(bottomLeft == null) CalculateBounds();
				return bottomLeft.Value;
            }
		}
        
        public static Vector2 TopRight
        {
            get
            {
				if(topRight == null) CalculateBounds();
                return topRight.Value;
            }
		}

		public static Vector2 ScreenDims { get { return TopRight - BottomLeft; } }
		public static float Left { get { return bottomLeft.Value.x; } }
		public static float Right { get { return topRight.Value.x; } }
		public static float Top { get { return topRight.Value.y; } }
		public static float Bottom { get { return bottomLeft.Value.y; } }

		public static void CalculateBounds(){
			bottomLeft = GameObject.FindWithTag("WorldManager").
                                           GetComponent<WorldManager>().
                                           mainCamera.
                                           ViewportToWorldPoint(Vector2.zero);   
			topRight = GameObject.FindWithTag("WorldManager").
                                           GetComponent<WorldManager>().
                                           mainCamera.
                                           ViewportToWorldPoint(Vector2.one);
		}
	}   
}