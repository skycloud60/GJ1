using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum foodType
{
    empty = 0,
    chicken = 1,
    steak  = 2,
    burger = 3,
    pancakes = 4
}

public class Foods : MonoBehaviour {

    private Cook sr_cook;


    public foodType thisFoodType;

    public bool isReady;
    public int chopProgress;
    public int chopFinish;
    public float foodDuration;


    private void Awake()
    {
        sr_cook = gameObject.GetComponent<Cook>();
    }

    // Use this for initialization
    void Start () {

        isReady = false;
        chopProgress = 0;
        chopFinish = 6;
        foodDuration = 20.0f;
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
        //if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        //{
        //
        //}

    }


    public void getChopped()
    {
        chopProgress += 1;
        
        if (chopProgress >= chopFinish)
        {                                                                                      
           isReady = true;
           //sr_cook.ToggleDone(true);
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
            thisFoodType = foodType.burger;
        }
        if (val == 4)
        {
            thisFoodType = foodType.pancakes;
        }

    }

}
