using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foods : MonoBehaviour {

    public float lifeSpan;
    private GameObject food;

	// Use this for initialization
	void Start () {
        lifeSpan = 2.0f;
        food = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
        {
            Destroy(food);
        }



	}
}
