using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foods : MonoBehaviour {

    public float lifeSpan;
    private GameObject food;
    public int chopsToCook;

	// Use this for initialization
	void Start () {

        food = this.gameObject;
        chopsToCook = 10;
        lifeSpan = 2.0f;
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
