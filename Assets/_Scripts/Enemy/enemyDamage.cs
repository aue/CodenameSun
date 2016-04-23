using UnityEngine;
using System.Collections;

public class enemyDamage : MonoBehaviour {
	private GameObject BlackMask;
	private GameObject Eve;

    public float damage;
    public float damageRate;
    public float pushBackForce;
    float nextDamage;

    bool playerInRange = false;
    GameObject thePlayer;
    playerHealth thePlayerHealth;

	bool blackMaskInRange = false;
	bool eveInRange = false;

	// Use this for initialization
	void Start () {
		BlackMask = GameObject.Find ("Black Mask");
		Eve = GameObject.Find ("Eve");

        nextDamage = Time.time;
        //thePlayer = GameObject.FindGameObjectWithTag("Player");
        //thePlayerHealth = thePlayer.GetComponent<playerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		if (blackMaskInRange || eveInRange) Attack();
	}

    void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
            //playerInRange = true;

			if (other.gameObject.name == "Black Mask")
				blackMaskInRange = true;
			else if (other.gameObject.name == "Eve")
				eveInRange = true;
        }
    }

    void OnTriggerExit(Collider other) {
		if(other.tag == "Player") {
            //playerInRange = false;

			if (other.gameObject.name == "Black Mask")
				blackMaskInRange = false;
			else if (other.gameObject.name == "Eve")
				eveInRange = false;
        }
    }

    void Attack() {
        if (nextDamage <= Time.time) {
			if (blackMaskInRange) {
				BlackMask.GetComponent<playerHealth>().addDamage(damage);
				nextDamage = Time.time + damageRate;
				pushBack(BlackMask.transform);
			}

			if (eveInRange) {
				Eve.GetComponent<playerHealth>().addDamage(damage);
				nextDamage = Time.time + damageRate;
				pushBack(Eve.transform);
			}
        }
    }

    void pushBack(Transform pushedObject)
    {
        Vector3 pushDirection = new Vector3(0, (pushedObject.position.y - transform.position.y), 0).normalized;
        pushDirection *= pushBackForce;
        Rigidbody pushedRB = pushedObject.GetComponent<Rigidbody>();

        pushedRB.velocity = Vector3.zero;
        pushedRB.AddForce(pushDirection, ForceMode.Impulse);
    }
}
