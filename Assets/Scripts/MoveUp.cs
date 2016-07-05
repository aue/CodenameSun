using UnityEngine;
using System.Collections;

public class MoveUp : MonoBehaviour {
    
    public float speed;
    public Canvas story;
    // Use this for initialization
    void Start () {
        story = story.GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        if(story.isActiveAndEnabled == false)
            MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        //move towards the center of the world (or where ever you like)
        Vector3 targetPosition = new Vector3(0, 500, -250);

        Vector3 currentPosition = this.transform.position;
        //first, check to see if we're close enough to the target
        if (Vector3.Distance(currentPosition, targetPosition) > .1f)
        {
            Vector3 directionOfTravel = targetPosition - currentPosition;
            //now normalize the direction, since we only want the direction information
            directionOfTravel.Normalize();
            //scale the movement on each axis by the directionOfTravel vector components

            this.transform.Translate(
                (directionOfTravel.x * speed * Time.deltaTime),
                (directionOfTravel.y * speed * Time.deltaTime),
                (directionOfTravel.z * speed * Time.deltaTime),
                Space.World);
        }
    }
}
