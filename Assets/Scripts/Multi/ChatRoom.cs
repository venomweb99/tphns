using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ChatRoom : NetworkBehaviour
{
    [SerializeField] private ChatMsg chatMsg;
    [SerializeField] private Transform chatMsgContainer;
    [SerializeField] private TMP_InputField chatInputField;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject chatWindow;

    private const int maxMessages = 25;
    private List<ChatMsg> _messages;
    public Color playerMessageColor = Color.red;
    
    // Start is called before the first frame update
    void Start()
    {
        _messages = new List<ChatMsg>();
    }

    // Update is called once per frame
    void Update()
    {
        
                if (Input.GetKeyDown(KeyCode.Return)){
                    if(nameInputField.text.Length > 0){
                        if(chatInputField.text.Length > 0)
                        {
                            SendChatMessage();

                        }else{
                            chatInputField.Select();
                            chatInputField.ActivateInputField();
                        }
                        nameInputField.interactable = false;
                    }
                }
    }
    public void SendChatMessage(){
        string msg = chatInputField.text;
        chatInputField.text = "";

        if(string.IsNullOrWhiteSpace(msg)){
            return;
        }
        AddMessage(nameInputField.text, msg);
        RecieveChatMessageServerRpc(nameInputField.text, msg);
    }
    private void AddMessage(string playerName, string msg)
    {
        if(_messages.Count >= maxMessages){
            Destroy(_messages[0].gameObject);
            _messages.RemoveAt(0);
        }
        ChatMsg newMsg = Instantiate(chatMsg, chatMsgContainer);
        newMsg.SetMessage(playerName, msg);
        if(playerName == nameInputField.text){
            newMsg.SetColor(playerMessageColor);
        }
        _messages.Add(newMsg);
        //call scroll to bottom
        chatWindow.GetComponent<scroller>().ScrollToBottom();
        
    }
    [ClientRpc]
    private void RecieveChatMessageClientRpc(string playerName, string msg)
    {
        AddMessage(playerName, msg);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RecieveChatMessageServerRpc(string playerName, string msg)
    {
        RecieveChatMessageClientRpc(playerName, msg);
    }
}
