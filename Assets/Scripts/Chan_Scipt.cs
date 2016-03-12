using UnityEngine;
using System.Collections;

public class Chan_Scipt : MonoBehaviour {

    public Animator ani;

    public Rigidbody rbody;

    private float inputH;
    private float inputV;
    private bool run;
  
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        run = false;
	}
	
	// Update is called once per frame
	void Update () {
	   if(Input.GetKeyDown("1"))
        {
            ani.Play("WAIT01",-1,0f);
         }
        if (Input.GetKeyDown("2"))
        {
            ani.Play("WAIT02", -1, 0f);
        }
        if (Input.GetKeyDown("3"))
        {
            ani.Play("WAIT03", -1, 0f);
        }
        if (Input.GetKeyDown("4"))
        {
            ani.Play("WAIT04", -1, 0f);
        }
        if (Input.GetMouseButtonDown(0))
        {
            int n = Random.Range(0, 2);
            if (n == 0)
            {
                ani.Play("DAMAGED00", -1, 0f);
            }
            else
            {
                ani.Play("DAMAGED01", -1, 0f);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            ani.SetBool("jump", true);


        }
        else
        {
            ani.SetBool("jump", false);
        }

        //==========================================
        //this is for crouching 
        if (Input.GetKey(KeyCode.LeftControl))
        {
            ani.SetBool("crouch", true);


        }
        else
        {
            ani.SetBool("crouch", false);
        }
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        ani.SetFloat("inputH", inputH);
        ani.SetFloat("inputV", inputV);
        ani.SetBool("run", run);

        float moveX = inputH * 20f * Time.deltaTime;
        float moveZ = inputV * 50f * Time.deltaTime;
        if (moveZ <= 0f)
        {
            moveX = 0f;
        }
        else if(run)
        {
            moveX *= 3f;
            moveZ *= 3f;
        }
        rbody.velocity = new Vector3(moveX, 0f, moveZ);
    }
}
