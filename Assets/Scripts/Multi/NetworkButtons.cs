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

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(true);
        chat.SetActive(false);
        Cursor.visible = true;
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
    }
}
