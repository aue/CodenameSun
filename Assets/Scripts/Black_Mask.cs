using UnityEngine;
using System.Collections;

public class Black_Mask : MonoBehaviour
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
       
    }

}



