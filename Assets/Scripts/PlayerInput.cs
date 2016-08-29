using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	const int STATE_IDLE = 0;
	const int STATE_WALK = 1;

	AudioSource audio;

	int _currentAnimationState = STATE_IDLE;

	Animator animator;

	private Rigidbody2D _playerController;
	private bool _allowJump = false;

	private bool _facingRight;

	public float movementSpeed = 40.0f;
	public int jumpSpeed = 12;
	public float jumpRecharge = 1.0f;
	private float nextJump = 0.0f;

	public Vector2 startPosition = new Vector2 (-3, -2);

	public bool disableMovement = false;

	void Start () {
		_playerController = GetComponent<Rigidbody2D> ();
		_facingRight = true;
		transform.position = startPosition;
		animator = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
	}
	
	void FixedUpdate () {

		if (!disableMovement) {
			playerMovement();
		}

	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "ground") {
			_allowJump = true;
		}
	}

	private void Flip (float horizontal) {
		if (horizontal > 0 && !_facingRight || horizontal < 0 && _facingRight) {
			_facingRight = !_facingRight;
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}

	private void playerMovement() {
		Vector2 currentVelocity = _playerController.velocity;
		
		float horizontalMovement = Input.GetAxis ("Horizontal");
		float deltaX = horizontalMovement * movementSpeed;
		
		Flip (horizontalMovement);

		if (_playerController.velocity.x != 0) {
			_playerController.velocity = new Vector2 (deltaX, _playerController.velocity.y);
			changeState (STATE_WALK);
		} 
		else {
			transform.Translate (deltaX * Time.deltaTime, 0, 0); //to ensure the player still moves when they get hung up on objects
			_playerController.velocity = new Vector2 (deltaX, _playerController.velocity.y);
			animator.StopPlayback();
			changeState (STATE_IDLE);
		}
		
		
		
		if (Input.GetAxisRaw ("Jump") != 0 && Time.time > nextJump && _allowJump == true) {
			_allowJump = false;
			nextJump = Time.time + jumpRecharge;
			_playerController.AddForce(new Vector2(0, jumpSpeed - currentVelocity.y), ForceMode2D.Impulse);
			audio.pitch = Random.Range(0.9f, 1.0f);
			audio.Play();
		}
	}

	void changeState(int state){
		
		if (_currentAnimationState == state)
			return;
				switch (state) {
			
		case STATE_WALK:
			animator.SetInteger ("state", STATE_WALK);
			break;

		case STATE_IDLE:
			animator.SetInteger ("state", STATE_IDLE);
			break;

		}
		
		_currentAnimationState = state;
	}
}
