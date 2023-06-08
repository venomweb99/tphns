using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using System;

public class EnemyCreate : NetworkBehaviour
{
    public GameObject PrefabToSpawn;
    public bool DestroyWithSpawner;
    private GameObject m_PrefabInstance;
    private NetworkObject m_SpawnedNetworkObject;


    // Start is called before the first frame update
    private void Start()
    {
        // Instantiate our instance when we start (for both clients and server)
        m_PrefabInstance = Instantiate(PrefabToSpawn);

        // Get the NetworkObject component assigned to the Prefab instance
        m_SpawnedNetworkObject = m_PrefabInstance.GetComponent<NetworkObject>();

        // Set it to be inactive
        m_PrefabInstance.SetActive(false);
    }

    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(2);
        m_SpawnedNetworkObject.Despawn();
        StartCoroutine(SpawnTimer());
        yield break;
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(2);
        SpawnInstance();
        yield break;
    }
    public void Destroy(NetworkObject networkObject)
    {
        m_PrefabInstance.SetActive(false);
    }

    public void SpawnInstance()
    {
        if (!IsServer)
        {
            return;
        }

        if (m_PrefabInstance != null && m_SpawnedNetworkObject != null && !m_SpawnedNetworkObject.IsSpawned)
        {
            m_PrefabInstance.SetActive(true);
            m_SpawnedNetworkObject.Spawn();
            StartCoroutine(DespawnTimer());
        }
    }
    public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
    {
        m_PrefabInstance.SetActive(true);
        m_PrefabInstance.transform.position = transform.position;
        m_PrefabInstance.transform.rotation = transform.rotation;
        return m_SpawnedNetworkObject;
    }

    public void OnNetworkSpawn()
    {
        // Only the server spawns, clients will disable this component on their side
        enabled = IsServer;
        if (!enabled || PrefabToSpawn == null)
        {
            return;
        }
        // Instantiate the GameObject Instance
        m_PrefabInstance = Instantiate(PrefabToSpawn);

        // Optional, this example applies the spawner's position and rotation to the new instance
        m_PrefabInstance.transform.position = transform.position;
        m_PrefabInstance.transform.rotation = transform.rotation;

        // Get the instance's NetworkObject and Spawn
        m_SpawnedNetworkObject = m_PrefabInstance.GetComponent<NetworkObject>();
        m_SpawnedNetworkObject.Spawn();
    }

    public void OnNetworkDespawn()
    {
        if (IsServer && DestroyWithSpawner && m_SpawnedNetworkObject != null && m_SpawnedNetworkObject.IsSpawned)
        {
            m_SpawnedNetworkObject.Despawn();
        }
        base.OnNetworkDespawn();
    }
}
