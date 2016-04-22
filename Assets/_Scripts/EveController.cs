using UnityEngine;
using System.Collections;

public class EveController : Pathfinding {

	[System.Serializable]
	public class MoveSetting {
		public float forwardVel = 8;
		public float depthVel = 4;
		public float rotateVel = 250;
		public float jumpVel = 15;
		public float distToGrounded = 0.1f;
		public float distFromPlayer = 3;
		public LayerMask ground;
	}

	[System.Serializable]
	public class PhysSetting {
		public float downAccel = 1f;
	}

	[System.Serializable]
	public class InputSetting {
		public float inputDelay = 0.1f;
		public string FORWARD_AXIS = "Horizontal";
		public string DEPTH_AXIS = "Vertical";
		public string JUMP_AXIS = "Jump";
	}

	public MoveSetting moveSetting = new MoveSetting ();
	public PhysSetting physSetting = new PhysSetting ();
	public InputSetting inputSetting = new InputSetting ();

	Vector3 velocity = Vector3.zero;
	Quaternion targetRotation;
	Rigidbody rBody;
	GameObject body;
	Animator cAnimator;
	float forwardInput, depthInput, jumpInput;

	bool hasPath;
	public bool isActivePlayer;

	public Quaternion TargetRotation {
		get { return targetRotation; }
	}

	bool Grounded() {
		return Physics.Raycast (transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
	}

	void Awake() {
		forwardInput = depthInput = jumpInput = 0;
		hasPath = false;
		isActivePlayer = false;
	}

	private void Start() {
		targetRotation = transform.rotation;

		body = transform.Find ("Body").gameObject;

		if (GetComponent<Rigidbody> ())
			rBody = GetComponent<Rigidbody> ();
		else
			Debug.LogError ("The character needs a rigidbody.");

		if (GetComponent<Animator> ()) {
			cAnimator = GetComponent<Animator> ();
			cAnimator.SetBool ("OnGround", true);
		}
		else
			Debug.LogError ("The character needs an animator.");
	}

	private void GetInput() {
		forwardInput = Input.GetAxis (inputSetting.FORWARD_AXIS); // interpolated
		depthInput = Input.GetAxis (inputSetting.DEPTH_AXIS); // interpolated
		jumpInput = Input.GetAxis (inputSetting.JUMP_AXIS); // non-interpolated
	}

	private void Update() {
		if (isActivePlayer) {
			GetInput ();
			Turn ();
		}
	}

	private void FixedUpdate() {
		if (isActivePlayer) {
			Run ();
			Jump ();

			//Actions for the Character
			// x is death
			if (Input.GetKeyDown ("x")) {
				if (cAnimator.GetInteger ("CurrentAction") == 0) {
					cAnimator.SetInteger ("CurrentAction", 1);
				}
			}
		// something cool
		else if (Input.GetKeyDown ("1")) {
				if (cAnimator.GetInteger ("CurrentAction") == 0) {
					cAnimator.SetInteger ("CurrentAction", 2);
				}
			}
		// 3 && 4 special punches 
		else if (Input.GetKeyDown ("2")) {
				if (cAnimator.GetInteger ("CurrentAction") == 0) {
					cAnimator.SetInteger ("CurrentAction", 3);
				}
			} else if (Input.GetKeyDown ("3")) {
				if (cAnimator.GetInteger ("CurrentAction") == 0) {
					cAnimator.SetInteger ("CurrentAction", 4);
				}
			} else {
				cAnimator.SetInteger ("CurrentAction", 0);
			}

			// crounching
			if (Input.GetKey (KeyCode.LeftControl)) {
				cAnimator.SetBool ("Crouch", true);
			} else {
				cAnimator.SetBool ("Crouch", false);
			}

			rBody.velocity = transform.TransformDirection (velocity);
		}
		else {
			// follow the active player
			GameObject player = GameObject.Find ("Black Mask");
			float distFromPlayer = Vector3.Distance (transform.position, player.transform.position);

			if (!hasPath && distFromPlayer > moveSetting.distFromPlayer) {
				// no active path and too far from the player
				hasPath = true;
				FindPath (transform.position, player.transform.position);
				cAnimator.SetFloat("Forward", 1);
			}
			else {
				// there is a path, keep using it
				if (Path.Count > 0 && distFromPlayer > moveSetting.distFromPlayer/2) {
					// path not yet complete
					float dist = Vector3.Distance (transform.position, Path [0]);
					transform.position = Vector3.MoveTowards (transform.position, Path [0], Time.deltaTime * 7f);

					Vector3 movement = Vector3.RotateTowards(transform.position, Path [0], Time.deltaTime * 100f, 0.0f);
					body.transform.rotation = Quaternion.LookRotation (Path [0] - transform.position);

					if (dist < 0.5) {
						Path.RemoveAt (0);
					}
				}
				else {
					// path complete
					hasPath = false;
					cAnimator.SetFloat("Forward", 0);
				}
			}
		}
	}

	private void Run() {
		if (Mathf.Abs (forwardInput) > inputSetting.inputDelay || Mathf.Abs (depthInput) > inputSetting.inputDelay) {
			cAnimator.SetFloat("Forward", Mathf.Abs (forwardInput) + Mathf.Abs (depthInput));

			if (Mathf.Abs (forwardInput) > inputSetting.inputDelay) {
				// move forward/backward
				velocity.z = moveSetting.forwardVel * forwardInput;
			}
			if (Mathf.Abs (depthInput) > inputSetting.inputDelay) {
				// move in/out
				velocity.x = moveSetting.depthVel * depthInput * -1;
			}
		}
		else {
			// zero velocity
			cAnimator.SetFloat("Forward", 0);
			velocity.x = 0;
			velocity.z = 0;
		}
	}

	private void Turn() {
		if (Mathf.Abs (forwardInput) > inputSetting.inputDelay || Mathf.Abs (depthInput) > inputSetting.inputDelay) {
			Vector3 movement = new Vector3 (forwardInput, 0.0f, depthInput);
			body.transform.rotation = Quaternion.LookRotation (movement);
		}
	}

	private void Jump() {
		if (jumpInput > 0 && Grounded ()) {
			// jump
			cAnimator.SetFloat("Jump", 5);
			cAnimator.SetBool ("OnGround", false);
			velocity.y = moveSetting.jumpVel;
		}
		else if (jumpInput == 0 && Grounded()) {
			// zero out our velocity.y
			cAnimator.SetFloat("Jump", 0);
			cAnimator.SetBool ("OnGround", true);
			velocity.y = 0;
		}
		else {
			// decrease velocity.y
			cAnimator.SetFloat("Jump", -9);
			velocity.y -= physSetting.downAccel;
		}
	}
}
