using UnityEngine;
using System.Collections;

/*
 * A class that determines if the ghost of a teleportable object is blocked by another object, i.e. if the parent object can be teleported without transporter accidents
 */
public class GhostOccupied : MonoBehaviour {

	private int occupying = 0;	//number of blocking objects
	
	/*
	 * Returns whether the ghost is blocked
	 */
	public bool IsOccupied(){

		return occupying != 0;
	}
	
	/*
	 * Sets the ghost to an unblocked state. Must always be called before disabling the ghost because OnTriggerExit won't be called on disabled objects.
	 */
	public void Reset(){
		occupying = 0;
	}
	
	void OnTriggerEnter(Collider other){
		if (blocking(other))
			occupying++;
	}
	
	void OnTriggerExit(Collider other){
		if (blocking(other))
			occupying--;
	}

    bool blocking(Collider other){
        return (other.gameObject.layer == Tags.layerTeleportable || other.gameObject.tag == Tags.teleportable) && other.gameObject.layer != Tags.layerBlockingIgnore;
    }
}
