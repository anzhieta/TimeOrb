using UnityEngine;
using System.Collections;

/*
 * Pauses the game when the Pause button is pressed.
 */
public class Pause : MonoBehaviour {

    public bool paused = false; //Is the game paused now?
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown(InputStrings.pause)){
            TogglePause();
        }
	}

    // Toggles pause state
    public void TogglePause() {
        paused = !paused;
        if (paused) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
    }
}
