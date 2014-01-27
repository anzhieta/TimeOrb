using UnityEngine;
using System.Collections;

/*
 * Fades the screen in and out.
 */
public class ScreenFader : MonoBehaviour {

    public float fadeSpeed = 1; //The speed of the fade
    public bool fadeIn = true;  //Are we fading in? (If not, we're fading out)

    void Update(){
        if (fadeIn && guiTexture.color.a > 0.01){
            Fade(0);
        }
        if (!fadeIn && guiTexture.color.a < 0.99){
            Fade(1);
        }
    }

    //Fades the fader's opacity towards target value
	void Fade(int target){
        Color c = guiTexture.color;
        c.a = Mathf.Lerp(c.a, target, fadeSpeed * Time.deltaTime);
        guiTexture.color = c;
    }
}
