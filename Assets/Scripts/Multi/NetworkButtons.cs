using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkButtons : NetworkBehaviour
{
    public GameObject panel;
    public GameObject chat;
    public TextMeshProUGUI m_NumberOfPlayers;
    public NetworkVariable<int> m_PlayersNum = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone);

    public NetworkVariable<int[]> seed = new NetworkVariable<int[]>(
        new int[5], NetworkVariableReadPermission.Everyone);

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(true);
        chat.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_NumberOfPlayers.text = "Players: " + m_PlayersNum.Value.ToString();
        if(!IsServer) return;
        m_PlayersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
        
    }

    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
        panel.SetActive(false);
        chat.SetActive(true);
        
    }
    public void ServerButton()
    {
        NetworkManager.Singleton.StartServer();
        panel.SetActive(false);
        chat.SetActive(true);
    }
    public void ClientButton()
    {
       NetworkManager.Singleton.StartClient();
       panel.SetActive(false);
       chat.SetActive(true);
       insertSeed(seed.Value);
    }

    public void generateSeed(){
        if(!IsServer) return;
        int[] seedArray = new int[5];
        for(int i = 0; i < 5; i++){
            seedArray[i] = Random.Range(0, 4);
        }
        seed.Value = seedArray;
    }
    public void insertSeed(int[] seedArray){
        //find MapManager
        GameObject mapManager = GameObject.Find("MapManager");
        mapManager.GetComponent<ChunkGen>().seed = seedArray;
    }
}
