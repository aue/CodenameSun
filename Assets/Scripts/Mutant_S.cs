using UnityEngine;
using System.Collections;

public class Mutant_S : MonoBehaviour
{

    public Animator ani;

    public Rigidbody rbody;

    private float inputH;
    private float inputV;
    private bool run;

    //private bool roll;
    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        run = false;
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetFloat("inputV", Input.GetAxis("Vertical"));

        //Jumping mechanism
        if (Input.GetButtonDown("Jump"))
        {
            ani.SetBool("Jump", true);
            Invoke("StopJumping", 0.1f);
        }

        //Turnning Left
        if (Input.GetKey("q"))
        {
            transform.Rotate(Vector3.down * Time.deltaTime * 100.0f);

            if((Input.GetAxis("Vertical") == 0f) && (Input.GetAxis("Horizontal") == 0f))
            {
                ani.SetBool("TurnLeft", true);
            }

        }
        else
        {
            ani.SetBool("TurnLeft", false);
        }
        //Turn Right
        if (Input.GetKey("e"))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 100.0f);

            if ((Input.GetAxis("Vertical") == 0f) && (Input.GetAxis("Horizontal") == 0f))
            {
                ani.SetBool("TurnRight", true);
            }
            
        }
        else
        {
            ani.SetBool("TurnRight", false);
        }
        //================================
        //Actions

        //Flexing
        if (Input.GetKeyDown("1"))
        {
            if (ani.GetInteger("CurrentAction") == 0)
            {
                ani.SetInteger("CurrentAction", 1);
            }
        } 
        

        //Roaring
        else if (Input.GetKeyDown("2"))
        {
            if (ani.GetInteger("CurrentAction") == 0)
            {
                ani.SetInteger("CurrentAction", 2);
            }
        }
        
        //Punching
        else if (Input.GetMouseButtonDown(0))
        {
            if (ani.GetInteger("CurrentAction") == 0)
            {
                ani.SetInteger("CurrentAction", 3);
            }
        }
        

        //Swipping
        else if (Input.GetMouseButtonDown(1))
        {
            if (ani.GetInteger("CurrentAction") == 0)
            {
                ani.SetInteger("CurrentAction", 4);
            }
        }
       

        //Jump Attack
        else if (Input.GetKeyDown("3"))
        {
            if (ani.GetInteger("CurrentAction") == 0)
            {
                ani.SetInteger("CurrentAction", 5);
            }
        }
        

        //Dying 
        else if (Input.GetKeyDown("x"))
        {
            if (ani.GetInteger("CurrentAction") == 0)
            {
                ani.SetInteger("CurrentAction", 6);
            }
        }
        else
        {

            ani.SetInteger("CurrentAction", 0);
        }
    }

    void StopJumping()
    {
        ani.SetBool("Jump", false);
    }

}



