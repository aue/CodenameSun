using UnityEngine;
using System.Collections;

public class RobotController : Pathfinding {
	private GameObject characterManager;
	private CharacterManager characterManagerController;

	[System.Serializable]
	public class MoveSetting {
		public float forwardVel = 8;
		public float depthVel = 4;
		public float rotateVel = 250;
		public float jumpVel = 15;
		public float distToGrounded = 0.1f;
		public float distFromPlayer = 5;
		public LayerMask ground;
	}

	[System.Serializable]
	public class PhysSetting {
		public float downAccel = 1f;
	}

	[System.Serializable]
	public class InputSetting {
		public float inputDelay = 0.1f;
	}

	public MoveSetting moveSetting = new MoveSetting ();
	public PhysSetting physSetting = new PhysSetting ();
	public InputSetting inputSetting = new InputSetting ();

	Rigidbody rBody;
	GameObject body;
	Animator cAnimator;
	Vector3 originalPosition;

	bool hasPath, isPunching;

	void Awake() {
		hasPath = isPunching = false;
	}

	private void Start() {
		characterManager = GameObject.Find ("CharacterManager");
		characterManagerController = characterManager.GetComponent<CharacterManager> ();
		body = transform.Find ("Body").gameObject;
		originalPosition = transform.position;

		if (GetComponent<Rigidbody> ())
			rBody = GetComponent<Rigidbody> ();
		else
			Debug.LogError ("The character needs a rigidbody.");

		if (GetComponent<Animator> ())
			cAnimator = GetComponent<Animator>();
		else
			Debug.LogError ("The character needs an animator.");
	}

	private void Update() {
		
	}

	private void FixedUpdate() {
		// follow the active player
		GameObject player = characterManagerController.activePlayer();
		float distFromPlayer = Vector3.Distance (transform.position, player.transform.position);

		if (!hasPath && distFromPlayer < moveSetting.distFromPlayer && distFromPlayer > moveSetting.distFromPlayer/2) {
			// no active path and not close enough to the player
			hasPath = true;
			FindPath (transform.position, player.transform.position);
			cAnimator.SetFloat("VSpeed", 1);
		}
		else {
			// there is a path, keep using it
			if (Path.Count > 0 && distFromPlayer < moveSetting.distFromPlayer/2) {
				// path complete
				hasPath = false;
				cAnimator.SetFloat("VSpeed", 0);
			}
			else if (Path.Count > 0 && distFromPlayer < moveSetting.distFromPlayer) {
				// path not yet complete
				float dist = Vector3.Distance (transform.position, Path [0]);
				transform.position = Vector3.MoveTowards (transform.position, Path [0], Time.deltaTime * 5f);

				body.transform.rotation = Quaternion.LookRotation (Path [0] - transform.position);

				if (dist < 0.5) {
					Path.RemoveAt (0);
				}
			}
			else {
				// path complete
				hasPath = false;
				cAnimator.SetFloat("VSpeed", 0);
			}
		}

		// punch
		if (!isPunching && distFromPlayer < moveSetting.distFromPlayer / 2) {
			isPunching = true;
			cAnimator.SetInteger ("CurrentAction", 3);
		} else if (isPunching && distFromPlayer > moveSetting.distFromPlayer / 2) {
			isPunching = false;
			cAnimator.SetInteger ("CurrentAction", 0);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			//step.SetActive(true);
		}
	}
}
