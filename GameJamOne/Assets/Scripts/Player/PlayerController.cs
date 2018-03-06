using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {

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

	// Use this for initialization
	void Start () {

        //foodScript = GetComponent;
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
        // LEFT ANALOG STICK AIM
        //--------------------------------------------------

        Vector2 leftInput = new Vector2(XCI.GetAxisRaw(XboxAxis.LeftStickX, controller),
                                        XCI.GetAxisRaw(XboxAxis.LeftStickY, controller));

        if (XCI.GetButtonDown(XboxButton.A, controller))
        {
            Debug.Log("hello");
            SetFood(foodA);
        }
        if (XCI.GetButtonDown(XboxButton.B, controller))
        {
            SetFood(foodB);
        }
        if (XCI.GetButtonDown(XboxButton.X, controller))
        {
            SetFood(foodX);
        }
        if (XCI.GetButtonDown(XboxButton.Y, controller))
        {
            SetFood(foodY);
        }

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
            //canThrow = true;
        }

        if (throwCooldown <= 0 && ((XCI.GetButton(XboxButton.LeftBumper, controller)) || (XCI.GetButton(XboxButton.RightBumper, controller))))
        {
            if (throwCharge <= 0.5)
            {
                throwAngle = 0.1f;
                DeliverFoodLoad(currentFood, throwPos.position, 0.1f);
                Debug.Log("shot charge 1!");
            }

            if (throwCharge > 1)
            {
                DeliverFoodLoad(currentFood, throwPos.position, 0.3f);
                Debug.Log("shot charge 2!");
            }

            if (throwCharge > 2)
            {
                DeliverFoodLoad(currentFood, throwPos.position, 0.5f);
                Debug.Log("overthrown!");
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

    private void DeliverFoodLoad(GameObject food, Vector3 sPos, float throwAngle)
    {
        Vector3 actualAim = aim;
        actualAim.y = throwAngle;
        actualAim.Normalize();
        // GameObject newFood = Instantiate(currentFood, sPos, Quaternion.Euler(actualAim)) as GameObject;
        currentlyOnStove.transform.position = throwPos.position;
        currentlyOnStove.transform.Rotate(Vector3.right, throwAngle);
        currentlyOnStove.GetComponent<Rigidbody>().AddForce(actualAim * throwForce);
        throwCooldown = 1.5f;
        currentFood = null;
        ResetAim();
        
    }

    private void SetFood(GameObject food)
    {
        isCooking = true;
        canThrow = true;
        currentFood = food;

        GameObject setFood = Instantiate(currentFood, cookPos.transform) as GameObject;
        setFood.transform.position = cookPos.transform.position;
        currentlyOnStove = setFood;

    }

    private void CookFood()
    {

    }
}
