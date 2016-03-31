using UnityEngine;
using System.Collections;

public class Amanda_Script : MonoBehaviour
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

        if (Input.GetButtonDown("Jump"))
        {
            myAnimator.SetBool("Jumping", true);
            Invoke("StopJumping", 0.1f);
        }

        //Actions for the Character
        if (Input.GetMouseButtonDown(0))
        {
            if (myAnimator.GetInteger("CurrentAction") == 0)
            {
                myAnimator.SetInteger("CurrentAction", 1);
            }

        }
        
        else if (Input.GetKeyDown("1"))
        {
            if (myAnimator.GetInteger("CurrentAction") == 0)
            {
                myAnimator.SetInteger("CurrentAction", 2);
            }

        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            if (myAnimator.GetInteger("CurrentAction") == 0)
            {
                myAnimator.SetInteger("CurrentAction", 5);
            }
        }
        else
        {
            myAnimator.SetInteger("CurrentAction", 0);
        }
    }
    void StopJumping()
    {
        myAnimator.SetBool("Jumping", false);
    }
}
