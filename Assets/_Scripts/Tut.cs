using UnityEngine;
using System.Collections;

public class Tut : MonoBehaviour {

	public GameObject step;

	// Use this for initialization
	void Start () {
		//step = GameObject.Find("Step1");
		step.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			step.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			step.SetActive(false);
		}
	}
}
