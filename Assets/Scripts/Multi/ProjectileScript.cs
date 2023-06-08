using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float m_Force;
    private Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = transform.forward * m_Force;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
