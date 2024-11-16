using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProGen : MonoBehaviour
{
    bool touchingGround = false;
    [Header("Procedural Generation Parameters")]
    [SerializeField] Vector3 start;
    [Header("Possible values for generation")]
    static GameObject a, b, c;
    [SerializeField] GameObject[] spawned = new GameObject[10];
    [SerializeField] GameObject[] Tiles;


    [SerializeField] GameObject[] pool = new GameObject[3];
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    void Awake()
    {
        Tiles = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i] = gameObject.transform.GetChild(i).gameObject;
        }
        var player = GameObject.Find("Player");
        start = player.transform.position;
    }

    void Generate()
    {
        
        for (int i = 0; i < spawned.Length; i++)
        {
            try 
            {
                var SelectedTile = Tiles[UnityEngine.Random.Range(0, Tiles.Length - 1)];
                var position = SelectedTile.transform.position;



                GenerateOne(position, gameObject.transform.rotation, SelectedTile);
            }

            catch (Exception)
            {
                return;
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
            var Tiles = GameObject.FindGameObjectsWithTag("Finish");
            var newtile = Tiles[UnityEngine.Random.Range(0, Tiles.Length)];
            position = newtile.transform.position + start;
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
        var player = GameObject.Find("Player");
        Generate();
        start = player.transform.position + gameObject.transform.position;
    }
}