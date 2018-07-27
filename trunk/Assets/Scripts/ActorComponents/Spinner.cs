using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

    public float xSpeed;
    public float ySpeed = -2f;
    public float zSpeed;

    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(xSpeed, ySpeed, zSpeed));
	}
}
