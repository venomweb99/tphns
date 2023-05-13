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

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
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
            if (waypointIndex == 3) {
                waypointIndex = 0;
            }else waypointIndex++;
        }
        Debug.Log("Waypoint: " + waypointIndex);
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
 