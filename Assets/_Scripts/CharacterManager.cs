using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterManager : MonoBehaviour {
	public GameObject gameovermsg;
	public GameObject gameoverbtn;

	public GameObject healthStat;
	public GameObject killStat;

	private GameObject BlackMask;
	private GameObject Eve;

	private BlackMaskController blackMaskController;
	private EveController eveController;

	private playerHealth blackMaskHealth;
	private playerHealth eveHealth;

	public int killed;

	void Start () {
		BlackMask = GameObject.Find ("Black Mask");
		Eve = GameObject.Find ("Eve");

		blackMaskController = BlackMask.GetComponent<BlackMaskController> ();
		eveController = Eve.GetComponent<EveController> ();

		blackMaskHealth = BlackMask.GetComponent<playerHealth> ();
		eveHealth = Eve.GetComponent<playerHealth> ();

		// set black mask as active
		blackMaskController.isActivePlayer = true;
		eveController.isActivePlayer = false;

		killed = 0;
	}

	void Update () {
		healthStat.GetComponent<Text> ().text = blackMaskHealth.currentHealth + "/" + blackMaskHealth.fullHealth + " Black Mask\n" + eveHealth.currentHealth + "/" + eveHealth.fullHealth + " Eve";
		killStat.GetComponent<Text> ().text = "Killed " + killed;

		if (blackMaskHealth.isDead && eveHealth.isDead) {
			Time.timeScale = 0;
			gameovermsg.SetActive (true);
			gameoverbtn.SetActive (true);
		}
		else if (blackMaskHealth.isDead && !eveController.isActivePlayer) {
			blackMaskController.isActivePlayer = false;
			eveController.isActivePlayer = true;
		}
		else if (eveHealth.isDead && !blackMaskController.isActivePlayer) {
			blackMaskController.isActivePlayer = true;
			eveController.isActivePlayer = false;
		}

		if (Input.GetKeyDown (KeyCode.E) && !blackMaskHealth.isDead && !eveHealth.isDead) {
			blackMaskController.isActivePlayer = !blackMaskController.isActivePlayer;
			eveController.isActivePlayer = !eveController.isActivePlayer;
		}
	}

	public GameObject activePlayer() {
		if (blackMaskController && blackMaskController.isActivePlayer)
			return BlackMask;
		else if (eveController && eveController.isActivePlayer)
			return Eve;
		else
			return BlackMask;
	}
}
