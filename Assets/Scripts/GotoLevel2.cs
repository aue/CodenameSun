using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GotoLevel2 : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    if(this.transform.position.z < -250)
        {
            SceneManager.LoadScene("Level2");
        }
	}
}
