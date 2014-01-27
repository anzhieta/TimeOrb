using UnityEngine;
using System.Collections;

/*
 * Simple class for making a light oscillate between high and low intensity
 */
public class LightPulse : MonoBehaviour {

	public float pulseSpeed = 0.25f;	//speed of the oscillation
	public float minIntensity = 0f;		//the lowest intensity of the light
	public float maxIntensity = 8f;		//the highest intensity of the light
	public float deadZone = 0.5f;		//The distance from the high/low value where the oscillation direction is reversed
	
	private bool towardsHigh = false;	//current oscillation direction
	
	// Update is called once per frame
	void Update () {
		light.intensity = Mathf.Lerp(light.intensity, (towardsHigh ? maxIntensity : minIntensity), pulseSpeed * Time.deltaTime);
		if (Mathf.Abs(light.intensity - (towardsHigh ? maxIntensity : minIntensity)) < deadZone) towardsHigh = !towardsHigh;
	}
}
