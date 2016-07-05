using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour {

    public float fullHealth;
    public float currentHealth;
	public bool isDead = false;

    //public GameObject playerDeathFX;

	// Use this for initialization
	void Start () {
        currentHealth = fullHealth;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addDamage(float damage)
    {
		if (currentHealth <= 0) {
			makeDead ();
		} else {
			currentHealth -= damage;
		}
    }

    public void makeDead()
    {
        //Instantiate(playerDeathFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        //Destroy(gameObject);
		isDead = true;
    }
}
