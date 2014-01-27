using UnityEngine;
using System.Collections;

/*
 * A simple script for resetting and loading levels
 */
public class SceneLoader : MonoBehaviour {
	
	public int currentLevel; //the ID of the current level
	public int nextLevel;	 //the ID of the next level

    private float loadLvTimer = 0.5f;   //Time until the end of the level
    private int loadLvNum = -1; //Number of the next level
    private ScreenFader sf;

	/*
	 * Restarts the current level
	 */
	public void ResetLevel(){
        sf.fadeIn = false;
        loadLvNum = currentLevel;
	}
	
	/*
	 * Starts the next level
	 */
	public void EndLevel(){
        sf.fadeIn = false;
        loadLvNum = nextLevel;
	}
	
    void Awake(){
        sf = GameObject.FindGameObjectWithTag(Tags.screenFader).GetComponent<ScreenFader>();
    }

	void Update(){
		if (Input.GetButtonDown(InputStrings.reset)){
			ResetLevel(); //Reset level when the reset button is pressed
		}
        if (loadLvNum >= 0){
            loadLvTimer -= Time.deltaTime;
            if (loadLvTimer < 0){
                Application.LoadLevel(loadLvNum);
            }
        }
	}
}
