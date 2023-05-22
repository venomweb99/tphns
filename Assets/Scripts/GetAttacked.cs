using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAttacked : MonoBehaviour
{
    public bool isAttacked = false;
    [SerializeField]
    private GameObject player;
    private Transform childTransform;
    // Start is called before the first frame update
    void Start()
    {
        childTransform = player.transform.GetChild(0);
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
            
            GetComponent<Rigidbody>().AddForce(childTransform.transform.forward * 1f, ForceMode.Impulse);
        }
    }
    #endregion
}
