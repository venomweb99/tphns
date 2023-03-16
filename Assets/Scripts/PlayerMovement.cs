using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variables
    private Vector3 dashForce = new Vector3(0, 0, 7);
    #endregion
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation.Set(0f,0f,0f,0f);
        dash();
    }
    #region METHODS
    void dash()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Rigidbody>().AddForce(dashForce, ForceMode.Impulse);
        }
    }
    //make the player move in any direction
    
    
    #endregion
}
