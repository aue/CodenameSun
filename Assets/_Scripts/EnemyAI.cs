﻿using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;                          // The nav mesh agent's speed when patrolling.
    public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
    public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
    public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
    public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.
    public Canvas story;
    public Animator ani;
    public bool gameStart = false;

    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Transform player;
    //private PlayerHealth playerHealth;
    private LastPlayerSighting lastPlayerSighting;
    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIndex;

    void Start()
    {
        if (story != null)
        {
            story = story.GetComponent<Canvas>();
        }
    }

    void Awake()
    {
        // Setting up the references.
        ani = GetComponent<Animator>();
        enemySight = GetComponent<EnemySight>();
        Debug.Log(enemySight.playerInSight);
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        
    }

    void Update() {
        Debug.Log(enemySight.playerInSight);
        if (story != null) {
            gameStart = !(story.isActiveAndEnabled);
        }

        if ( (gameStart) || (story == null) )
        {
            if (enemySight.playerInSight)
            {
                Debug.Log("Attacking");
                ani.SetInteger("CurrentAction", 0);
                Attacking();
            }
            else if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition)
            {
                Debug.Log("Chasing");
                ani.SetInteger("CurrentAction", 0);
                nav.Resume();
                Chasing();
            }
            else
            {
                Debug.Log("Patrolling");
                ani.SetInteger("CurrentAction", 0);
                nav.Resume();
                Patrolling();
            }
        }
    }

    void Attacking()
    {
        Debug.Log("Attacking");

        nav.destination = enemySight.personalLastSighting;
        Debug.Log("rem dist " + nav.remainingDistance + (nav.remainingDistance < 100f));
        if (nav.remainingDistance < 50f)
        {
            nav.Stop();
            Debug.Log("Hit pls");
            ani.SetInteger("CurrentAction", 3);
            //Debug.Log(ani.get)
            StartCoroutine(Wait());
        }
        
    }

    IEnumerator Wait()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(1000);
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

        nav.destination = patrolWayPoints[wayPointIndex].localPosition;
    }
}
