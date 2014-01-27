using UnityEngine;
using System.Collections;

/*
 * A script for managing user interaction with teleportable objects
 */
public class TeleportControl : MonoBehaviour {
	
	public int saveStateCount = 5; 	//the number of save states in the game
	public int activeState = 0;	//the index of the currently selected state
	
	public Vector3 invalidState = new Vector3(1000, 1000, 1000);	//indicates a null location
	
	private bool initialUpdate = true;	//is this the first update call of the level?
	private DrawSaveSlots ssGUI;	//script for drawing the save slots on the screen
	
	
	void Awake(){
		ssGUI = GameObject.FindGameObjectWithTag(Tags.gui).GetComponent<DrawSaveSlots>();
	}
	
	void Update () {
		if (initialUpdate){
			ssGUI.SetActiveSlot(0);
			SaveState();	//save initial state for player convenience
			initialUpdate = false;
		}
		//read input
		handleMouse();
		handleKeyboard();
	}
	
	/*
	 * Creates a new savestate
	 */
	void SaveState(){
		GameObject[] balls = GameObject.FindGameObjectsWithTag(Tags.ball);
		GameObject[] teleportables = GameObject.FindGameObjectsWithTag(Tags.teleportable);
		for (int i = 0; i < balls.Length; i++){
			Teleport tp = balls[i].GetComponent<Teleport>();
			tp.CreateTeleportPoint(activeState);
		}
		for (int i = 0; i < teleportables.Length; i++){
			Teleport tp = teleportables[i].GetComponent<Teleport>();
			tp.CreateTeleportPoint(activeState);
		}
	}
	
	/*
	 * Loads the currently selected savestate
	 */
	void LoadState(){
		GameObject[] balls = GameObject.FindGameObjectsWithTag(Tags.ball);
		GameObject[] teleportables = GameObject.FindGameObjectsWithTag(Tags.teleportable);
		for (int i = 0; i < balls.Length; i++){
			Teleport tp = balls[i].GetComponent<Teleport>();
			tp.TeleportToPoint(activeState, true); //force teleport, because anything that's blocking an object now is going to be gone in a second
		}
		for (int i = 0; i < teleportables.Length; i++){
			Teleport tp = teleportables[i].GetComponent<Teleport>();
			tp.TeleportToPoint(activeState, true);
		}
	}
	
	/*
	 * Process mouse input
	 */
	void handleMouse(){
		//find if the player is pointing at a teleportable object
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)){
			GameObject target = hit.collider.gameObject;
			if (target.layer == Tags.layerTeleportable){
				Teleport tp = target.GetComponent<Teleport>();
				tp.showGhost(activeState);
				
				if (Input.GetButtonDown(InputStrings.teleport)){ //teleport on click
					tp.TeleportToPoint(activeState, false);
				}
			}
		}
	}
	/*
	 * Process keyboard input
	 */
	void handleKeyboard(){
		for (int i = 1; i <= Mathf.Max(saveStateCount, 9); i++){
			//switch save slot
			if (Input.GetKeyDown("" + i)){
				activeState = i-1;
				ssGUI.SetActiveSlot(i-1);
			}
			//save state
            if (Input.GetButtonDown(InputStrings.save)){
				SaveState();
			}
			//load state
            if (Input.GetButtonDown(InputStrings.load)){
				LoadState();
			}
		}
	}
}
