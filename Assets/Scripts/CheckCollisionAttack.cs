using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionAttack : MonoBehaviour
{
    public bool isColliding = false;
    public Material DebugRed;
    public Material DebugBlue;

    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isColliding = true;
            enemy = collision.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            isColliding = false;
        }
    }

}
