using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foods : MonoBehaviour {

 

    public enum foodType
    {
        empty = 0,
        chicken = 1,
        steak  = 2,
        salad = 3,
        pancakes = 4
    }

    public foodType thisFoodType;

    public bool isReady;
    public int chopProgress;
    public int chopFinish;
    public float foodDuration;


    private void Awake()
    {

    }

    // Use this for initialization
    void Start () {

        isReady = false;
        chopProgress = 0;
        chopFinish = 6;
        foodDuration = 12.0f;
	}
	
	// Update is called once per frame
	void Update () {
        foodDuration -= Time.deltaTime;
        if (foodDuration <= 0)
        {
            Destroy(this.gameObject);
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
        //if (other.gameObject.tag == NPC)

    }


    public void getChopped()
    {
        if (chopProgress < chopFinish)
        {
            chopProgress += 1;
        }

        if (chopProgress >= chopFinish)
        {                                                                                      
           isReady = true;
        }


        
    }

    public void SetFoodType(int val)
    {
        if (val == 1)
        {
            thisFoodType = foodType.chicken;
        }
        if (val == 2)
        {
            thisFoodType = foodType.steak;
        }
        if (val == 3)
        {
            thisFoodType = foodType.salad;
        }
        if (val == 4)
        {
            thisFoodType = foodType.pancakes;
        }

    } 
      
    public void ToggleReady(bool tof)
    {
        isReady = tof;
    }
    

}
