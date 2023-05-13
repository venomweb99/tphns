using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    #region VARIBLES
    [SerializeField]
    GameObject[] platforms;
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    bool playerIn = false;
    private int iterator = 0;
    private Vector3 direction;
    private bool movement = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            playerIn = true;
           
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            playerIn = false;
        }
    }
    #region METHODS

    private void Move()
    {
        if (playerIn) { 
            for(int i = 0; i <= 1; i++)
            {
                if (platforms[i].transform.position != transform.position)
                {
                    iterator = i;
                    movement = true;
                    break;
                }
            }
            direction = platforms[iterator].transform.position - transform.position;
            if ((platforms[iterator].transform.position-transform.position).sqrMagnitude>1 && movement)
            {

                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            }
        }
        
    }

    #endregion
}
