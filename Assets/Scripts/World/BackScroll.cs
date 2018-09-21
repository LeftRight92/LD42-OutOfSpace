using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Utility;

public class BackScroll : MonoBehaviour {
	[SerializeField] public float scrollSpeed = -0.01f;

    private Renderer renderer;
	[SerializeField] private Vector2 offset = Vector2.zero;
	[SerializeField] private Vector2 pos;


	void Start(){
        renderer = gameObject.GetComponent<Renderer>();
	}
	
	void Update(){
        //float offsetY = Mathf.Repeat(offset.y - Time.deltaTime*scrollSpeed, 1);
        //offset = new Vector2(0, offsetY);
		offset = (offset.y - Time.deltaTime * scrollSpeed).Call(Mathf.Repeat, 1f).Call(MethodsOther.NewVec, 0f, true);

        renderer.material.mainTextureOffset = offset;
	}
}
