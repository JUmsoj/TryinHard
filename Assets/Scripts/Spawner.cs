using Unity.VisualScripting.FullSerializer;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Spawner : ScriptableObject
{
    public int count;
    public int num = 2;
    public GameObject enemy;
    private bool oneatatime = true;
    private GameObject[] inventory;
    private void Awake()
    {
        inventory = new GameObject[10];
      
        inventory[0] = GameObject.Find("Sword");
        if (inventory[0] != null)
        {
            Debug.Log(inventory[0].name);
        }
        enemy = Resources.Load<GameObject>("Enemy");
    }
    public void spawn()
    {
        // wave system
        if (oneatatime && count == 0)
        {
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
                    Instantiate(enemy).tag = "Enemy";
                }
            }
            Debug.Log("thing");
            oneatatime=true;
            
        }
        
    }
    
}
