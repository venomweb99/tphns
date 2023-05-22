using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOpen : MonoBehaviour
{
    public GameObject Trapdoor;
    private void OnTriggerEnter(Collider other)
    {
        Trapdoor.GetComponent<Animation>().Play("TrapDoorAnim");
    }
}
