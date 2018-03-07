using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using XboxCtrlrInput;

public class Cook : MonoBehaviour {

    private PlayerController sr_pController;
    private Foods sr_food;
    //private GameObject chefferson;
    public XboxController controller;

    public GameObject foodA;
    public GameObject foodB;
    public GameObject foodX;
    public GameObject foodY;
    public Image chopDisplay;
    public float fillCurrent;
    public float fillMax;
    
    public bool doneChopping;
    
    // SCRIPT REFERENCE


    private void Awake()
    {
        sr_pController = GetComponent<PlayerController>();
        sr_food = GetComponent<Foods>();

        if (gameObject.tag == "Player1")
        {
            controller = XboxController.First;
        }
        else if (gameObject.tag == "Player2")
        {
            controller = XboxController.Second;
        }

    }

    // Use this for initialization
    void Start () {
        doneChopping = false;
        fillCurrent = 0;
        fillMax = 6f;

	}
	
	// Update is called once per frame
	void Update () {

        if(sr_pController.currentlyOnStove != null)
        {
            chopDisplay.fillAmount = fillCurrent / fillMax;
        
        }

        //--------------------------------------------------
        // COOK BUTTONS
        //--------------------------------------------------

        if (XCI.GetButtonDown(XboxButton.A, controller))
        {
            if (sr_pController.currentlyOnStove)
            {
                Foods temp = sr_pController.currentlyOnStove.GetComponent<Foods>();
                if (temp.isReady)
                {
                    ToggleDone(true);
                }
                else if (!temp.isReady)
                {
                    if ((int)temp.thisFoodType == 0)
                    {
                        temp.SetFoodType(1);
                    }
                    if ((int)temp.thisFoodType == 1)
                    {
                        temp.getChopped();
                        fillCurrent += 1f;
                    }
                }

            }

            else
            {
                SetFood(foodA, 1);
            }
        }

        if (XCI.GetButtonDown(XboxButton.B, controller))
        {
            if (sr_pController.currentlyOnStove)
            {
                Foods temp = sr_pController.currentlyOnStove.GetComponent<Foods>();
                if (!temp.isReady)
                {
                    if ((int)temp.thisFoodType == 0)
                    {
                        temp.SetFoodType(2);
                    }
                    if ((int)temp.thisFoodType == 2)
                    {
                        temp.getChopped();
                        fillCurrent += 1;
                    }
                }
                else if (temp.isReady)
                {
                    ToggleDone(true);
                }
            }

            else
            {
                SetFood(foodB, 2);
            }
        }

        if (XCI.GetButtonDown(XboxButton.X, controller))
        {
            if (sr_pController.currentlyOnStove)
            {
                Foods temp = sr_pController.currentlyOnStove.GetComponent<Foods>();
                if (!temp.isReady)
                {
                    //chopDisplay.fillAmount = sr_food.chopProgress / sr_food.chopFinish;
                    if ((int)temp.thisFoodType == 0)
                    {
                        temp.SetFoodType(3);
                    }
                    if ((int)temp.thisFoodType == 3)
                    {
                        temp.getChopped();
                        fillCurrent += 1;
                    }
                }
                else if (temp.isReady)
                {
                    ToggleDone(true);
                }
            }

            else
            {
                SetFood(foodX, 3);
            }
        }

        if (XCI.GetButtonDown(XboxButton.Y, controller))
        {
            if (sr_pController.currentlyOnStove)
            {
                Foods temp = sr_pController.currentlyOnStove.GetComponent<Foods>();
                if (!temp.isReady)
                {
                    //chopDisplay.fillAmount = sr_food.chopProgress / sr_food.chopFinish;
                    if ((int)temp.thisFoodType == 0)
                    {
                        temp.SetFoodType(4);
                    }
                    if ((int)temp.thisFoodType == 4)
                    {
                        temp.getChopped();
                        fillCurrent += 1;
                    }
                }
                else if (temp.isReady)
                {
                    ToggleDone(true);
                }
            }

            else
            {
                SetFood(foodY, 4);
            }
        }
    }

    private void SetFood(GameObject food, int val)
    {
        sr_pController.canThrow = true;
        sr_pController.currentFood = food;

        GameObject setFood = Instantiate(sr_pController.currentFood, sr_pController.cookPos.transform) as GameObject;
        
        if( sr_pController.tag == "Player1")
        {
            setFood.gameObject.tag = "Player1";
        }
        if (sr_pController.tag == "Player2")
        {
            setFood.gameObject.tag = "Player2";
        }
       
        setFood.transform.position = sr_pController.cookPos.transform.position;
        sr_pController.currentlyOnStove = setFood;
        sr_pController.currentlyOnStove.GetComponent<Foods>().SetFoodType(val);
        //set food plop sound?
    }

    public void ToggleDone(bool tof)
    {
        doneChopping = tof;
    }

    public void ToggleChopDone(int val)
    {
        fillCurrent = val;
    }


}
