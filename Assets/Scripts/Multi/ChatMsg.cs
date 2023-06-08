using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatMsg : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textField;
    public void SetMessage(string playerName, string msg)
    {
        textField.text = playerName + ": " + msg;
    }
    public void SetColor(Color color)
    {
        textField.color = color;
    }
}
