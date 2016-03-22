using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float jumpSpeed = 5f;

    private Rigidbody rb;
    private Transform player;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        //Debug.Log(GameObject.FindGameObjectWithTag(Tags.player).transform.position);
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        Debug.Log("jump speed is " + jumpSpeed);
        Debug.Log("player.position is " + player.position);

        float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3( ((-1) * moveHorizontal), 0.0f, 0.0f);
        player.position += movement;

        if (Input.GetKeyDown("space"))
        {
            //player.Translate(Vector3.up * jumpSpeed * Time.deltaTime, Space.World);
            player.position += transform.up * jumpSpeed * Time.deltaTime;
        }
    }
}
