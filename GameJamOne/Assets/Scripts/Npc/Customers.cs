using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customers : MonoBehaviour {

    public GameObject spawnPoint;
    public GameObject exitPoint;
    public GameObject[] seats;

	// Use this for initialization
	void Start () {
        seats = GameObject.FindGameObjectsWithTag("Seat");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
