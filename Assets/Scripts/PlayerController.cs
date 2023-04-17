using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public float currentSpeed = 0.0f;
    public float maxSpeed = 10.0f;
    public float acceleration = 1.0f;
    public float dashForce = 20.0f;
    public float pushForce = 20.0f;
    public float pushUpForce = 20.0f;
    public float jumpForce = 10.0f;

    public GameObject attackHitbox;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        //read input from axis
        float AxisMx = Input.GetAxis("Horizontal");
        float AxisMy = Input.GetAxis("Vertical");

        //move the player accelerating it exponentially
        currentSpeed += acceleration * dt;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        transform.Translate(AxisMx * currentSpeed * dt, 0.0f, AxisMy * currentSpeed * dt);
        dash();

        //on jump input jump
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }


        basicAttack();
        debugTests();

    }
    #region
    void dash()
    {
        float AxisMx = Input.GetAxis("Horizontal");
        float AxisMy = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Dash"))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * AxisMy * dashForce, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce(transform.right * AxisMx * dashForce, ForceMode.Impulse);
        }
    }
    
    void basicAttack()
    {
        //check if BasicAttack is being pressed
        if (Input.GetButtonDown("BasicAttack"))
        {
            Debug.Log("BasicAttack");
            //check if the attack hitbox is colliding with an enemy
            if (attackHitbox.GetComponent<CheckCollisionAttack>().isColliding)
            {
                //if it is, push the enemy away
                attackHitbox.GetComponent<CheckCollisionAttack>().enemy.GetComponent<Rigidbody>().AddForce(transform.forward * pushForce, ForceMode.Impulse);
                attackHitbox.GetComponent<CheckCollisionAttack>().enemy.GetComponent<Rigidbody>().AddForce(transform.up * pushUpForce, ForceMode.Impulse);
                //set the material of attackHitbox to DebugRed
                attackHitbox.GetComponent<Renderer>().material = attackHitbox.GetComponent<CheckCollisionAttack>().DebugRed;
         
            }
            

        }


    }

    void debugTests(){
        //raycast to transform forward in red
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        //raycast to transform right in green
        Debug.DrawRay(transform.position, transform.right * 10, Color.green);
    }
    #endregion
}
   
