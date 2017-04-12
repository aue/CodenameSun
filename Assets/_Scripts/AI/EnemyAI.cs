using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;                          // The nav mesh agent's speed when patrolling.
    public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
    public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
    public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
    public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.

    private EnemySight enemySight;
    private UnityEngine.AI.NavMeshAgent nav;
    private Transform player;
    private GameObject enemy;
    private Animator myAnimator;
    //private PlayerHealth playerHealth;
    private LastPlayerSighting lastPlayerSighting;
    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIndex;
    private bool Detected;
    private bool running;

    void Awake()
    {
        // Setting up the references.
        enemySight = GetComponent<EnemySight>();
        //enemy = GameObject.FindGameObjectWithTag("enemy");
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        myAnimator = GetComponent<Animator>();
        Detected = false;
        running = false;
    }

    void FixedUpdate() {
        
        if (enemySight.playerInSight)
        {
            Debug.Log("Attacking");
            Attacking();
        }
        else if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition)
        {
            Debug.Log("Chasing");
            nav.Resume();
            //myAnimator.SetInteger("CurrentAction", 0);
            Chasing();
        }
        else
        {
            Detected = false;
            if (running)
            {
                myAnimator.SetBool("detected", Detected);
                running = false;
            }
            Debug.Log("Patrolling");
            //myAnimator.SetInteger("CurrentAction", 0);
            Patrolling();
        }

        if (!Detected) myAnimator.SetBool("walk", true);
        else myAnimator.SetBool("walk", false);
    }

    void Attacking()
    {
        if (!Detected)
        {
            Detected = true;
            myAnimator.SetBool("detected", Detected);
            running = true;
        }
    }

    void Chasing()
    {
        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
        if (sightingDeltaPos.sqrMagnitude > 4f)
            nav.destination = enemySight.personalLastSighting;

        nav.speed = chaseSpeed;

        Debug.Log("rem distance is " + nav.remainingDistance + " and stopping dist is " + nav.stoppingDistance);
        if (nav.remainingDistance < nav.stoppingDistance)
        {
            Debug.Log("Chase timer is " + chaseTimer + " and chase wait time is " + chaseWaitTime);
            chaseTimer += Time.deltaTime;

            if (chaseTimer > chaseWaitTime)
            {
                lastPlayerSighting.position = lastPlayerSighting.resetPosition;
                enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
                chaseTimer = 0f;
            }
        }
        else
        {
            chaseTimer = 0f;
        }
    }

    void Patrolling()
    {
        Debug.Log(wayPointIndex + "is the way point index");
        //Set an appropriate speed for the NavMeshAgent
        nav.speed = patrolSpeed;

        if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
        {
            //increment timer
            patrolTimer += Time.deltaTime;

            //if timer exceeds wait time
            if(patrolTimer >= patrolWaitTime)
            {
                //increment waypoint index
                if(wayPointIndex == patrolWayPoints.Length - 1)
                {
                    wayPointIndex = 0;
                }
                else
                {
                    wayPointIndex++;
                }

                //reset timer
                patrolTimer = 0f;
            }
        }

        else //Use to put back on patrol eventually
        {
            patrolTimer = 0f;
        }

        nav.destination = patrolWayPoints[wayPointIndex].position;
    }
}
