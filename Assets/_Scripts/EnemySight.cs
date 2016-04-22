using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 150f;           // Number of degrees, centred on forward, for the enemy see.
    public bool playerInSight;                      // Whether or not the player is currently sighted.
    public Vector3 personalLastSighting;            // Last place this enemy spotted the player.


    private NavMeshAgent nav;                       // Reference to the NavMeshAgent component.
    private SphereCollider col;                     // Reference to the sphere collider trigger component.
    private Animator anim;                          // Reference to the Animator.
    private LastPlayerSighting lastPlayerSighting;  // Reference to last global sighting of the player.
    private GameObject player;                      // Reference to the player.
    //private Animator playerAnim;                    // Reference to the player's animator component.
    //private PlayerHealth playerHealth;              // Reference to the player's health script.
    //private HashIDs hash;                           // Reference to the HashIDs.
    private Vector3 previousSighting;               // Where the player was sighted last frame.


    void Awake()
    {
        // Setting up the references.
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        //playerAnim = player.GetComponent<Animator>();
        //playerHealth = player.GetComponent<PlayerHealth>();
        //hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        // Set the personal sighting and the previous sighting to the reset position.
        personalLastSighting = lastPlayerSighting.resetPosition;
        previousSighting = lastPlayerSighting.resetPosition;
    }


    void Update()
    {
        // If the last global sighting of the player has changed...
        if (lastPlayerSighting.position != previousSighting)
        {
            // ... then update the personal sighting to be the same as the global sighting.
            personalLastSighting = lastPlayerSighting.position;

            Debug.Log("Update last sighting to " + personalLastSighting);
        }

        // Set the previous sighting to the be the sighting from this frame.
        previousSighting = lastPlayerSighting.position;

        // If the player is alive...
        //if (playerHealth.health > 0f)
        // ... set the animator parameter to whether the player is in sight or not.
        //anim.SetBool(hash.playerInSightBool, playerInSight);
        //else
        // ... set the animator parameter to false.
        //anim.SetBool(hash.playerInSightBool, false);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Stone")
        {
            Destroy(col.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        // If the player has entered the trigger sphere...
        if (other.gameObject == player)
        {
            Debug.Log("Player in sphere");
            // By default the player is not in sight.
            playerInSight = false;

            Vector3 correctedPosition = transform.position;
            correctedPosition.y = 0;

            float scaledRadius = 60 * col.radius;

            // Create a vector from the enemy to the player and store the angle between it and forward.
            Vector3 direction = player.transform.localPosition - correctedPosition;
            float angle = Vector3.Angle(direction, transform.forward);
            Debug.Log("ANGLE VIEW " + (angle < fieldOfViewAngle * 0.5f));
            // If the angle between forward and where the player is, is less than half the angle of view...
            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                Vector3 raycast = ((scaledRadius) * (direction.normalized)) + correctedPosition + (5f * transform.up);

                Debug.DrawLine(transform.position, raycast, Color.cyan);

                // ... and if a raycast towards the player hits something...
                if (Physics.Raycast(correctedPosition + (5f * transform.up), direction.normalized, out hit, scaledRadius))
                {
                    Debug.Log(hit.collider.gameObject);
                    // ... and if the raycast hits the player...
                    if (hit.collider.gameObject == player)
                    {
                        Debug.Log("Raycast hit player");
                        // ... the player is in sight.
                        playerInSight = true;

                        // Set the last global sighting is the players current position.
                        lastPlayerSighting.position = player.transform.localPosition;
                    }
                }
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the player leaves the trigger zone...
        if (other.gameObject == player)
            // ... the player is not in sight.
            playerInSight = false;
    }
}