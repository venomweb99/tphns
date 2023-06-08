using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAttacked : MonoBehaviour
{
    public bool isAttacked = false;
    [SerializeField]
    private GameObject player;
    private Transform childTransform;
    public float force = 10f;
    public float hp = 100f;
    // Start is called before the first frame update
    void Start()
    {
        childTransform = player.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        attacksItself();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        //if below 0, destroy
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
     
    }

    #region METHODS
    
    //if the code allows the action, it generates a force that pushes enemy back
    private void attacksItself()
    {    
        if (isAttacked)
        {
            hp -= 1f;
            GetComponent<Rigidbody>().AddForce(childTransform.transform.up * force, ForceMode.Impulse);
        }
    }
    #endregion
}
