using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Configuration;
using LD42.Scripts.World;

public class ShipControl : MonoBehaviour {
    public float speedHor = 5, speedVert = 5;
    //public GameObject cannonFire;
    //public float cannonCooldown = 0.05f;
    public Weapon cannons;

    private SpriteRenderer renderer;
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private Camera camera;

    //private Vector3 relCannonPos;
    private float cannonCountdown = 0;

    void Start () {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        Vector3 relCannonPos = new Vector3(
            renderer.bounds.size.x * 7.5f/32, 
            renderer.bounds.size.y * 5.5f/32,
            0
        );

        //cannons = Weapon.New(gameObject, WeaponType.CANNON, Resources.Load<GameObject>("Prefabs/CannonFire"), 
                             //new List<Vector3>{relCannonPos, new Vector3(-relCannonPos.x, relCannonPos.y, 0)},
                             //simultaneousFire:false);

	}
	
	void Update () {
        rigidbody.AddForce(new Vector2(
            Input.GetAxis("Horizontal") * speedHor,
            Input.GetAxis("Vertical") * speedVert
        ));

        if(Input.GetButton("Fire1")){
            cannons.AttemptFire();
        }
	}
}
