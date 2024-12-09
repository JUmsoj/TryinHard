using Unity.VisualScripting.FullSerializer;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Spawner : ScriptableObject
{
    public int hand = 0;
    public int count;

    public GameObject enemy;
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
                try
                {
                    var y = Instantiate(enemy);
                    y.tag = "Enemy";
                    y.transform.position = new Vector3(spawns[random_number].transform.position.x, 3, spawns[random_number].transform.position.z);
                    y.GetComponent<Rigidbody>().linearVelocity *= long.MaxValue;
                }
                catch (ArgumentException)
                {
                    enemy = Resources.Load<GameObject>("Enemy");
                    i--;
                    continue;
                }
            }
            Debug.Log("thing");
            oneatatime = true;



        }

    }
}
