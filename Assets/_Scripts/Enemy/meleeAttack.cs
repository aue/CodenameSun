using UnityEngine;
using System.Collections;

public class meleeAttack : MonoBehaviour {

    bool running;
    Rigidbody myRB;
    Animator myAnim;
    int random;

	// Use this for initialization
	void Start () {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GetComponentInParent<Animator>();
        running = false;
	}

    void OnTriggerEnter(Collider other)
    {
        
        running = myAnim.GetBool("detected");
		if (running && other.tag == "Player")
        {
            myAnim.SetBool("detected", false);
            running = false;
            //if(random == 0)
            myAnim.SetBool("punch", true);
            //else myAnim.SetBool("swipe", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        running = myAnim.GetBool("detected");
		if (!running && other.tag == "Player")
        {
            myAnim.SetBool("detected", true);
            running = true;
            myAnim.SetBool("punch", false);
            //myAnim.SetBool("swipe", false);
        }
    }

    // Update is called once per frame
    void Update () {
        random = Random.Range(0, 2);
    }
}
