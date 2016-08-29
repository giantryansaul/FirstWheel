using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	private Vector2 checkpointCoordinates;

	private GameObject gm;
	private GameManager gameManager;

	void Start () {
		checkpointCoordinates = transform.position;
	}


	void OnTriggerEnter2D (Collider2D other) {
		gm = GameObject.Find("GM");
		gameManager = gm.GetComponent<GameManager> ();
		if (other.gameObject.tag == "player") {
			gameManager.currentCheckpoint = checkpointCoordinates;
			gameManager.inStartArea = false;
		}
	}
}
