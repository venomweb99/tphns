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
    public GameObject[] prefabs;
    public GameObject Player;
    public int[] seed;

    public float prefab1size;
    public float prefab2size;
    public float prefab3size;
    public float prefab4size;
    public float prefab5size;
    private float[] sizes;
    private float currentX = 0;
    private bool active = false;

    void Start()
    {
        /*
        //insert all prefabs that are not null into an array
        prefabs = new GameObject[] { prefab1, prefab2, prefab3, prefab4, prefab5 };
        sizes = new float[] { prefab1size, prefab2size, prefab3size, prefab4size, prefab5size };
        

        //intantiate all prefabs and disable them
        for (int i = 0; i < prefabs.Length; i++)
        {
            prefabs[i] = Instantiate(prefabs[i], new Vector3(0, 0, 0), Quaternion.identity);
            prefabs[i].SetActive(false);
        }
        //seed = new int[prefabs.Length];
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(active){
            //if the player is far from a chunk, deactivate it
            for (int i = 0; i < prefabs.Length; i++)
            {
                if (prefabs[i].transform.position.z < Player.transform.position.z - RenderDistance)
                {
                    prefabs[i].SetActive(false);
                }
            }
            //if the player is close to a chunk, activate it
            for (int i = 0; i < prefabs.Length; i++)
            {
                if (prefabs[i].transform.position.z > Player.transform.position.z - RenderDistance && prefabs[i].transform.position.z < Player.transform.position.z + RenderDistance)
                {
                    prefabs[i].SetActive(true);
                }
            }
        }
    
    }

    public void GenerateMap(){
        
        //generate the chunks so they are palced one after the other in the x axis and activate them
        /*
        for (int i = 0; i < seed.Length; i++)
        {
            prefabs[seed[i]].transform.position = new Vector3(0, 0, currentX);
            currentX += sizes[seed[i]];
            prefabs[seed[i]].SetActive(true);
            Debug.Log(seed[i]);
        }*/
        Instantiate(prefab5, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void setSeed(int[] seed)
    {
        this.seed = seed;
        active = true;
        GenerateMap();
    }
}
