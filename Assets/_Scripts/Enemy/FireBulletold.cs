using UnityEngine;
using System.Collections;

public class FireBulletold : MonoBehaviour {
	private GameObject characterManager;
	private CharacterManager characterManagerController;

    public float timeBetweenBullets = 0.15f;
    public GameObject projectile;
	private GameObject gunMuzzle;
    float nextBullet;
	GameObject body;

	// Use this for initialization
	void Awake () {
		nextBullet = 0f;
	}

	void Start() {
		characterManager = GameObject.Find ("CharacterManager");
		characterManagerController = characterManager.GetComponent<CharacterManager> ();
		body = transform.Find ("Body").gameObject;
		gunMuzzle = GameObject.Find ("gunMuzzle").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.F) && nextBullet < Time.time && characterManagerController.activePlayer().name == "Black Mask") {
			Debug.Log ("Fire");
            nextBullet = Time.time + timeBetweenBullets;

			Instantiate(projectile, gunMuzzle.transform.position, body.transform.rotation);
        }
	}
}
