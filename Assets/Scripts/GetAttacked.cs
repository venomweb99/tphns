using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAttacked : MonoBehaviour
{

    public bool isAttacked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attacksItself();
     
    }

    #region METHODS
    
    //if the code allows the action, it generates a force that pushes enemy back
    private void attacksItself()
    {
        if (isAttacked)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 1f, ForceMode.Impulse);
        }
    }
    #endregion
}
