using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class HotkeysManager : NetworkBehaviour
{
    private bool m_AlreadyEntered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_AlreadyEntered){
            if(Input.GetKey(KeyCode.C)){
                m_AlreadyEntered = true;
                NetworkManager.Singleton.StartServer();

                Debug.Log("C was pressed");
            }
            if(Input.GetKey(KeyCode.V)){
                m_AlreadyEntered = true;
                NetworkManager.Singleton.StartHost();

                Debug.Log("C was pressed");
            }
            if(Input.GetKey(KeyCode.B)){
                m_AlreadyEntered = true;
                NetworkManager.Singleton.StartClient();

                Debug.Log("C was pressed");
            }
        }
        
    }
}
