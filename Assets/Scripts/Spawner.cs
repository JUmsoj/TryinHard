
using Unity.VisualScripting.FullSerializer;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Spawner : ScriptableObject
{
    const int ysize = 3;
    public int hand = 0;
    public int count;

    [SerializeField] private GameObject enemy;
    private bool oneatatime = true;

    private void Awake()
    {


        enemy = Resources.Load<GameObject>("Enemy");
    }
    public void spawn(GameObject[] spawns)
    {
        // wave system
        if (oneatatime && count == 0)
        {
            oneatatime = false;
            int random_number = UnityEngine.Random.Range(0, spawns.Count());
            ref int num = ref spawns[random_number].GetComponent<TerrainGen>().num;
            
            num *=  1+spawns[random_number].GetComponent<TerrainGen>().difficulty;
           
            for (int i = 0; i < num; i++)
            {
               
                   
                    var y = Instantiate(enemy);
                    
                    y.transform.position = new Vector3(spawns[random_number].transform.position.x, 3, spawns[random_number].transform.position.z);
                    
                
                
            }
           
            oneatatime = true;



        }

    }
    public void SpawnInOnePlace(float zlow, float zhigh, float xlow, float xhigh, int times)
    {
        for (int count = 0; count < times; count++) {
            Vector3 selected_one = new Vector3(UnityEngine.Random.Range(xlow, xhigh), 2^3, UnityEngine.Random.Range(zlow, zhigh));
            Instantiate(enemy);

        }
    }
    public void SpawnInOnePlace(int zlow, int zhigh, int xlow, int xhigh, int times)
    {
        for (int count = 0; count < times; count++)
        {
            Vector3 selected_one = new Vector3(UnityEngine.Random.Range(xlow, xhigh), 2^3, UnityEngine.Random.Range(zlow, zhigh));
            Instantiate(enemy);

        }
    }
}
