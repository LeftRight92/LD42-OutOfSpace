using UnityEngine;
using LD42.Scripts.World;

public class SnapToPixelGrid : MonoBehaviour
{
    [SerializeField] public float pixelsPerUnit = 0.5f;

    void LateUpdate()
    {      
        transform.localPosition = transform.parent.position.RoundXY(pixelsPerUnit);
    }   
}