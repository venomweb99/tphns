using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //face the input direction according to the camera
        float AxisMx = Input.GetAxis("Horizontal");
        float AxisMy = Input.GetAxis("Vertical");
        
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 inputDirection = cameraForward * AxisMy + cameraRight * AxisMx;
        inputDirection.Normalize();
        if (inputDirection.magnitude > 0.0f)
        {
            transform.forward = inputDirection;
        }

    }
}
