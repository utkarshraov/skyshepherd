using System.Collections;
using UnityEngine;

public class beeAI : MonoBehaviour {

    private GameObject queenBee;
    private bool started = false;
    private float minSpeed;
    private float maxSpeed;
    private float chaos;
    private GameObject target;

    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
        StartCoroutine("beeNavigation");
	}

	void Update () {
        transform.LookAt(transform.forward);
	}



    IEnumerator beeNavigation()
    {
        while(true)
        {
            if(started)
            {
                rb.velocity = rb.velocity  + whereIsTheFlower() * Time.deltaTime * 2;

                float speed = rb.velocity.magnitude;
                if (speed>maxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * maxSpeed;
                }
                else if (speed < minSpeed)
                {
                    rb.velocity = rb.velocity.normalized * minSpeed;
                }
            }

            float waitTime = Random.Range(0.3f, 0.4f);
            yield return new WaitForSeconds(waitTime);
        }
    }


    private Vector3 whereIsTheFlower()
    {
        Vector3 randomise = new Vector3(Random.value, Random.value, Random.value);
        randomise.Normalize();
        hiveController queen = queenBee.GetComponent<hiveController>();

        Vector3 hiveCenter = queen.hiveCenter;
        Vector3 hiveSpeed = queen.hiveSpeed;
        Vector3 follow = target.transform.position;

        hiveCenter = hiveCenter - transform.localPosition;
        hiveSpeed = hiveSpeed - rb.velocity;
        follow = follow - transform.position;


        return (hiveCenter + hiveSpeed + follow * 2 + randomise * chaos);
    }

    public void setQueen(GameObject newQueen)
    {
        queenBee = newQueen;
        hiveController qb = queenBee.GetComponent<hiveController>();
        minSpeed = qb.minVelocity;
        maxSpeed = qb.maxVelocity;
        chaos = qb.chaos;
        target = qb.target;
        started = true;
    }
}
