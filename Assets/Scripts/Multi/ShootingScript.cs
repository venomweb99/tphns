using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject m_Missile;
    public Transform m_SpawnPoint;

    public List<GameObject> m_Missiles = new List<GameObject>();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newMissile = Instantiate(m_Missile, m_SpawnPoint.position, m_SpawnPoint.rotation);
        }
    }
}
