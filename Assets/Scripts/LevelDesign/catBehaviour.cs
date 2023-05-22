using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catBehaviour : MonoBehaviour
{
    #region variables
    [SerializeField] public GameObject projectile;
    public GameObject player;




    #endregion
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        Attack();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    private void Attack()
    {
        Projectile project = Instantiate(projectile, transform);
        project.transform.position = transform.position;
    }
}
