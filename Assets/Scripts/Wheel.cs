using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour {

	AudioSource audio;

	public float intersectionDistance = 2.0f;

	public float pushForce = 20.0f;
	public float pushRecharge = 0.1f;
	private float nextPush = 0.0f;

	public float shoveForce = 200.0f;
	public float shoveRecharge = 0.7f;
	private float nextShove = 0.0f;

	public Vector2 startPosition = new Vector2 (14, -3);

	private Rigidbody2D _wheel;

	[SerializeField] private GameObject player;
	private PlayerInput playerInput;
	
	void Start () {
		_wheel = GetComponent<Rigidbody2D> ();
		transform.position = startPosition;
		audio = GetComponent<AudioSource> ();
	}

	void Update () {
		float distance = Vector2.Distance (player.transform.position, transform.position);
		PlayerInput playerInput = player.GetComponent<PlayerInput> ();
		Rigidbody2D playerBody = player.GetComponent<Rigidbody2D> ();
		float playerDirection;
		if (playerBody.velocity.x < 0) {
			playerDirection = -1;
		} else {
			playerDirection = 1;
		}

		if (distance <= intersectionDistance) {
			playerInput.movementSpeed = 4.0f;
			if (Input.GetAxisRaw ("Fire1") != 0 && Time.time > nextPush) {
				nextPush = Time.time + pushRecharge;
				float push = playerBody.velocity.x * pushForce;
				_wheel.AddForce (new Vector2 (push, 0), ForceMode2D.Impulse);
				audio.pitch = Random.Range(1.0f, 1.2f);
				audio.Play();
			} else if (Input.GetAxisRaw ("Fire2") != 0 && Time.time > nextShove) {
				nextShove = Time.time + shoveRecharge;
				_wheel.AddForce (new Vector2 (playerDirection * shoveForce / 3, shoveForce), ForceMode2D.Impulse);
				audio.pitch = Random.Range(0.5f, 0.8f);
				audio.Play();
			}
		} else {
			playerInput.movementSpeed = 10.0f;
		}
	}
}