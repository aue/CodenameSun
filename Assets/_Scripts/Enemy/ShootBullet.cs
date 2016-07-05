using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {


    public float range = 20f;
    public float damage = 33f;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;

	// Use this for initialization
	void Awake () {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        gunLine.SetPosition(0, transform.position);

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            gunLine.SetPosition(1, shootHit.point);
            if(shootHit.collider.tag == "Enemy")
            {
                enemyHealth theEnemyHealth = shootHit.collider.GetComponent<enemyHealth>();
                if(theEnemyHealth != null)
                {
                    theEnemyHealth.addDamage(damage);
                }
            }
        }
        else gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
