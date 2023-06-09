using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using System;
using Unity.VisualScripting;

public class GetAttacked : MonoBehaviour
{
    public bool isAttacked = false;
    [SerializeField] private GameObject player;
    private Transform childTransform;
    private float force = 0.1f;
    public float hp = 100f;
    // Start is called before the first frame update
    void Start()
    {
        childTransform = player.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        attacksItself();
        //if below 0, destroy
        if (transform.position.y < -10)
        {
            hp = 0;
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
     
    }

    #region METHODS
    
    //if the code allows the action, it generates a force that pushes enemy back
    private void attacksItself()
    {    
        if (isAttacked)
        {
            hp -= 1f;
            GetComponent<Rigidbody>().AddForce(childTransform.transform.up * force, ForceMode.Impulse);
        }
    }
    #endregion
}
