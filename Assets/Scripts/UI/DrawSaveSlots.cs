using UnityEngine;
using System.Collections;

/*
 * Script for drawing save slot numbers on the GUI
 */
public class DrawSaveSlots : MonoBehaviour {
	
	public GUIText slotTemplate;	//The GUIText template that defines e.g. the font and default color of the save slot text
	public Color selectedColor;		//The color of the currently selected slot
	private GUIText[] slotTexts;	//The array of save slots in the GUI
	private Color defaultColor;		//the color of unselected slots
	
	void Awake () {
		defaultColor = slotTemplate.color;
		TeleportControl tpc = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<TeleportControl>();
		slotTexts = new GUIText[tpc.saveStateCount];
		for (int i = 0; i < slotTexts.Length; i++){
			slotTexts[i] = GameObject.Instantiate(slotTemplate) as GUIText;
			slotTexts[i].text = "" + (i+1); //write slot number
			slotTexts[i].pixelOffset = new Vector2(slotTemplate.pixelOffset.x + i * slotTemplate.fontSize, slotTemplate.pixelOffset.y); //position the slot according to font size
		}
	}
	
	/*
	 * Changes which save slot is shown as active. To actually change the active slot, see TeleportControl
	 */
	public void SetActiveSlot(int slot){
		if (slot < 0 || slot >= slotTexts.Length){
			return;
		}
		for (int i = 0; i < slotTexts.Length; i++){
			slotTexts[i].color = defaultColor;
		}
		slotTexts[slot].color = selectedColor;
	}
}
