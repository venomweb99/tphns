using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyMiguel : MonoBehaviour
{
    #region public
    public GameObject Player;
    public float speed = 1.0f;
    public float DistaToPlayer = 5.0f;
    #endregion
    #region private
    private Vector3 _target;
    private Vector3 _direction;
    float dista;
    Rigidbody m_Rigidbody;
    #endregion
    void Start()
    {
        print("Start distance to player: " + dista);
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called more than once per frame
    void FixedUpdate()
    {
        print("Distance 2 player: " + dista);
        dista = Vector3.Distance(transform.position, Player.transform.position);
        //check if we input key left control and key k and destroy it
        if (Input.GetKey("k"))
        {
            Destroy(gameObject);
        }
        if (Input.GetKey("f"))
        {
            //we check distance beteween two objects and draw a line between them
            Debug.DrawLine(transform.position, Player.transform.position, Color.red);
        }
        if (dista < DistaToPlayer)
        {
            transform.LookAt(Player.transform.position);
        }
        else
        {
            GetCloser();
        }
    }

    void GetCloser()
    {
        //we will get the direction of the player and move towards it
        _target = Player.transform.position;
        _direction = _target - transform.position;
        transform.position += _direction * speed * Time.deltaTime;
    }
    // if the enemy is in the range, we will draw the live of the enemy
    void LookAtPlayer()
    {
        transform.LookAt(Player.transform.position);
    }
    //we will draw the distance between the player and the enemy
    void OnDrawGizmos()
    {
        GUI.color = Color.black;
        Handles.Label(transform.position - (transform.position - Player.transform.position) / 2, dista.ToString());
    }
}
