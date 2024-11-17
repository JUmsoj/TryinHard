using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProGen : MonoBehaviour
{
    Vector3 pos;
    bool touchingGround = false;
    [Header("Procedural Generation Parameters")]
    [SerializeField] Vector3 start;
    [Header("Possible values for generation")]
    
    [SerializeField] GameObject[] spawned = new GameObject[10];
    [SerializeField] GameObject[] Tiles;


    [SerializeField] GameObject[] pool = new GameObject[3];
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void UpdateTiles()
    {
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i] = gameObject.transform.GetChild(i).gameObject;
        }
        var player = GameObject.Find("Player");
        start = player.transform.position;
    }
    void Awake()
    {
        Tiles = new GameObject[gameObject.transform.childCount];
        UpdateTiles();  
    }

    void Generate()
    {
        
        for (int i = 0; i < spawned.Length; i++)
        {
            try
            {
                var SelectedTile = Tiles[UnityEngine.Random.Range(0, Tiles.Length - 1)];
                pos = SelectedTile.transform.position;
                GenerateOne(pos, gameObject.transform.rotation, SelectedTile);
            }
            catch (Exception)
            {
                print("No Objects to Instansiate");
            }

            
            
            
        }
    }

    // Update is called once per frame
    void GenerateOne(Vector3 position, Quaternion rotation, GameObject tile)
    {

        if (CheckIfValid(position))
        {
            var x = Instantiate(pool[UnityEngine.Random.Range(0,2)], position, rotation);
            x.transform.parent = gameObject.transform;
            AddToArray(x);
            tile.tag = "Untagged";
        }
        else
        {
            Tiles = GameObject.FindGameObjectsWithTag("Finish");
            var newtile = Tiles[UnityEngine.Random.Range(0, Tiles.Length)];
            pos= newtile.transform.position + start;
            return;
        }
            
           
        
    }

    bool CheckIfValid(Vector3 position)
    {
        foreach (var item in spawned)
        {
            if (item != null && Vector3.Distance(position, item.transform.position) < 15)
            {
                return false;
            }
        }
        return true;
    }
    void AddToArray(GameObject prop)
    {
        for (int i = 0; i < spawned.Length; i++)
        {
            if (spawned[i] != null)
            {
                continue;
            }
            else
            {
                spawned[i] = prop;
                return;
            }
        }
        Destroy(prop);
    }

    void Update()
    {
        UpdateTiles();
        
        Generate();
        if(Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position) > 50)
        {
            foreach(var item in Tiles)
            {
                item.GetComponent<MeshRenderer>().enabled = false;  
                item.GetComponent<MeshCollider>().enabled = false;
               
            }
        }
        else
        {
            foreach (var item in Tiles)
            {
                item.GetComponent<MeshRenderer>().enabled = true;
                item.GetComponent<MeshCollider>().enabled = true;
                
            }
        }
       
    }
}