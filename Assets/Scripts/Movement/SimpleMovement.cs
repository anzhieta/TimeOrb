using UnityEngine;
using System.Collections;

/*
 * A script for moving objects in a straight line until they're blocked by other objects
 */
public class SimpleMovement : MonoBehaviour{

	public float speed = 2f;	//the speed of the movement
	public Vector3 direction = Vector3.zero;	//the direction of the movement
    public float epsilon = 0.05f; //error margin for preventing the object from clipping into others in extreme corner cases

	void FixedUpdate ()	{
		RaycastHit hit;
		//Check to see if the movement is blocked by an object
		if (!rigidbody.SweepTest (direction, out hit, speed * direction.magnitude * Time.deltaTime + epsilon)) {
			//move forward
			rigidbody.MovePosition (rigidbody.position + direction * speed * Time.deltaTime);
		} else {
			//stop the object before it collides; this prevents the built-in physics from taking over
            rigidbody.MovePosition (rigidbody.position + direction * (hit.distance - epsilon));
		}
	}
}
