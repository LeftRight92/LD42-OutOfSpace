using UnityEngine;

public class SnapToPixelGrid : MonoBehaviour
{
    [SerializeField] public int pixelsPerUnit = 270;

    private void Start()
    {
		
    }

    private void LateUpdate()
    {
        Vector3 newLocalPosition = Vector3.zero;

		newLocalPosition.x = (Mathf.Round(transform.parent.position.x * pixelsPerUnit) / pixelsPerUnit) - transform.parent.position.x;
		newLocalPosition.y = (Mathf.Round(transform.parent.position.y * pixelsPerUnit) / pixelsPerUnit) - transform.parent.position.y;

        transform.localPosition = newLocalPosition;
    }
}