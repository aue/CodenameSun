using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Button startButton;
    public Button exitButton;

	// Use this for initialization
	void Start () {
        startButton = startButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
    }
	
	public void ExitButton()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }

}
