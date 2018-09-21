using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Configuration;
using LD42.Scripts.World;

namespace LD42.Scripts.Weapons
{
    public class WeaponController : MonoBehaviour
	{
		[SerializeField] public WeaponType type;
		[SerializeField] public GameObject weaponFire;
		[SerializeField] public float cooldown = 0.5f;
		[SerializeField] public bool simultaneousFire = true;

		[SerializeField] private bool available = true;
		[SerializeField] private int quantity = 1;
		[SerializeField] private int size = 1;
		[SerializeField] private int damage = 1;

        public List<Vector3> fireLocations = new List<Vector3>{Vector3.zero};
              
        private int selectedLoc = 0;
		private float prevTime = 0;

        public void AttemptToFire(GameObject spawner)
        {
            if(Time.time - prevTime >= cooldown)
            {
                if(fireLocations.Count > 1 && simultaneousFire == false){
					PrepToFire(spawner, fireLocations[selectedLoc]);
                    selectedLoc++;
                    selectedLoc %= fireLocations.Count;
                }
                else{
                    for (int i = 0; i < fireLocations.Count; i++){
						PrepToFire(spawner, fireLocations[i]);
                    }
                }
                prevTime = Time.time;
            }
        }

		private void PrepToFire(GameObject spawner, Vector3 location){
			int numProjectiles = quantity;
            if(type == WeaponType.BEAM)
            {
                numProjectiles = 1;
            }

			float direction = 0;
			if (type == WeaponType.BOMB)
			{
				direction =
                    Vector2.SignedAngle(Vector2.right, spawner.GetComponent<Rigidbody2D>().velocity) -
                           spawner.transform.rotation.eulerAngles.z;
                direction = Mathf.Repeat(direction, 360);
			}

			if(numProjectiles == 1){
				Fire(spawner, location, direction);
			}
			else{
				if(type == WeaponType.CANNON){
					int turnwise = location.x >= 0 ? 1 : -1;
                    for (int i = 0; i < numProjectiles; i++)
                    {
                        Fire(spawner, location, i*36*turnwise);
                    }
				}
				else{
					for(int i = 0; i < numProjectiles; i++){
                        Fire(spawner, location, direction);
					}
				}
			}
		}

        private void Fire(GameObject spawner, Vector3 location, float direction)
        {
			GameObject projectile = Instantiate(weaponFire);         

            Vector3 spawnerBounds = 
				spawner.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size;

            float firedOffset = 0;         
			if(type == WeaponType.BEAM || type == WeaponType.CANNON){
				firedOffset = 
					projectile.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.extents.y;
			}

            projectile.transform.position =
                Vector3.Scale(spawner.transform.position, Vector2.one) +
                spawner.transform.rotation * location +
                //Vector3.Scale(location, spawnerBounds) +
                new Vector3(0, firedOffset, 0);

			projectile.transform.rotation = spawner.transform.rotation *
				Quaternion.AngleAxis(direction, Vector3.back);

            if (spawner == GameObject.FindWithTag("Player"))
            {
                projectile.tag = "PlayerFire";
            }
            else
            {
                projectile.tag = "EnemyFire";
            }

            //Physics2D.IgnoreCollision(spawner.GetComponent<BoxCollider2D>(), 
			                          //projectile.GetComponent<BoxCollider2D>());
            projectile.transform.parent = gameObject.transform;
        }
    }
}