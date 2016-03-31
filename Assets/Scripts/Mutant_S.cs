using UnityEngine;
using System.Collections;

public class Mutant_S : MonoBehaviour
{

    public Animator ani;

    public Rigidbody rbody;

    private Quaternion qTo;
    public float speed = 2.0f;

    //private bool roll;
    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
    
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetFloat("inputV", Input.GetAxis("Horizontal"));

        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), 0.0f);

        if (direction != Vector2.zero)
            qTo = Quaternion.LookRotation(direction);
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
        transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * speed);

        //Jumping mechanism
        if (Input.GetButtonDown("Jump"))
        {
            ani.SetBool("Jump", true);
            Invoke("StopJumping", 0.1f);
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



