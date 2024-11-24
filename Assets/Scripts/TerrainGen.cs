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
    public GameObject walker;
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
        WorldGen();
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
   
    void WorldGen()
    {
        touching = gameObject.transform.GetChild(61).gameObject;
        other = gameObject.transform.GetChild(97).gameObject;
        walker = new GameObject("Walker", typeof(WalkerScript));
        walker.transform.position = touching.transform.position;
        walker.transform.parent = gameObject.transform;
        
        for (int i = 0; i <= 1000;i++) 
        {
            targetpos = walker.GetComponent<WalkerScript>().SimulateMove(1);
            
            else
            {
                Debug.LogWarning(targetpos);
                Debug.LogWarning(Sec(targetpos));
                targetpos.x *= -1;
                targetpos.z *= -1;
                if (Sec(targetpos) != null)
                {
                    walker.transform.position = targetpos;
                    Sec(targetpos).GetComponent<ProGen>().WalkerOn();
                }
            }

           
        }
        


    }
    GameObject Sec(Vector3 target)
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
