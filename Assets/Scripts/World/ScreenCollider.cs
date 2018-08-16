using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.World;

namespace LD42.Scripts.World
{
    public class ScreenCollider : MonoBehaviour
    {
        void Start()
        {
            Camera camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
			camera.gameObject.AddComponent<EdgeCollider2D>();

            UpdateCollider(camera);
        }

        public static void UpdateCollider(Camera camera)
        {
            Vector2 bottomLeft = ScreenTools.BottomLeft;
			Vector2 topRight = ScreenTools.TopRight;
            Vector2 dimensions = topRight - bottomLeft;

            EdgeCollider2D collider = camera.gameObject.GetComponent<EdgeCollider2D>();
            collider.points = new Vector2[]{
				new Vector2(ScreenTools.Left, ScreenTools.Bottom),
				new Vector2(ScreenTools.Left, ScreenTools.Top),
				new Vector2(ScreenTools.Right, ScreenTools.Top),
				new Vector2(ScreenTools.Right, ScreenTools.Bottom),
                new Vector2(ScreenTools.Left, ScreenTools.Bottom)
            };
        }

        //public static void UpdateCollider(Camera camera)
        //{
        //    Vector3 bottomLeft = camera.ViewportToWorldPoint(Vector2.zero);
        //    Vector3 topRight = camera.ViewportToWorldPoint(Vector2.one);
        //    Vector3 dimensions = topRight - bottomLeft;

        //    EdgeCollider2D collider = camera.gameObject.GetComponent<EdgeCollider2D>();
        //    collider.points = new Vector2[]{
        //        new Vector2(bottomLeft.x, bottomLeft.y),
        //        new Vector2(bottomLeft.x, topRight.y),
        //        new Vector2(topRight.x, topRight.y),
        //        new Vector2(topRight.x, bottomLeft.y),
        //        new Vector2(bottomLeft.x, bottomLeft.y)
        //    };
        //}
    }
}