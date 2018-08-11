using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD42.Scripts.World
{
    public class ScreenCollider : MonoBehaviour
    {
        void Start()
        {
            Camera camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

            GenerateCollider(camera);
        }

        public static void GenerateCollider(Camera camera)
        {
            Vector3 bottomLeft = camera.ViewportToWorldPoint(Vector2.zero);
            Vector3 topRight = camera.ViewportToWorldPoint(Vector2.one);
            Vector3 dimensions = topRight - bottomLeft;

            EdgeCollider2D collider = camera.gameObject.AddComponent<EdgeCollider2D>();
            collider.points = new Vector2[]{
                new Vector2(bottomLeft.x, bottomLeft.y),
                new Vector2(bottomLeft.x, topRight.y),
                new Vector2(topRight.x, topRight.y),
                new Vector2(topRight.x, bottomLeft.y),
                new Vector2(bottomLeft.x, bottomLeft.y)
            };
        }

        public static void UpdateCollider(Camera camera)
        {
            Vector3 bottomLeft = camera.ViewportToWorldPoint(Vector2.zero);
            Vector3 topRight = camera.ViewportToWorldPoint(Vector2.one);
            Vector3 dimensions = topRight - bottomLeft;

            EdgeCollider2D collider = camera.gameObject.GetComponent<EdgeCollider2D>();
            collider.points = new Vector2[]{
                new Vector2(bottomLeft.x, bottomLeft.y),
                new Vector2(bottomLeft.x, topRight.y),
                new Vector2(topRight.x, topRight.y),
                new Vector2(topRight.x, bottomLeft.y),
                new Vector2(bottomLeft.x, bottomLeft.y)
            };
        }
    }
}