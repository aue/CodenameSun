using UnityEngine;
using System.Collections;

public class CameraController: MonoBehaviour {
	private GameObject characterManager;
	private CharacterManager characterManagerController;
	public GameObject mainCamera;
	private Vector3 offset;

	[SerializeField] private float positionX = 0;
	[SerializeField] private float positionY = 3;
	[SerializeField] private float positionZ = -6;
	[SerializeField] private float rotate = 15;

	void Start() {
		characterManager = GameObject.Find ("CharacterManager");
		characterManagerController = characterManager.GetComponent<CharacterManager> ();

		offset = new Vector3(positionX, positionY, positionZ);
		mainCamera.transform.rotation = Quaternion.AngleAxis(rotate, Vector3.right);
		mainCamera.transform.position = characterManagerController.activePlayer().transform.position + offset;
	}

	void LateUpdate() {
		//offset = new Vector3(positionX, positionY, positionZ);
		mainCamera.transform.position = characterManagerController.activePlayer().transform.position + offset;
	}
}
