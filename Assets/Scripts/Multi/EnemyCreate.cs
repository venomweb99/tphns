using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class EnemyCreate : NetworkBehaviour
{ 
    public GameObject enemy;
    private Transform prefabInstance;
    [SerializeField] private Transform prefab;
    //hola
    private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData> (
        new MyCustomData {
            _int = 56,
            _bool = true,
        } , NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner) ;

    public struct MyCustomData : INetworkSerializable {
        public int _int;
        public bool _bool;
        public FixedString128Bytes message;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
            serializer.SerializeValue(ref message);
        }
    }

    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (MyCustomData previousValue, MyCustomData newVaIue) => {
            Debug.Log(OwnerClientId + "; " + newVaIue._int + "; " + newVaIue._bool + newVaIue.message);
        };
    }
    //adios
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

            float targethp = enemy.GetComponent<GetAttacked>().hp;
            Debug.Log("HP: " + targethp);
            if (targethp <= 0)
            {
                prefabInstance.GetComponent<NetworkObject>().Despawn(true);
                Destroy(prefabInstance.gameObject);
            }
    }
}
