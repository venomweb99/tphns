using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirZone : MonoBehaviour
{
    private float distance;
    [SerializeField] float airForce = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "player") {
            distance = (transform.position - other.transform.position).magnitude;
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * (1/distance)*airForce, ForceMode.Acceleration);
            Debug.Log("funciona");
        }
        
    }
}
