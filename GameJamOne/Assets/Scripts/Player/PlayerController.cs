using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour
{
    private Cook sr_cook;
    //private Foods sr_food;


    public XboxController controller;

    public Vector3 aim; //throw angle must be between 0-1
    public Transform throwPos;
    public GameObject cookPos;

    public float aMultiplier;
    public float fMultiplier;
    public int throwPower;
    public float throwCooldown;
    public float throwAngle;
    public float throwForce;
    public float throwCharge = 0;
    public float minCharge;
    public float maxCharge;
    private bool initiateCharge = false;
    public bool charging = false;
    public bool canThrow = false;
    public GameObject currentlyOnStove;
    public GameObject currentFood;

    private void Awake()
    {
        sr_cook = gameObject.GetComponent<Cook>();
        //sr_food = gameObject.GetComponent<Foods>();

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

    void Start()
    {
        minCharge = 0.2f;
        maxCharge = 2.5f;
        throwCooldown = 0;
        throwForce = 425.0f;
        throwAngle = 0.5f;
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



        if (leftInput.x != 0 || leftInput.y != 0)
        {
            aim.x = leftInput.x;
            aim.y = 0;
            aim.z = leftInput.y;
            aim.Normalize();

            Vector3 up = new Vector3(0, 0.2f);

            Debug.DrawRay(this.transform.position + up, aim);
        }

        if ((throwCooldown <= 0 && sr_cook.doneChopping) && 
           ((XCI.GetButtonDown(XboxButton.LeftBumper, controller)) || (XCI.GetButtonDown(XboxButton.RightBumper, controller))))
        {
            if (initiateCharge)
            {

                //ThrowFood(currentFood, throwPos.position, aMultiplier, fMultiplier);
                if (throwCharge <= 0.9f)
                {

                    throwCharge = Mathf.Clamp(throwCharge, 0.55f, 0.9f);
                    Debug.Log(throwCharge);
                    ThrowFood(currentFood, throwPos.position, aMultiplier, fMultiplier * throwCharge);
                   // throwCharge = 0;
                   // initiateCharge = false;
                }
                if (throwCharge > 0.9f && throwCharge <= 1.35f)
                {
                   throwCharge = Mathf.Clamp(throwCharge, 0.9f, 1.3f);
                   Debug.Log(throwCharge);
                   ThrowFood(currentFood, throwPos.position, aMultiplier, fMultiplier * throwCharge);
                }
                
                if (throwCharge > 1.35f)
                {
                   throwCharge = Mathf.Clamp(throwCharge, 1.35f, 1.4f);
                   Debug.Log(throwCharge);
                   ThrowFood(currentFood, throwPos.position, aMultiplier, fMultiplier * throwCharge);
                }
            }

            else
            {
               
                initiateCharge = true;

                if (currentFood != null)
                {
                    canThrow = true;
                }
                
            }
                       
        }

        else if (leftInput.x == 0 || leftInput.y == 0)
        {
            ResetAim();
        }

        if (initiateCharge)
        {
        
           throwCharge += Time.deltaTime;
           //aMultiplier += Time.deltaTime;
           //fMultiplier += Time.deltaTime;
        }
    }

    private void ResetAim()
    {
        aim.x = 0;
        aim.y = 0;
        aim.z = 0;

    }
    

    private void ThrowFood(GameObject food, Vector3 sPos, float throwAngle, float throwForce)
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
            throwAngle = 0;
            throwCharge = 0;
            currentFood = null;
            currentlyOnStove = null;
            initiateCharge = false;
            canThrow = false;
            sr_cook.ToggleDone(false);
            sr_cook.ToggleChopDone(0);
            ResetAim();
            
            //throw sound
        }

        throwCharge = 0;
        initiateCharge = false;
    }
      

}
