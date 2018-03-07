using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Customers : MonoBehaviour {

    private Foods sr_foods;
    private PlayerController sr_pController;
    private NavMesh sr_navMesh;

    //public enum fOrder
    //{
    //    chicken = 0,
    //    steak = 1,
    //    pancakes = 2,
    //    burger = 3
    //}

    //public fOrder custOrder;

    public Text speechBub;
    public Image orderImage;
    public Image aChicken;
    public Image aSteak;
    public Image aBurger;
    public Image aPancakes;
    public Image goodReview;
    public Image badReview;
    public GameObject spawnPoint;
    public GameObject exitPoint;
    public GameObject[] seats;
    public foodType custOrder;
    private bool hasOrdered;
    private bool isFed;

    private void Awake()
    {
        sr_foods = gameObject.GetComponent<Foods>();
        sr_pController = gameObject.GetComponent<PlayerController>();
        sr_navMesh = gameObject.GetComponent<NavMesh>();
    }

    // Use this for initialization
    void Start () {
        seats = GameObject.FindGameObjectsWithTag("Seat");
        custOrder = (foodType)Random.Range(1, 4);
        hasOrdered = false;
        isFed = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(hasOrdered == false)
        {
            if((int)custOrder == 1)
            {
                speechBub.text = "chicken!";
                orderImage = aChicken;
            }

            if((int)custOrder == 2)
            {
                speechBub.text = "steak me";
                orderImage = aSteak;
            }

            if((int)custOrder == 3)
            {
                speechBub.text = "burger wif cheese";
                orderImage = aBurger;
            }

            if((int)custOrder == 4)
            {
                speechBub.text = "pancakes pls";
                orderImage = aPancakes;
            }
        }

        if(isFed)
        {
            sr_navMesh.Leave();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (custOrder == sr_foods.thisFoodType)
        {
            isFed = true;
            speechBub.text = "aw yis";
            orderImage = goodReview;
            sr_pController.Scoring(2);
            Destroy(other.gameObject);
        }

        if(custOrder != sr_foods.thisFoodType)
        {
            isFed = false;
            speechBub.text = "no!";
            orderImage = badReview;
            sr_pController.Scoring(-1);
            Destroy(other.gameObject);
        }



    }



}
