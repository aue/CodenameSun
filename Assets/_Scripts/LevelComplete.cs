using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour {

	public GameObject button;

	// Use this for initialization
	void Start () {
		button.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			Time.timeScale = 0;
			button.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			Time.timeScale = 1;
			button.SetActive(false);
		}
	}

}
