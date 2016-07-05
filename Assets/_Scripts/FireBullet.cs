using UnityEngine;
using System.Collections;

public class FireBullet : MonoBehaviour {
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
		body = transform.root.Find ("Body").gameObject;
		//gunMuzzle = GameObject.Find ("gunMuzzle").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		/*GameObject myPlayer = transform.root.gameObject;
        if(Input.GetAxisRaw("Fire1") > 0 && nextBullet < Time.time)
        {
            nextBullet = Time.time + timeBetweenBullets;
            Vector3 rot;
            //if (myPlayer.GetFacing() == -1f) rot = new Vector3(0, -90, 0);
			rot = myPlayer.transform.position;

            Instantiate(projectile, transform.position, Quaternion.Euler(rot));
        }*/

		if (Input.GetKey (KeyCode.F) && nextBullet < Time.time && characterManagerController.activePlayer().name == "Black Mask") {
			Debug.Log ("Fire");
			nextBullet = Time.time + timeBetweenBullets;


			GameObject myPlayer = transform.root.gameObject;
			//Instantiate(projectile, transform.position, Quaternion.Euler(myPlayer.transform.position));
			Instantiate(projectile, transform.position, body.transform.rotation);
		}
	}
}
