using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour {


	public Transform midPoint;
	public Transform endPoint;
	private NavMeshAgent NMA;

	public GameObject npc;
	public Transform lookAtPos;

	// Use this for initialization
	void Start () {
		NMA = GetComponent<NavMeshAgent> ();
		NMA.destination = midPoint.position;
		npc = this.gameObject;
		lookAtPos = GameObject.FindGameObjectWithTag ("LookAtPos").transform;
	}
	
	// Update is called once per frame
	void Update () {

		npc.transform.LookAt (lookAtPos);
	}


	 public void Leave()
	{
			NMA.destination = endPoint.position;
	}
}
