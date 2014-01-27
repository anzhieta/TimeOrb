using UnityEngine;
using System.Collections;

/*
 * Script for keeping multiple teleport locations and transporting and object between them
 */
public class Teleport : MonoBehaviour {
	
	Vector3[] states;	//array of teleport locations
	
	private TeleportControl tpc;	//the TeleportControl script
	private GameObject ghost;		//A "ghost" object that shows the player where the object will be teleported
	private bool ghostVisible = false;	//True if the ghost has been requested to be drawn this frame

	
	void Awake(){
		tpc = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<TeleportControl>();
		states = new Vector3[tpc.saveStateCount];
		for (int i = 0; i < states.Length; i++){
			states[i] = tpc.invalidState;
		}
		
		ghost = transform.Find("Ghost").gameObject;
	}
	
	void LateUpdate(){
		
		//hide the ghost if it doesn't need to be shown
		if (ghostVisible){
			ghostVisible = false;
		}
		else {
			ghost.SetActive(false);
			ghost.GetComponent<GhostOccupied>().Reset();	//reset ghost collisions to 0
		}
	}
	
	/*
	 * Saves the object's current location as a teleport destination
	 * state: the index of the teleport location
	 */
	public void CreateTeleportPoint(int state){
		if (state < 0 || state >= states.Length){
			return;
		}
		states[state] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	/*
	 * Moves the object to a saved teleport location
	 * state: the index of the teleport location
	 * force: whether the teleport should be carried out even if the target area is occupied
	 */
	public void TeleportToPoint(int state, bool force){
		if (state < 0 || state >= states.Length){
			return;
		}
		//To avoid transporter accidents, don't teleport to locations occupied by other objects unless forced
		if (!force && ghost.GetComponent<GhostOccupied>().IsOccupied()){
			return;
		}
		if (states[state] != tpc.invalidState){
			transform.position = states[state];
			//hide ghost so it doesn't teleport with us
			ghostVisible = false;
			ghost.SetActive(false);
			ghost.GetComponent<GhostOccupied>().Reset();
		}
	}
	
	public void showGhost(int state){
		if (states[state] != tpc.invalidState){
			//find the target location of the teleport
			Vector3 ghostPos = states[state];
			ghost.transform.position = ghostPos;
			//show ghost
			ghost.SetActive(true);
			ghostVisible = true;
		}
	}
	
}
