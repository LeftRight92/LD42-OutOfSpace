using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Configuration;

public class Weapon : MonoBehaviour {
    [SerializeField] public WeaponType type;
    [SerializeField] public GameObject weaponFire;
	[SerializeField] public float cooldown = 0.5f;
	[SerializeField] public Vector3 location;

	[SerializeField] private bool available = true;
    [SerializeField] private int quantity = 1;
    [SerializeField] private int size = 1;
    [SerializeField] private int damage = 1;
    
	private float prevTime = 0;

	public bool AttemptToFire(GameObject spawner)
    {
        if (Time.time - prevTime >= cooldown)
        {
			Fire(spawner);
            prevTime = Time.time;
			return true;
        }
		else
		{
            return false;
		}
    }

	private void Fire(GameObject spawner){
		GameObject projectile = Instantiate(weaponFire);

		projectile.transform.position =
			          Vector3.Scale(spawner.transform.position, Vector2.one) +
					  spawner.transform.rotation * location;

		projectile.transform.rotation = spawner.transform.rotation; //*
                //Quaternion.AngleAxis(direction, Vector3.back);

		if (spawner == GameObject.FindWithTag("Player"))
        {
            projectile.tag = "PlayerFire";
        }
        else
        {
            projectile.tag = "EnemyFire";
        }

        projectile.transform.parent = gameObject.transform;
	}
}
