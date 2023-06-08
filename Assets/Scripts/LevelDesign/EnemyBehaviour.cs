using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] float speed = 5f;
    [SerializeField] private int waypointIndex = 0;
    [SerializeField] GameObject player;
    private Vector3 direction;
    private int state = 0;
    private int waypointSize;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        waypointSize = waypoints.Length;
    }   

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                Move();
                break;
            case 1:
                Chase();
                break;
        }
        
        
    }

    #region METHODS

    private void Move()
    {
        if((transform.position-player.transform.position).sqrMagnitude < 100)
        {
            state = 1;
        }
        else { state = 0; }
        direction = waypoints[waypointIndex].transform.position- transform.position;
        transform.LookAt(waypoints[waypointIndex].transform.position);
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if ((transform.position - waypoints[waypointIndex].transform.position).sqrMagnitude<3.5)
        {
            int RandomNumber = UnityEngine.Random.Range(-1, 2);
            if(RandomNumber == 0)
            {
                RandomNumber = 1;
            }
            if(RandomNumber + waypointIndex < 0)
            {
                waypointIndex = waypointSize - 1;
            }
            else if(RandomNumber + waypointIndex > waypointSize-1)
            {
                waypointIndex = 0;
            }
            else waypointIndex += RandomNumber;
            if (waypointIndex >= waypointSize-1) {
                waypointIndex = 0;
            }else waypointIndex+= RandomNumber;
        }
        //Debug.Log("Waypoint: " + waypointIndex);
    }

    private void Chase()
    {
        if((player.transform.position-transform.position).sqrMagnitude > 100)
        {
            state = 0;
        }
        direction = player.transform.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
    
    #endregion
}
 