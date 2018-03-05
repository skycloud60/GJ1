using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {

    public XboxController controller;
    public Vector3 aim;
 
    public float throwCooldown;
    public bool canThrow = false;

    //throw angle must be between 0-1
    public float throwAngle;
    public float throwForce;
    public float throwCharge = 0;
    public int throwPower;
    public bool charging = false;

    public Transform spawnPos;
    public GameObject currentFood;
    

	// Use this for initialization
	void Start () {

        throwCooldown = 0;
        throwForce = 750.0f;
    
            
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
                canThrow = true;
            }

            if (throwCooldown <= 0 && ((XCI.GetButton(XboxButton.LeftBumper, controller)) || (XCI.GetButton(XboxButton.RightBumper, controller))))
            {
                if (throwCharge <= 0.5)
                {
                    throwAngle = 0.1f;
                    DeliverFoodLoad(currentFood, spawnPos.position, 0.1f);
                    Debug.Log("shot charge 1!");
                }

                if (throwCharge > 1)
                {
                    DeliverFoodLoad(currentFood, spawnPos.position, 0.3f);
                    Debug.Log("shot charge 2!");
                }

                if (throwCharge > 2.5f)
                {
                    DeliverFoodLoad(currentFood, spawnPos.position, 0.5f);
                    Debug.Log("overthrown!");
                }

            }

            /*
            //--------------------------------------------------
            // RIGHT ANALOG STICK INPUT
            //--------------------------------------------------

            Vector2 rightInput = new Vector2(XCI.GetAxisRaw(XboxAxis.RightStickX, controller),
                                             XCI.GetAxisRaw(XboxAxis.RightStickY, controller));

            if (rightInput.x != 0 || rightInput.y != 0)
            {
                aim.x = rightInput.x;
                aim.y = 0;
                aim.z = rightInput.y;

                aim.Normalize();

                Vector3 up = new Vector3(0, 0.2f);

                Debug.DrawRay(this.transform.position + up, aim);

                throwCooldown -= Time.deltaTime;
                if (throwCooldown <= canThrow)
                {
                    DeliverFoodLoad(currentFood, spawnPos.position);
                }
            }
            */
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
        GameObject newFood = Instantiate(food, sPos, Quaternion.Euler(actualAim)) as GameObject;
        newFood.transform.Rotate(Vector3.right, throwAngle);
        newFood.GetComponent<Rigidbody>().AddForce(actualAim * throwForce);
        throwCooldown = 1.5f;
        ResetAim();
    }
}
