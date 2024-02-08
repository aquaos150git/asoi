using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_GraphSpawn : MonoBehaviour
{
    private void Start()
    {
        B_SpawnRandomGraphs();
    }
    public GameObject[] B_GraphSpawner;
    public Transform B_SpawnArea;
    public void B_SpawnRandomGraphs()
    {
        int randomIndex = Random.Range(0, B_GraphSpawner.Length);
        Instantiate(B_GraphSpawner[randomIndex],B_SpawnArea.position,Quaternion.identity);
    }
        

    
}
