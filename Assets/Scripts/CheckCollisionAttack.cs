using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckCollisionAttack : MonoBehaviour
{
    public bool isColliding = false;
    public Material DebugRed;
    public Material DebugBlue;
    public GameObject player;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        //if the trigger hitbox is triggering an enemy at the same time press attack, allows to attack
        if (collision.gameObject.tag == "Enemy" && player.GetComponent<PlayerController>().attacking)
        { 
            collision.GetComponent<GetAttacked>().isAttacked = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //once the attack has been done, disables the intern comand to attack
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<GetAttacked>().isAttacked = false;
        }
    }
    

}
