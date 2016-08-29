using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Vector2 currentCheckpoint = new Vector2 (0, 0);

	public GUISkin guiSkin;

	private GameObject startCheckpoint;

	public bool inStartArea = true;

	void OnGUI() {
		GUI.skin = guiSkin;

		if (inStartArea) {
			GUI.Label( new Rect (Screen.width / 2 - 300, Screen.height / 2 - 200, 600, 200), "You've created the first wheel!\nBring the wheel back to your village!\nA/D or Left/Right to move back/forth and Space to jump\nReturn/Mouse1 to push the wheel forward\nShift/Mouse2 to shove the wheel up");
		}
	}
	
}
