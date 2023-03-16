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
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make the camera follow the player's back at distance both from the player and from the ground slowly at a dustance
        float dt = Time.deltaTime;
        Vector3 playerPos = player.transform.position;
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

        //move the camera towards the player
        cameraCurrentSpeed = Mathf.Min(cameraCurrentSpeed + cameraAcceleration * dt, cameraMaxSpeed);
        Vector3 cameraToPlayer = playerPos - cameraPos;
        float cameraToPlayerLength = cameraToPlayer.magnitude;
        if (cameraToPlayerLength > cameraDistance)
        {
            cameraToPlayer.Normalize();
            cameraToPlayer *= cameraCurrentSpeed * dt;
            cameraPos += cameraToPlayer;
        }
        else
        {
            cameraCurrentSpeed = 0.0f;
        }

        //set the camera position
        transform.position = cameraPos;

        
        
        

        //look at the player
        transform.LookAt(playerPos);
        

        


        
        

        

        

        //set the player rotation to the camera rotation horizontally only
        player.transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

        //
        
    }
}
