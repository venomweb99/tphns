using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGen : MonoBehaviour
{
    public int length = 10;
    public float RenderDistance = 30;
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;
    public GameObject prefab5;

    public GameObject gachaPrefab;

    public int likelyhood1 = 100;
    public int likelyhood2 = 0;
    public int likelyhood3 = 0;
    public int likelyhood4 = 0;
    public int likelyhood5 = 0;
    //private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        likelyhood1 = 100 - likelyhood2 - likelyhood3 - likelyhood4 - likelyhood5;
        likelyhood2 = 100 - likelyhood1 - likelyhood3 - likelyhood4 - likelyhood5;
        likelyhood3 = 100 - likelyhood1 - likelyhood2 - likelyhood4 - likelyhood5;
        likelyhood4 = 100 - likelyhood1 - likelyhood2 - likelyhood3 - likelyhood5;
        likelyhood5 = 100 - likelyhood1 - likelyhood2 - likelyhood3 - likelyhood4;

        //find any objects with tag "chunk"
        GameObject[] chunk = GameObject.FindGameObjectsWithTag("Chunk");
        //check if there are more castles than the length
        if (chunk.Length > length)
        {
            Destroy(gameObject);
            Instantiate(gachaPrefab, transform.position + new Vector3(5,0,0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - Camera.main.transform.position.x < RenderDistance)
        {
            int random = Random.Range(0, 100);
            if(random < likelyhood1)
            {
                Instantiate(prefab1, transform.position + new Vector3(5,0,0), Quaternion.identity);
                //make the spawner destroy itself
                Destroy(gameObject);
            }
            else if(random < likelyhood1 + likelyhood2)
            {
                Instantiate(prefab2, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (random < likelyhood1 + likelyhood2 + likelyhood3)
            {
                Instantiate(prefab3, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (random < likelyhood1 + likelyhood2 + likelyhood3 + likelyhood4)
            {
                Instantiate(prefab4, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if (random <= likelyhood1 + likelyhood2 + likelyhood3 + likelyhood4 + likelyhood5)
            {
                Instantiate(prefab5, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }
    }
}
