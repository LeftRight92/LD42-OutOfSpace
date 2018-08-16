using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Configuration;
using LD42.Scripts.Weapons;

public class ShipControl : MonoBehaviour{
	public float speedHor = 5, speedVert = 5;
	public WeaponController cannonPrefab, beamPrefab, missilePrefab, bombPrefab;
   
	[SerializeField] private WeaponController cannon, beam, missile, bomb;   
    private SpriteRenderer renderer;
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private Camera camera;

    void Start () {
		Physics2D.IgnoreLayerCollision(8, 8);

        renderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

		cannon = Instantiate(cannonPrefab, transform.position, Quaternion.identity);
		cannon.transform.parent = transform;

		beam = Instantiate(beamPrefab, transform.position, Quaternion.identity);
		beam.transform.parent = transform;

		missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
		missile.transform.parent = transform;

        bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        bomb.transform.parent = transform;
	}
	
	void Update () {
		int i = Mathf.RoundToInt(Random.value * 4);
		gameObject.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite =
			          Resources.Load<Sprite>("/Sprites/0.2/ShiftRunnerEngine" + i + ".png");

        rigidbody.AddForce(new Vector2(
            Input.GetAxis("Horizontal") * speedHor,
            Input.GetAxis("Vertical") * speedVert
        ));

        if(Input.GetButton("Fire1")){
			cannon.AttemptToFire(gameObject);
			beam.AttemptToFire(gameObject);
            missile.AttemptToFire(gameObject);
            bomb.AttemptToFire(gameObject);
        }
	}
}
