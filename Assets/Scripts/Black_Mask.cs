using UnityEngine;
using System.Collections;

public class Black_Mask : MonoBehaviour
{
    private Animator myAnimator;
    private Quaternion qTo;
    public float speed = 2.0f;
    private bool run;
    // Use this for initialization
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnimator.SetFloat("VSpeed", Input.GetAxis("Horizontal"));

        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), 0.0f);

        if (direction != Vector2.zero)
            qTo = Quaternion.LookRotation(direction);
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
        transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * speed);

        //======================================
        //Crouch 
        //==========================================
        //this is for crouching 
        if (Input.GetKey(KeyCode.LeftControl))
        {
            myAnimator.SetBool("Crouch", true);


        }
        else
        {
            myAnimator.SetBool("Crouch", false);
        }

        //=========================================
        //Running

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    run = true;
        //}
        //else
        //{
        //    run = false;
        //}
        //float inputH = Input.GetAxis("Horizontal");

        //myAnimator.SetBool("run", run);

        //float moveX = inputH * 50f * Time.deltaTime;
        //if (moveZ <= 0f)
        //{
        //    moveX = 0f;
        //}
        //else if (run)
        //{
        //    moveX *= 3f;
        //    moveZ *= 3f;
        //}
        //rbody.velocity = new Vector3(moveX, 0f, moveZ);

        //=========================================
        //Jumping 

        if (Input.GetButtonDown("Jump"))
        {
            myAnimator.SetBool("Jumping", true);
            Invoke("StopJumping", 0.1f);
        }
        //=============================================
        //Actions for the Character
        // x is death
        if (Input.GetKeyDown("x"))
        {
            if (myAnimator.GetInteger("CurrentAction") == 0)
            {
                myAnimator.SetInteger("CurrentAction", 1);
            }

        }
        // something cool
        else if (Input.GetKeyDown("1"))
        {
            if (myAnimator.GetInteger("CurrentAction") == 0)
            {
                myAnimator.SetInteger("CurrentAction", 2);
            }

        }
        // 3 && 4 special punches 
        else if (Input.GetMouseButtonDown(0))
        {
            if (myAnimator.GetInteger("CurrentAction") == 0)
            {
                myAnimator.SetInteger("CurrentAction", 3);
            }

        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (myAnimator.GetInteger("CurrentAction") == 0)
            {
                myAnimator.SetInteger("CurrentAction", 4);
            }

        }
        else
        {
            myAnimator.SetInteger("CurrentAction", 0);
        }

        // crounching
        if (Input.GetKey(KeyCode.LeftControl))
        {
            myAnimator.SetBool("Crouch", true);


        }
        else
        {
            myAnimator.SetBool("Crouch", false);
        }
    }
    void StopJumping()
    {
        myAnimator.SetBool("Jumping", false);
    }
}
