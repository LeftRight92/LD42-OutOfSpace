using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD42.Scripts.Configuration;

namespace LD42.Scripts.World
{
    [System.Serializable]
    public class Weapon : MonoBehaviour
    {
		public WeaponType type;
        public GameObject weaponFire;
        public float cooldown = 0.05f;
        public bool simultaneousFire = true;

        public List<Vector3> fireLocations = new List<Vector3>{Vector3.zero};


        private int selectedLoc = 0;
        private float timer = 0;

        public static Weapon New(GameObject parent, WeaponType type, GameObject weaponFire, List<Vector3> fireLocations, 
                                 float cooldown = 0.05f, bool simultaneousFire = true)
        {
            Weapon weapon = parent.AddComponent<Weapon>();

            weapon.type = type;
            weapon.weaponFire = weaponFire;
            weapon.fireLocations = fireLocations;
            weapon.cooldown = cooldown;
            weapon.simultaneousFire = simultaneousFire;

            return weapon;
        }

        public void AttemptFire()
        {
            if(Time.time - timer >= cooldown)
            {
                if(fireLocations.Count > 1 && simultaneousFire == false){
                    Fire(selectedLoc);
                    selectedLoc++;
                    selectedLoc %= fireLocations.Count;
                }
                else{
                    for (int i = 0; i < fireLocations.Count - 1; i++){
                        Fire(i);
                    }
                }
                timer = Time.time;
            }
        }

        private void Fire(int locIndex)
        {
            GameObject fired = Instantiate(weaponFire);

            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), fired.GetComponent<BoxCollider2D>());

            float firedOffset = fired.GetComponent<SpriteRenderer>().bounds.extents.y;
            fired.transform.position = gameObject.transform.position +
                gameObject.transform.rotation * fireLocations[locIndex] + 
                new Vector3(0, firedOffset, 0);
        }
    }
}