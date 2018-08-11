using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour {
    public float speedHor = 0.5f, speedVert = 1;
    public GameObject cannonFire;
    public float cannonCooldown = 0.05f;

    private SpriteRenderer renderer;
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private Camera camera;

    private Vector3 relCannonPos;
    private float cannonCountdown = 0;

    void Start () {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        relCannonPos = new Vector3(
            renderer.bounds.size.x * 7.5f/32, 
            renderer.bounds.size.y * 5.5f/32,
            0
        );
	}
	
	void Update () {
        rigidbody.AddForce(new Vector2(
            Input.GetAxis("Horizontal") * speedHor,
            Input.GetAxis("Vertical") * speedVert
        ));

        if(cannonCountdown > 0){
            cannonCountdown -= Time.deltaTime;
        }
        if(Input.GetButton("Fire1") && cannonCountdown<=0){
            GameObject fired = Instantiate(cannonFire);
            Physics2D.IgnoreCollision(collider, fired.GetComponent<BoxCollider2D>());
            float firedOffset = fired.GetComponent<SpriteRenderer>().bounds.extents.y;
            fired.transform.position = gameObject.transform.position + relCannonPos + 
                new Vector3(0, firedOffset, 0); 
                
            cannonCountdown = cannonCooldown;
        }
	}
}
