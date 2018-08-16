using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackScroll : MonoBehaviour {
    public float scrollSpeed = -0.01f;

    private Renderer renderer;
    private Vector2 offset = Vector2.zero;
    private Vector2 pos;


	void Start () {
        renderer = gameObject.GetComponent<Renderer>();
	}
	
	void Update () {
        float offsetY = Mathf.Repeat(offset.y - Time.deltaTime*scrollSpeed, 1);
        offset = new Vector2(0, offsetY);
        renderer.material.mainTextureOffset = offset;
	}
}
