using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using System;
using Unity.VisualScripting;

public class EnemyCreate : NetworkBehaviour
{ 
    [SerializeField] private Transform prefab;
    private void Start()
    {
        Transform prefabInstance = Instantiate(prefab);
        prefabInstance.GetComponent<NetworkObject>().Spawn(true);
    }
}
