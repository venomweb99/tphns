using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraDistance = 10.0f;
    public float groundDistance = 5.0f;
    public float cameraCurrentSpeed = 0.0f;
    public float cameraMaxSpeed = 10.0f;
    public float cameraAcceleration = 2.0f;
    public float cameraTargetAcceleration = 2.0f;
    public GameObject target;
    private GameObject[] players;
    private float deadzone = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayers();
        //check if players is empty
        if (players.Length > 0)
        {
            doCam();
        }
        
    }

    public void FindPlayers(){
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
        //get the position of the players
        Vector3 playersPos = Vector3.zero;
        foreach (GameObject player in players)
        {
            playersPos += player.transform.position;
        }
        playersPos /= players.Length;
        target.transform.position = playersPos;
        }
    }

    void doCam(){
//make the camera follow the player's back at distance both from the player and from the ground slowly at a dustance
        float dt = Time.deltaTime;
        Vector3 playerPos = target.transform.position;
        Vector3 cameraPos = transform.position;

        //cast a ray from the camera to the ground to get the height and set the camera height to the ground height + camera distance unles the player is distance away from the camera
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            //set the camera height to the ground height + camera distance unless the player is distance away from the camera
            float cameraHeight = hit.point.y + groundDistance;
            if (playerPos.y > cameraHeight + groundDistance)
            {
                cameraHeight = playerPos.y;
            }
            cameraPos.y = cameraHeight;

            
        }

        //move the camera towards the target
        cameraCurrentSpeed = Mathf.Min(cameraCurrentSpeed + cameraAcceleration * dt, cameraMaxSpeed);
        Vector3 cameraToPlayer = playerPos - cameraPos;
        float cameraToPlayerLength = cameraToPlayer.magnitude;
        if (cameraToPlayerLength > cameraDistance + deadzone)
        {
            cameraToPlayer.Normalize();
            cameraToPlayer *= cameraCurrentSpeed * dt;
            cameraPos += cameraToPlayer;
        }
        if (cameraToPlayerLength < cameraDistance - deadzone)
        {
            //if the camera is too close it should go back
            cameraCurrentSpeed = Mathf.Max(cameraCurrentSpeed + cameraAcceleration * 2 * dt, 0.0f);
            cameraToPlayer.Normalize();
            cameraToPlayer *= cameraCurrentSpeed * dt;
            cameraPos -= cameraToPlayer;
            

        }

        //set the camera position
        transform.position = cameraPos;
        //look at the player
        transform.LookAt(playerPos);
        

        


        
        

        
        //for each player
        //set the player rotation to the camera rotation horizontally only

        foreach (GameObject player in players)
        {
            player.transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
        }

        //
    }
}
