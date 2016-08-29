using UnityEngine;
using System.Collections;

public class Village : MonoBehaviour {

	AudioSource audio;
	public GUISkin guiSkin;

	private GameObject gm;
	private GameManager gameManager;

	private GameObject camera;
	private CameraFollow cameraFollow;

	private GameObject floor;
	private Floor floorControls;

	private GameObject wheel;
	private Wheel wheelControls;

	private bool isGameOver = false;

	[SerializeField] private GameObject player;
	private PlayerInput playerInput;

	void Start() {
		wheel = GameObject.Find ("wheel");
		wheelControls = wheel.GetComponent<Wheel> ();
		playerInput = player.GetComponent<PlayerInput> ();

		camera = GameObject.Find ("Camera");
		cameraFollow = camera.GetComponent<CameraFollow> ();

		audio = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "wheel") {
			GameOver();
			audio.Play();
		}
	}

	void GameOver() {
		isGameOver = true;

		cameraFollow.target = wheel;

		playerInput.disableMovement = true;

	}

	void OnGUI() {
		GUI.skin = guiSkin;

		if (isGameOver) {
			GUI.Label( new Rect (Screen.width / 2 - 200, Screen.height / 2, 400, 100), "You've brought the first wheel to your village!\nCivilization will remember you forever!");

			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 100, 100, 40), "Start again?")) {
				Restart();
			}
		}
	}

	void Restart() {
		cameraFollow.target = player;

		floor = GameObject.Find ("DeathFloor");
		floorControls = floor.GetComponent<Floor> ();

		floorControls.ResetPlayerAndWheel (playerInput.startPosition, wheelControls.startPosition);

		isGameOver = !isGameOver;
	}
}