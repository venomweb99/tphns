using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    #region VARIABLES

    [SerializeField] GameObject player;
    [SerializeField] float speed = 3f;
    private float height;
    private float gravity;
    [SerializeField] float launchangle;
    private float tanAlpha;
    private Vector3 initialPosition = new Vector3();
    private Vector3 finalPosition = new Vector3();
    private float distX;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region METHODS
    
    private void ThrowTheProjectile()
    {
        initialPosition = transform.position;
        finalPosition = player.transform.position;
        distX = Mathf.Abs(finalPosition.x-initialPosition.x);
        gravity = Physics.gravity.y;
        tanAlpha = Mathf.Tan(launchangle*Mathf.Deg2Rad);
        height = finalPosition.y - initialPosition.y;
        float velocityX = Mathf.Sqrt((gravity * distX * distX) / (2 * (height - distX * tanAlpha)));
        float velocityY = tanAlpha * velocityX;
        this.GetComponent<Rigidbody>().velocity = new Vector3(velocityX, velocityY, 0);


    }
    
    #endregion
}
