using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float patrolWaitTime = 1f;
    public Transform[] patrolWayPoints;

    private NavMeshAgent nav;
    //private Transform player;
    private float patrolTimer;
    private int wayPointIndex;

	void Awake ()
    {
        nav = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag(Tags.player).transform;
	}
	
	void FixedUpdate () {
        Patrolling();
	}

    void Patrolling()
    {
        //Set an appropriate speed for the NavMeshAgent
        nav.speed = patrolSpeed;

        if(nav.remainingDistance < nav.stoppingDistance)
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
                patrolTimer = 0;
            }
        }

        else //Use to put back on patrol eventually
        {
            
        }

        nav.destination = patrolWayPoints[wayPointIndex].position;
    }
}
