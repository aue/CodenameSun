using UnityEngine;
using System.Collections;

public class mutantController : MonoBehaviour {

    public GameObject flipModel;

    //audio options
    /*
    public AudioClip[] idleSounds;
    public float idleSoundTIme;
    AudioSource enemyMovementAS;
    float nextIdleSound = 0f;
    */
    //public float detectionTime;
    float startRun;
    bool firstDetection;

    //movement options
    public float runSpeed;
    //pubilc float walkSpeed;
    public bool facingRight;

    float moveSpeed;
    bool running;

    Rigidbody myRB;
    Animator myAnim;
    Transform detectedPlayer;
    bool Detected;

	// Use this for initialization
	void Start () {
        myRB = GetComponentInParent<Rigidbody>();
        myAnim = GetComponentInParent<Animator>();
        //enemyMovementAS = GetComponent<AudioSource>();

        running = false;
        Detected = false;
        firstDetection = false;
        moveSpeed = runSpeed;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Player" && !Detected)
        {
            Detected = true;
            detectedPlayer = other.transform;
            myAnim.SetBool("detected", Detected);
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip(); 
        }

        if(other.tag == "patrolwalk")
        {

        }
    }

    void FixedUpdate()
    {
        if (Detected)
        {
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();

            if (!firstDetection)
            {
                firstDetection = true;
            }

			if (myAnim.GetBool ("detected") == false) {
				myRB.velocity = new Vector3 (0, myRB.velocity.y, 0);
			} else if (Vector3.Distance (transform.position, detectedPlayer.transform.position) > 3) {
				if (Detected && !facingRight) {
					myRB.velocity = new Vector3 (moveSpeed * -1, myRB.velocity.y, moveSpeed * -1);
				} else if (Detected && facingRight) {
					myRB.velocity = new Vector3 (moveSpeed, myRB.velocity.y, moveSpeed);
				}
			}

            if(!running && Detected)
            {
                moveSpeed = runSpeed;
                myAnim.SetBool("detected", Detected);
                running = true;
            }
            if (!running)
            {
                
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
		if(other.tag == "Player")
        {
            firstDetection = false;
            Detected = false;
            if (running)
            {
                moveSpeed = 0;
                myAnim.SetBool("detected", Detected);
                running = false;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = flipModel.transform.localScale;
        theScale.z *= -1;
        flipModel.transform.localScale = theScale;
    }
}
