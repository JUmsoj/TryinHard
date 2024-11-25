using UnityEngine;
using System.Collections.Generic;
public class WalkerScript : MonoBehaviour
{
    [SerializeField] Vector3 __1__;
    [SerializeField] Vector3 __2__;
    public List<GameObject> Visited = new List<GameObject>();
    int[] Randoms = new int[] {-80, 80,0, -40, 40,0};
    public Vector3 SimulateMove(int negative) 
    {
        int movex = Randoms[UnityEngine.Random.Range(0, 2)] * negative;
        int movey = Randoms[UnityEngine.Random.Range(3, 5)] * negative;
        
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
