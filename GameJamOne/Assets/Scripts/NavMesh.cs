using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour {


	public Transform midPoint;
	public Transform endPoint;
	private NavMeshAgent NMA;
	private bool gotFood;

	// Use this for initialization
	void Start () {
		NMA = GetComponent<NavMeshAgent> ();
		NMA.destination = midPoint.position;
	}
	
	// Update is called once per frame
	void Update () {
		Leave ();
	}


	void Leave()
	{
		if (gotFood == true) {
			NMA.destination = endPoint.position;
		}
	}
}
