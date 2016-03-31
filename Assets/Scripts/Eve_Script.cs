using UnityEngine;
using System.Collections;

public class Eve_Script : MonoBehaviour
{
    private Animator myAnimator;
    private Quaternion qTo;
    public float speed = 2.0f;
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

        if (Input.GetKey(KeyCode.LeftControl))
        {
            myAnimator.SetBool("Crouch", true);


        }
        else
        {
            myAnimator.SetBool("Crouch", false);
        }

        //========================================
        //Jumping
        if (Input.GetButtonDown("Jump"))
        {
            myAnimator.SetBool("Jumping", true);
            Invoke("StopJumping", 0.1f);
        }

        //Actions for the Character
        //death 
        if (Input.GetKeyDown("x"))
        {
            if (myAnimator.GetInteger("CurrentAction") == 0)
            {
                myAnimator.SetInteger("CurrentAction", 1);
            }
          
        }
        //somethig cool
        else if (Input.GetKeyDown("1"))
        {
            if (myAnimator.GetInteger("CurrentAction") == 0)
            {
                myAnimator.SetInteger("CurrentAction", 2);
            }

        }
        // punching 3 && 4
        else if (Input.GetMouseButtonDown(0))
        {
            if (myAnimator.GetInteger("CurrentAction") == 0)
            {
                int n = Random.Range(0, 2);
                if (n == 0)
                {
                    myAnimator.SetInteger("CurrentAction", 3);
                }
                else
                {
                    myAnimator.SetInteger("CurrentAction", 4);
                }
            }
        }
        // hook and uppercut 3 && 4
        else if (Input.GetMouseButtonDown(1))
        {

            if (myAnimator.GetInteger("CurrentAction") == 0)
            {
                int n = Random.Range(0, 2);
                if (n == 0)
                {
                    myAnimator.SetInteger("CurrentAction", 5);
                }
                else
                {
                    myAnimator.SetInteger("CurrentAction", 6);
                }
            }

        }
        else
        {
            myAnimator.SetInteger("CurrentAction", 0);
        }

        //// crounching
        //if (Input.GetKey(KeyCode.LeftControl))
        //{
        //    if (myAnimator.GetInteger("CurrentAction") == 0)
        //    {
        //        myAnimator.SetInteger("CurrentAction", 5);
        //    }
        //    else if (myAnimator.GetInteger("CurrentAction") == 5)
        //    {
        //        myAnimator.SetInteger("CurrentAction", 0);
        //    }
        //}
    }
    void StopJumping()
    {
        myAnimator.SetBool("Jumping", false);
    }
}
