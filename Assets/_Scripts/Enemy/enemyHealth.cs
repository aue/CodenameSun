using UnityEngine;
using System.Collections;

public class enemyHealth : MonoBehaviour {
	private GameObject characterManager;
	private CharacterManager characterManagerController;

    public float enemyMaxHealth;
    public float damageModifier;
    float currentHealth;
	public bool isDead = false;

	// Use this for initialization
	void Start () {
		characterManager = GameObject.Find ("CharacterManager");
		characterManagerController = characterManager.GetComponent<CharacterManager> ();
        currentHealth = enemyMaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addDamage(float damage)
    {
        damage *= damageModifier;
        if (damage <= 0) return;
        currentHealth -= damage;
        if (currentHealth <= 0) makeDead();
        Debug.Log(currentHealth + " " + damage);
    }

    void makeDead()
    {
        Destroy(gameObject);
		//isDead = true;
		characterManagerController.killed++;
    }
}
