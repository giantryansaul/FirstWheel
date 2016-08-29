using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	[SerializeField] private GameObject player;
	[SerializeField] private GameObject wheel;

	public Vector2 checkpoint;

	void OnCollisionEnter2D (Collision2D other) {
		GameObject gm = GameObject.Find("GM");
		GameManager gameManager = gm.GetComponent<GameManager> ();
		checkpoint = gameManager.currentCheckpoint;

		ResetPlayerAndWheel (checkpoint, checkpoint);
	}

	public void ResetPlayerAndWheel(Vector2 playerPosition, Vector2 wheelPosition) {
		ResetObject (player, playerPosition);
		ResetObject (wheel, wheelPosition);
	}

	void ResetObject (GameObject obj, Vector2 position) {
		obj.transform.position = position;
		obj.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
	}
}
