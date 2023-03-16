using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraDistance = 10.0f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make the camera follow the player at distance
        transform.position = player.transform.position - player.transform.forward * cameraDistance;
        //set the player rotation to the camera rotation horizontally only
        player.transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

        //
        
    }
}
