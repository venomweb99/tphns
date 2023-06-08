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

    public NetworkVariable<int[]> seedNet = new NetworkVariable<int[]>(
        new int[5], NetworkVariableReadPermission.Everyone);

    public int[] seed;

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
        generateSeed();
        panel.SetActive(false);
        chat.SetActive(true);
        
    }
    
    public void ClientButton()
    {
       NetworkManager.Singleton.StartClient();
       panel.SetActive(false);
       chat.SetActive(true);
       insertSeed(seedNet.Value);
    }

    public void generateSeed(){
        seed = new int[5];
        for (int i = 0; i < 4; i++)
        {
            seed[i] = Random.Range(0, 4);
            for (int j = 0; j < i; j++)
            {
                if (seed[i] == seed[j])
                {
                    i--;
                }

            }
        }
        seedNet.Value = seed;
        insertSeed(seedNet.Value);
    }
    public void insertSeed(int[] seedArray){
        //find MapManager
        GameObject mapManager = GameObject.Find("MapManager");
        if(mapManager == null){
            Debug.Log("MapManager not found");
            return;
        }else{
            Debug.Log("MapManager found, inserting seed" + seedArray[0].ToString() + seedArray[1].ToString() + seedArray[2].ToString() + seedArray[3].ToString() + seedArray[4].ToString() + "");
        }
        //seedArray = new int[5]{1,2,3,4,5};
        mapManager.GetComponent<ChunkGen>().setSeed(seedArray);
        //mapManager.GetComponent<ChunkGen>().GenerateMap();
    }
}
