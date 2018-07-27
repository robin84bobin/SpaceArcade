using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {

    // Use this for initialization
    Vector3 offset;
	void Start () {
        offset = new Vector3(0f, 0f, transform.position.z);
    }
	

    void OnMouseDrag()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = targetPosition;
    }
}
