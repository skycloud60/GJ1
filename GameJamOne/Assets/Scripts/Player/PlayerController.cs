using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour
{

    public XboxController controller;

    // THROW VARS
    //throw angle must be between 0-1
    public Vector3 aim;
    public Transform throwPos;
    public int throwPower;
    public float throwCooldown;
    public float throwAngle;
    public float throwForce;
    public float throwCharge = 0;
    public bool charging = false;
    public bool canThrow = false;

    // COOK VARS
    public GameObject cookPos;
    public GameObject currentFood;
    protected GameObject currentlyOnStove;
    public GameObject foodA;
    public GameObject foodB;
    public GameObject foodX;
    public GameObject foodY;
    public bool isCooking;

    protected Foods foodScript;

    private void Awake()
    {
        //setFoodType = currentFood.GetComponent<Foods>();
    }

    // Use this for initialization

    void Start()
    {

        throwCooldown = 0;
        throwForce = 500.0f;
        isCooking = false;
        currentFood = null;


    }

    // Update is called once per frame
    void Update()
    {
        if (throwCooldown > 0)
        {
            throwCooldown -= Time.deltaTime;
        }
        else if (throwCooldown <= 0)
        {
            throwCooldown = 0;
        }

        //--------------------------------------------------
        // COOK BUTTONS
        //--------------------------------------------------


        if (XCI.GetButtonDown(XboxButton.A, controller))
        {
            if (currentlyOnStove)
            {
                Foods temp = currentlyOnStove.GetComponent<Foods>();
                if((int)temp.thisFoodType == 4)
                {
                    temp.SetFoodType(0);
                }
                if ((int)temp.thisFoodType == 0)
                {
                    temp.Chop();
                }
            }
            else
            {
                SetFood(foodA, 0);                
            }
        }
        if (XCI.GetButtonDown(XboxButton.B, controller))
        {
            if (currentlyOnStove)
            {
                Foods temp = currentlyOnStove.GetComponent<Foods>();
                if ((int)temp.thisFoodType == 4)
                {
                    temp.SetFoodType(1);
                }
                if ((int)temp.thisFoodType == 1)
                {
                    temp.Chop();
                }
            }
            else
            {
                SetFood(foodB, 1);
            }
        }
        if (XCI.GetButtonDown(XboxButton.X, controller))
        {
            if (currentlyOnStove)
            {
                Foods temp = currentlyOnStove.GetComponent<Foods>();
                if ((int)temp.thisFoodType == 4)
                {
                    temp.SetFoodType(2);
                }
                if ((int)temp.thisFoodType == 2)
                {
                    temp.Chop();
                }
            }
            else
            {
                SetFood(foodX, 2);
            }
        }
        if (XCI.GetButtonDown(XboxButton.Y, controller))
        {
            if (currentlyOnStove)
            {
                Foods temp = currentlyOnStove.GetComponent<Foods>();
                if ((int)temp.thisFoodType == 4)
                {
                    temp.SetFoodType(3);
                }
                if ((int)temp.thisFoodType == 3)
                {
                    temp.Chop();
                }
            }
            else
            {
                SetFood(foodY, 3);
            }
        }


        //--------------------------------------------------
        // LEFT ANALOG STICK AIM
        //--------------------------------------------------

        Vector2 leftInput = new Vector2(XCI.GetAxisRaw(XboxAxis.LeftStickX, controller),
                                        XCI.GetAxisRaw(XboxAxis.LeftStickY, controller));



        if (leftInput.x != 0 || leftInput.y != 0)
        {
            aim.x = leftInput.x;
            aim.y = 0;
            aim.z = leftInput.y;
            aim.Normalize();

            Vector3 up = new Vector3(0, 0.2f);

            Debug.DrawRay(this.transform.position + up, aim);


            charging = true;
            if (charging)
            {
                throwCharge += Time.deltaTime;
                if (currentFood != null)
                {
                    canThrow = true;
                }

            }

            if (throwCooldown <= 0 && ((XCI.GetButton(XboxButton.LeftBumper, controller)) || (XCI.GetButton(XboxButton.RightBumper, controller))))
            {
                if (throwCharge <= 0.5)
                {
                    ThrowFood(currentFood, throwPos.position, 0.1f);
                }

                if (throwCharge > 0.5f && throwCharge < 1.5f)
                {
                    ThrowFood(currentFood, throwPos.position, 0.3f);
                }

                if (throwCharge >= 1.5f)
                {
                    ThrowFood(currentFood, throwPos.position, 0.5f);
                }

            }
        }

        else if (leftInput.x == 0 || leftInput.y == 0)
        {
            ResetAim();
        }
    }

    private void ResetAim()
    {
        aim.x = 0;
        aim.y = 0;
        aim.z = 0;
        charging = false;
        canThrow = false;
        throwCharge = 0;
        throwAngle = 0;
    }
    

    private void ThrowFood(GameObject food, Vector3 sPos, float throwAngle)
    {
        Vector3 actualAim = aim;
        actualAim.y = throwAngle;
        actualAim.Normalize();
        // GameObject newFood = Instantiate(currentFood, sPos, Quaternion.Euler(actualAim)) as GameObject;

        if (currentlyOnStove != null && currentlyOnStove.GetComponent<Foods>().isReady)
        {
            currentlyOnStove.transform.position = throwPos.position;
            currentlyOnStove.transform.Rotate(Vector3.right, throwAngle);
            currentlyOnStove.GetComponent<Rigidbody>().AddForce(actualAim * throwForce);
            throwCooldown = 1.0f;
            currentFood = null;
            currentlyOnStove = null;
            ResetAim();
            //throw sound
        }
    }

    private void SetFood(GameObject food, int val)
    {
        isCooking = true;
        canThrow = true;
        currentFood = food;

        GameObject setFood = Instantiate(currentFood, cookPos.transform) as GameObject;
        setFood.transform.position = cookPos.transform.position;
        currentlyOnStove = setFood;
        currentlyOnStove.GetComponent<Foods>().SetFoodType(val);
        //set food plop sound?
    }

  

}
