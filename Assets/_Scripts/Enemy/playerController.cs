using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    //Movement Variables
    public float runSpeed;
    Rigidbody myRB;
    Animator myAnim;
    bool facingRight = true;

    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;


	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        
        myAnim.SetFloat("speed", Mathf.Abs(move));

        myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);

        if (move > 0 && facingRight == false) Flip();
        else if (move < 0 && facingRight == true) Flip();

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;

        myAnim.SetBool("grounded", grounded);

        if(grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            myAnim.SetBool("grounded", grounded);

            myRB.AddForce(new Vector3(0, jumpHeight, 0));
        }
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public float GetFacing()
    {
        if (facingRight) return 1;
        else return -1;
    }
}
