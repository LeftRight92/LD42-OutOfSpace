using UnityEngine;
using LD42.Scripts.World;

public class SnapToPixelGrid : MonoBehaviour
{
	[SerializeField] public float step = 0.5f;

	//void Start(){
	//	step = step * GameObject.FindWithTag("WorldManager").GetComponent<WorldManager>().worldScale;
	//}

    void LateUpdate(){      
		transform.localPosition = transform.parent.position.RoundXY(step);      
    }   
}