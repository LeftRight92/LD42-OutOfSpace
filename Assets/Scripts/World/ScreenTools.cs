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
				if(bottomLeft == null)
					bottomLeft = GameObject.Find("WorldParameters").
					                       GetComponent<WorldParameters>().
                                           mainCamera.
					                       ViewportToWorldPoint(Vector2.zero);            
				return bottomLeft.Value;
            }
		}
        
        public static Vector2 TopRight
        {
            get
            {
                if (topRight == null)
					topRight = GameObject.Find("WorldParameters").
                                           GetComponent<WorldParameters>().
                                           mainCamera.
                                           ViewportToWorldPoint(Vector2.one);
                return topRight.Value;
            }
		}

		public static Vector2 ScreenDims { get { return TopRight - BottomLeft; } }
		public static float Left { get { return bottomLeft.Value.x; } }
		public static float Right { get { return topRight.Value.x; } }
		public static float Top { get { return topRight.Value.y; } }
		public static float Bottom { get { return bottomLeft.Value.y; } }
	}   
}