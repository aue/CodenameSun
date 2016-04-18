using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartLevel1 : MonoBehaviour {

    public GameObject jack; 
    public GameObject stone;
    public GameObject door; 
    public Button startButton;
    public Canvas story;
    public float speed;

    // Use this for initialization
    public void Start () {
        jack = GameObject.Find("Jack");
        stone = GameObject.Find("Stone");
        door = GameObject.Find("Door");
        jack.SetActive(false);
        stone.SetActive(false);
        startButton = startButton.GetComponent<Button>();
        story = story.GetComponent<Canvas>();
        story.enabled = true;
    }
	
	public void startScene()
    {
        story.enabled = false;
        jack.SetActive(true);
        stone.SetActive(true);
        //Update();
    }

    /*void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        //move towards the center of the world (or where ever you like)
        Vector3 targetPosition = new Vector3(0, 500, -250);

        Vector3 currentPosition = door.transform.position;
        //first, check to see if we're close enough to the target
        if (Vector3.Distance(currentPosition, targetPosition) > .1f)
        {
            Vector3 directionOfTravel = targetPosition - currentPosition;
            //now normalize the direction, since we only want the direction information
            directionOfTravel.Normalize();
            //scale the movement on each axis by the directionOfTravel vector components

            door.transform.Translate(
                (directionOfTravel.x * speed * Time.deltaTime),
                (directionOfTravel.y * speed * Time.deltaTime),
                (directionOfTravel.z * speed * Time.deltaTime),
                Space.World);
        }
    }*/
}
