using Unity.VisualScripting.FullSerializer;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Spawner : ScriptableObject
{
    public int hand = 0;
    public int count;
    public int num = 2;
    public GameObject enemy;
    private bool oneatatime = true;
    public List<GameObject> inventory = new(10);
    private void Awake()
    {
        
      
        enemy = Resources.Load<GameObject>("Enemy");
    }
    public void spawn()
    {
        // wave system
        if (oneatatime && count == 0)
        {
            num++;
            oneatatime = false;
            for (int i = 0; i < num; i++)
            {
                try
                {
                    var y = Instantiate(enemy);
                    y.tag = "Enemy";
                }
                catch(ArgumentException)
                {
                    enemy = Resources.Load<GameObject>("Enemy");
                    i--;
                    continue;
                }
            }
            Debug.Log("thing");
            oneatatime=true;
            
        }
        
    }
    
}
