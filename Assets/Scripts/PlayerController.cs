using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float currentSpeed = 0.0f;
    public float maxSpeed = 10.0f;
    public float acceleration = 1.0f;
    private Vector3 dashForce = new Vector3(0, 0, 7);
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
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
        



        
    }
    void dash()
    {
        if (Input.GetButtonDown("Dash"))
        {
            GetComponent<Rigidbody>().AddForce(dashForce, ForceMode.Impulse);
        }
    }
}
