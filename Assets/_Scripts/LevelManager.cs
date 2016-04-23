using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayLevel3() {
		if (SceneManager.GetActiveScene ().buildIndex == 3) {
			SceneManager.LoadScene (0);
			Time.timeScale = 1;
		} else {
			SceneManager.LoadScene (3);
			Time.timeScale = 1;
		}
	}

	public void PlayMenu() {
		SceneManager.LoadScene (0);
		Time.timeScale = 1;
	}

	public void Restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		Time.timeScale = 1;
	}

	void Next() {
		//SceneManager.LoadScene (SceneManager.GetActiveScene ());
		Time.timeScale = 1;
	}
}
