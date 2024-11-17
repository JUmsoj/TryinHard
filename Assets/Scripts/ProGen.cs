using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProGen : MonoBehaviour
{
    private Mesh mesh;
    Vector3 pos;
    bool touchingGround = false;
    [Header("Procedural Generation Parameters")]
    [SerializeField] Vector3 start;
    [Header("Possible values for generation")]
    [SerializeField] CombineInstance[] meshes;
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
            if (pool[0] != null && pool[1] != null && pool[2] != null)
            {
                var SelectedTile = Tiles[UnityEngine.Random.Range(0, Tiles.Length - 1)];
                pos = SelectedTile.transform.position;
                if (CheckIfValid(pos))
                {
                    GenerateOne(pos, pool[UnityEngine.Random.Range(0, 2)], SelectedTile);
                }
                else
                {
                    Randomize();
                    UpdateTiles();
                }
            }
            
            
            

            
            
            
        }
    }

    // Update is called once per frame
    void GenerateOne(Vector3 position, GameObject prop, GameObject tile)
    {

        
        var x = Instantiate(prop, position, gameObject.transform.rotation, gameObject.transform);
        x.transform.parent = gameObject.transform;
        AddToArray(x);
        tile.tag = "Untagged";
        
       
            
           
        
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
    
    void Randomize()
    {
        var newtile = Tiles[UnityEngine.Random.Range(0, Tiles.Length)];
        pos = newtile.transform.position + start;
        return;
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
    // if it ever lags call this function
    void FixLag()
    {
        // new array
        var x = new int[] {5, 3, 2, 1, 4, 6, 8, 2, 1, 5, 7};
        
        CombineInstance[] children = new CombineInstance[Tiles.Length];
        for(int i = 0; i < children.Length; i++)
        {
            children[i].mesh = Tiles[i].GetComponent<MeshFilter>().mesh;
            children[i].transform = Tiles[i].transform.localToWorldMatrix;
        }
        var mesh = gameObject.AddComponent<MeshFilter>();
        mesh.mesh = new Mesh();
        mesh.mesh.CombineMeshes(children, true);

        

        return;
    }
    void Update()
    {
        UpdateTiles();
        
        Generate();
        ChunkRender();
        
       
    }
    void ChunkRender()
    {
        if (Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position) > 50)
        {
            foreach (var item in Tiles)
            {
                
                item.GetComponent<MeshRenderer>().enabled = false;
                
            }
            
        }
        else
        {
            foreach (var item in Tiles)
            {
               
                item.GetComponent<MeshRenderer>().enabled = true;

            }
        }
    }
}