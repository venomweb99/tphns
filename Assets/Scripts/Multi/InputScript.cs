using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    private string inputString;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void readStringInput(string s)
    {
        inputString = s;
        Debug.Log("String is: " + inputString);
    }
}
