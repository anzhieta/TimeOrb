using UnityEngine;
using System.Collections;

/*
 * Automatically rotates an object according to a vector.
 */
public class Rotate : MonoBehaviour {

    public Vector3 rotation = Vector3.zero; //Vector containing the rotation speeds of each axis.
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotation * Time.deltaTime);
	}
}
