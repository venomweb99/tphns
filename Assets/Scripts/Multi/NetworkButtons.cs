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

    public NetworkVariable<int> seedVal0 = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone);

    public NetworkVariable<int> seedVal1 = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone);

    public NetworkVariable<int> seedVal2 = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone);

    public NetworkVariable<int> seedVal3 = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone);

    public NetworkVariable<int> seedVal4 = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone);

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
       insertSeed(getSeedFromNetList());
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
        //add the seed values to the char array seedNet
        addSeedToNetList(seed);

        insertSeed(getSeedFromNetList());
    }
    public void insertSeed(int[] seedArray){
        GameObject mapManager = GameObject.Find("MapManager");
        mapManager.GetComponent<ChunkGen>().setSeed(seedArray);
    }

    public void addSeedToNetList(int[] seedArray){
        //add the seed values to the char array seedNet
        seedVal0.Value = seedArray[0];
        seedVal1.Value = seedArray[1];
        seedVal2.Value = seedArray[2];
        seedVal3.Value = seedArray[3];
        seedVal4.Value = seedArray[4];  
    }
    public int[] getSeedFromNetList(){
        //read the seed from the string seedNet
        int[] seed = new int[5];
        seed[0] = seedVal0.Value;
        seed[1] = seedVal1.Value;
        seed[2] = seedVal2.Value;
        seed[3] = seedVal3.Value;
        seed[4] = seedVal4.Value;
        
        return seed;
    }
}
