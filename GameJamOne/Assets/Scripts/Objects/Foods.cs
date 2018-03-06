using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foods : MonoBehaviour {
    public enum foodType
    {
        chicken = 0,
        steak  = 1,
        salad = 2,
        pancakes = 3,
        empty = 4
    }

    public float lifeSpan;
    private GameObject food;
    public int chopsToCook;
    public int cookStatus;
    public bool isReady;

    public foodType thisFoodType;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        isReady = false;
        cookStatus = 0;
        chopsToCook = 9;
        //food = this.gameObject;
        //lifeSpan = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (cookStatus == chopsToCook)
        {
            isReady = true;                                  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // SET CONDITION FOR WALL/FLOOR COLLISION
        //if (other.gameObject.tag == wall || other.gameObject.tag == floor)
        //{
        //    Destroy(gameObject);
        //    //play splat sound?
        //    //change food animation
        //
        //}

        // SET CONDITION FOR NPC COLLISION
        //if (other.gameObject.tag == NPcC
    }
    public void Chop()
    {
        if (cookStatus == chopsToCook)
        {
            Debug.Log("DONE!");
        }

        if (cookStatus < chopsToCook)
        {
            cookStatus += 1;
        }
    }

    public void SetFoodType(int val)
    {
        if (val == 0)
        {
            thisFoodType = foodType.chicken;
        }
        if (val == 1)
        {
            thisFoodType = foodType.steak;
        }
        if (val == 2)
        {
            thisFoodType = foodType.salad;
        }
        if (val == 3)
        {
            thisFoodType = foodType.pancakes;
        }

    }
}
