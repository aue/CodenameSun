using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {
	private GameObject BlackMask;
	private GameObject Eve;

	private BlackMaskController blackMaskController;
	private EveController eveController;

	void Start () {
		BlackMask = GameObject.Find ("Black Mask");
		Eve = GameObject.Find ("Eve");

		blackMaskController = BlackMask.GetComponent<BlackMaskController> ();
		eveController = Eve.GetComponent<EveController> ();

		// set black mask as active
		blackMaskController.isActivePlayer = true;
		eveController.isActivePlayer = false;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			blackMaskController.isActivePlayer = !blackMaskController.isActivePlayer;
			eveController.isActivePlayer = !eveController.isActivePlayer;
		}
	}

	public GameObject activePlayer() {
		if (blackMaskController.isActivePlayer)
			return BlackMask;
		else if (eveController.isActivePlayer)
			return Eve;
		else
			return null;
	}
}
