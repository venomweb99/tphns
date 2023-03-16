using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraDistance = 10.0f;
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
        //make the camera follow the player at distance slowly
        float dt = Time.deltaTime;
        Vector3 playerPos = player.transform.position;
        Vector3 cameraPos = transform.position;
        Vector3 cameraDirection = playerPos - cameraPos;
        cameraDirection.Normalize();
        cameraCurrentSpeed += cameraAcceleration * dt;
        cameraCurrentSpeed = Mathf.Min(cameraCurrentSpeed, cameraMaxSpeed);
        transform.Translate(cameraDirection * cameraCurrentSpeed * dt);

        //change acceleration based on distance and stop it when it gets close
        float distance = Vector3.Distance(playerPos, cameraPos);
        if (distance > cameraDistance)
        {
            cameraAcceleration = cameraTargetAcceleration;
        }
        else if (distance < cameraDistance)
        {
            cameraAcceleration = -cameraTargetAcceleration;
        }
        else
        {
            cameraAcceleration = 0.0f;
        }
        

        //set the player rotation to the camera rotation horizontally only
        player.transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

        //
        
    }
}
