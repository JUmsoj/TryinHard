using Unity.VisualScripting.FullSerializer;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : ScriptableObject
{
    public int num = 5;
    public GameObject enemy;
    private bool oneatatime = true;
    private void Awake()
    {
        enemy = Resources.Load<GameObject>("Enemy");
    }
    public void spawn()
    {
        if (oneatatime)
        {
            oneatatime = false;
            for (int i = 0; i < num; i++)
            {
                var y = Instantiate(enemy);
                y.tag = "Enemy";
            }
            Debug.Log("thing");
            oneatatime=true;
            
        }
        
    }
    
}
