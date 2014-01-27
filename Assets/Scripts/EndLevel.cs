using UnityEngine;
using System.Collections;

/*
 * Triggers end of level when trigger is entered
 */
public class EndLevel : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == Tags.ball){
			GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<SceneLoader>().EndLevel();
		}
	}
}
