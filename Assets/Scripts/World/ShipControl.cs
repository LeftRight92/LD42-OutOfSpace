using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Utility;
using LD42.Scripts.Configuration;
using LD42.Scripts.Weapons;
using LD42.Scripts.World;

public class ShipControl : MonoBehaviour {    
	[SerializeField] public float speedHor = 5, speedVert = 5;
	//[SerializeField] public WeaponController cannonPrefab, beamPrefab, missilePrefab, bombPrefab;
	[SerializeField] public Sprite[] engineTails = new Sprite[4];
   
	//[SerializeField] private WeaponController cannon, beam, missile, bomb;   
    private SpriteRenderer renderer;
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
	//private Camera mainCamera;

	private WeaponsControl weapons;

	void Start () {
		renderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<BoxCollider2D>();

		weapons = gameObject.GetComponent<WeaponsControl>();

		WorldManager worldManager = GameObject.FindWithTag("WorldManager").GetComponent<WorldManager>();
		worldManager.update += _Update;
	}

	void _Update(){
        int i = Mathf.FloorToInt(Random.value * 4);
        gameObject.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite =
			          engineTails[i];
	}
	
	void FixedUpdate(){      
        rigidbody.AddForce(new Vector2(
            Input.GetAxis("Horizontal") * speedHor,
            Input.GetAxis("Vertical") * speedVert
        ));

        if(Input.GetButton("Fire1")){
			weapons.AttempToFire(WeaponType.CANNON);
            weapons.AttempToFire(WeaponType.BEAM);
        }
	}
}
