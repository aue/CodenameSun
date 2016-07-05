using UnityEngine;
using System.Collections;

public class CameraController: MonoBehaviour {
	private GameObject characterManager;
	private CharacterManager characterManagerController;
	public GameObject mainCamera;
	private Vector3 offset;
	private GameObject player;

	[SerializeField] private float positionX = 0;
	[SerializeField] private float positionY = 3;
	[SerializeField] private float positionZ = -6;
	[SerializeField] private float rotate = 15;

	void Start() {
		characterManager = GameObject.Find ("CharacterManager");
		characterManagerController = characterManager.GetComponent<CharacterManager> ();
		player = characterManagerController.activePlayer ();

		offset = new Vector3(positionX, positionY, positionZ);
		mainCamera.transform.rotation = Quaternion.AngleAxis(rotate, Vector3.right);
		if (player) mainCamera.transform.position = player.transform.position + offset;
	}

	void LateUpdate() {
		//offset = new Vector3(positionX, positionY, positionZ);
		player = characterManagerController.activePlayer ();
		mainCamera.transform.position = player.transform.position + offset;
	}
}
