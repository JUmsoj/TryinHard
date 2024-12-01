using UnityEngine;
using System.Collections.Generic;
using System;
public class WalkerScript : MonoBehaviour
{
    [SerializeField] Vector3 __1__;
    [SerializeField] Vector3 __2__;
    public List<GameObject> Visited = new List<GameObject>();
    int[] Randoms = new int[] {80, 40};
    int[] negatives = new int[2] {1, -1};
    int movex;
    int movey;
    public Vector3 SimulateMove(int negative) 
    {
        if (Randoms[UnityEngine.Random.Range(0, 1)] == 80) movex = 80 * negative * negatives[UnityEngine.Random.Range(0, 1)];
        
        else movey = 40 * negative * negatives[UnityEngine.Random.Range(0, 1)];
        
        return new Vector3(gameObject.transform.position.x + movex,0, gameObject.transform.position.z + movey);
      
        
         
        
        
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        __1__ = new Vector3(0, 0, 320);
        __2__ = new Vector3(-240, 0, -240);
    }

    // Update is called once per frame
    void Update()
    {

       
    }
}
