using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConector : MonoBehaviour
{
    public GameObject[] _obj;
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       for (int i=0; i < _obj.Length; i++)
        {
            line.SetPosition(i, _obj[i].transform.position);
        }
    }
}
