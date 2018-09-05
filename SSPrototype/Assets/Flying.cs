using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour {

    public float forwardForce = 50;
    private Vector3 forceVector;
    private Rigidbody rb;
	void Start () {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(100, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 camPos = transform.position - transform.forward * 10f + Vector3.up * 5f;
        float bias = 0.98f;
        Camera.main.transform.position = Camera.main.transform.position * bias + camPos * (1.0f-bias);
        Camera.main.transform.LookAt(transform.position + transform.forward * 20f);



        forwardForce -= transform.forward.y * 50f *  Time.deltaTime;

        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -2.0f * Input.GetAxis("Horizontal"));
        transform.position += transform.forward * Time.deltaTime * forwardForce;

        if(forwardForce <20)
        {
            forwardForce = 20f;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            forwardForce -= 40* Time.deltaTime;
        }
    }
     
    private void FixedUpdate()
    {
        //float forwardVelocity = rb.velocity.magnitude;
        //Vector3 liftForce = new Vector3(0, forwardVelocity* 2,0);
        //rb.AddForce(liftForce,ForceMode.Force);
        ////transform.rotation = Quaternion.LookRotation(rb.velocity);

    }
}
