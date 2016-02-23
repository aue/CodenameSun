using UnityEngine;
using System.Collections;

public class HelperAI : MonoBehaviour
{
    public float followSpeed = 2f;

    private NavMeshAgent nav;
    private Transform player;
    private Vector3 offset;
    private Vector3 destination;

    // Use this for initialization
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        destination = offset + player.transform.position;
    }

    void FixedUpdate()
    {
        Following();
    }

    void Following()
    {
        nav.speed = followSpeed;
        nav.destination = destination;
    }
}
