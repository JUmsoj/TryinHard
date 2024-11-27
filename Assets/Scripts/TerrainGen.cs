using UnityEngine;
using System;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Linq;
using static UnityEditor.PlayerSettings;
using System.Collections.Generic;
using System.Linq.Expressions;
using OpenCover.Framework.Model;
using UnityEditor.Experimental.GraphView;
using System.Runtime.InteropServices;
public class TerrainGen : MonoBehaviour
{
    public GameObject other;
    Vector3 targetpos;
    
     public GameObject touching;
    public int count;
    public GameObject[] Sectors;
    [SerializeField] int[] nums;
    [SerializeField] char[] s = new char[3];
    [SerializeField] string l;
    // set this tommorow 11/19/24
    public Material[] biomes;
    // 5 biomes and ten different rotations;
    public Quaternion[] rotations = new Quaternion[10];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sort();
        WorldGen(Sectors[UnityEngine.Random.Range(1, Sectors.Length)]);
    }





    int[] Bubble_Sort(int[] numbers)
    {
        bool sorted = true;
        for (int i = 0; i < numbers.Length; i++)
        {
            int index = i;
            int val = numbers[index];
            try
            {

                if (numbers[index + 1] < numbers[index])
                {
                    sorted = false;
                    numbers[index] = numbers[index + 1];
                    numbers[index + 1] = val;


                }
            }
            catch (Exception)
            {

            }
        }
        if (sorted)
        {
            return numbers;
        }
        else
        {
            return Bubble_Sort(numbers);
        }

    }

    void Sort()
    {

        Sectors = new GameObject[gameObject.transform.childCount];
        nums = new int[gameObject.transform.childCount];
        for (int i = 0; i < nums.Length; i++)
        {





            var name = gameObject.transform.GetChild(i).GetComponent<Transform>();
            s = name.gameObject.name[^3..^0].ToCharArray();
            bool thing = false;
            l = "";

            foreach (char c in s)
            {
                if (Char.IsDigit(c) && c != '0' && !thing)
                {
                    thing = true;
                    l += c;
                }
                else if (Char.IsDigit(c) && thing)
                {
                    l += c;
                }
            }
            try
            {
                nums[i] = int.Parse(l);
                print(l);
            }
            catch (Exception)
            {
                continue;
            }
        }
        Bubble_Sort(nums);
       
        for (int i = 0; i < nums.Length; i++)
        {
            gameObject.transform.GetChild(i).gameObject.name = $"Section_{nums[i]}";
        }
        for (int i = 0; i < nums.Length; i++)
        {
            Sectors[i] = gameObject.transform.GetChild(i).gameObject;
            
        }
        // free the memory
    }
   
    void WorldGen(GameObject start)
    {
        touching = start;
        other = gameObject.transform.GetChild(97).gameObject;

        var walker = new GameObject("Walker", typeof(WalkerScript));
        walker.transform.position = touching.transform.position;
        walker.transform.parent = gameObject.transform;
        touching.GetComponent<ProGen>().WalkerOn(walker);
        CreateAndMove(1000!, walker);
        return;
        
        


    }
    void CreateAndMove(int permutations, GameObject walker)
    {
        
        for(int i = 0;i<permutations;i++)
        {
            
            var Visited = walker.GetComponent<WalkerScript>().Visited;
            targetpos = walker.GetComponent<WalkerScript>().SimulateMove(1);
            if (Sec(targetpos, walker) != null)
            {
                walker.transform.position = targetpos;
                Sec(targetpos, walker).GetComponent<ProGen>().WalkerOn(walker);

            }
            else
            {

                targetpos.x *= UnityEngine.Random.Range(-1, 1);
                targetpos.z *= UnityEngine.Random.Range(-1,1);
                if (Sec(targetpos, walker) != null)
                {
                    walker.transform.position = targetpos;
                    Sec(targetpos, walker).GetComponent<ProGen>().WalkerOn(walker);

                }
                else
                {
                    var neighbor = ValidNeighbor(walker.transform.position, walker);
                    if (neighbor != null)
                    {
                        neighbor.GetComponent<ProGen>().WalkerOn(walker);
                        targetpos = neighbor.transform.position;
                        walker.transform.position = targetpos;
                        
                    }
                    else if (Visited.Count() >= 1)
                    {
                        walker.transform.position = Visited[Visited.Count() - 1].transform.position;
                        Visited.Remove(Visited.Last());
                        
                    }

                    
                    continue;
                }
            }
        }
    }
    GameObject ValidNeighbor(Vector3 thing, GameObject walker)
    {
        Vector3 temp = thing;
        var l = walker.GetComponent<Transform>().position;
        Vector3 Left()
        {
            temp.x = l.x + 0;
            temp.z = l.z - 40;
            return temp;
        }
        Vector3 Right()
        {
            temp.x = l.x + 0;
            temp.z = l.z + 40;
            return temp;
        }
        Vector3 Up()
        {
            temp.x = l.x + 80;
            temp.z = l.z + 0;
            return temp;
            
        }
        Vector3 Down()
        {
            temp.x = l.x - 80;
            temp.z = l.z + 0;
            return temp;
        }
        var x = new Func<Vector3>[] {() => Left(), () => Right(), () => Down(), () => Up()};
        var checkedout = new List<Func<Vector3>>();
        while(checkedout.Count < x.Count())
        {
            var select = x[UnityEngine.Random.Range(0, x.Length)];
            if(!checkedout.Contains(select) && Sec(select(), walker) != null)
            {
                return Sec(select(), walker);
            }
            else if(!checkedout.Contains(select))
            {
                checkedout.Add(select);
            }
        }
        return null;
        
        
    }
    GameObject Sec(Vector3 target, GameObject walker)
    {
        foreach (var item in Sectors)
        {
            if (item.transform.position == target  && !walker.GetComponent<WalkerScript>().Visited.Contains(item))
            {
                return item;
            }

        }
        return null;
    }
    void AddToArray(GameObject[] thing, GameObject val)
        {
            for (int i = 0; i < thing.Length; i++)
            {
                if (thing[i] == null)
                {
                    thing[i] = val;
                }
            }
        }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
