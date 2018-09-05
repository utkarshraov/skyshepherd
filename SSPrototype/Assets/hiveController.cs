using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hiveController : MonoBehaviour {

    public float minVelocity;
    public float maxVelocity;
    public float chaos;
    public int hiveSize;
    public GameObject beePrefab;
    public GameObject target;

    public Vector3 hiveCenter;
    public Vector3 hiveSpeed;
    private Collider coll;

    private GameObject[] bees;
    void Start () {
        bees = new GameObject[hiveSize];
        
        
        for (int i=0;i<hiveSize;i++)
        {
            coll = GetComponent<Collider>();
            Vector3 newPosition = new Vector3(Random.value * coll.bounds.size.x, Random.value * coll.bounds.size.y, Random.value * coll.bounds.size.z) - coll.bounds.extents;
            GameObject bee = Instantiate(beePrefab, transform.position, transform.rotation) as GameObject;
            bee.transform.parent = transform;
            bee.transform.localPosition = newPosition;
            bee.GetComponent<beeAI>().setQueen(gameObject);
            bees[i] = bee;
        }
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 center = Vector3.zero;
        Vector3 velocity = Vector3.zero;

        foreach(GameObject b in bees)
        {
            center = center + b.transform.localPosition;
            velocity = velocity + b.GetComponent<Rigidbody>().velocity;
        }

        hiveCenter = center / hiveSize;
        hiveSpeed = velocity / hiveSize;


	}
}
