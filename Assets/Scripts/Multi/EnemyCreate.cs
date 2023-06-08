using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using System;
using Unity.VisualScripting;

public class EnemyCreate : NetworkBehaviour
{ 
    public GameObject enemy;
    private Transform prefabInstance;
    [SerializeField] private Transform prefab;
    private void Start()
    {
        prefabInstance = Instantiate(prefab);
        prefabInstance.GetComponent<NetworkObject>().Spawn(true);
    }
    private void Update()
    {
        //pillar la salud del enemigo
        //si es menor o igual a 0
        //destruir el prefab
        //y updatevo id st

        if(enemy.Health<=0)
        {
            prefabInstance.GetComponent<NetworkObject>().Despawn(true);
        }
    }
}
