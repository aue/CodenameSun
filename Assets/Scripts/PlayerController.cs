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
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);

        if (Input.GetKeyDown("space"))
        {
            player.Translate(Vector3.up * jumpSpeed * Time.deltaTime, Space.World);
        }
    }
}
