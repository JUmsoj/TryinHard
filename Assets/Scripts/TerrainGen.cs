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
public class TerrainGen : MonoBehaviour
{
    [SerializeField] GameObject[] Sectors;
    [SerializeField] int[] nums;
    [SerializeField] char[] s = new char[3];
    [SerializeField] string l;
    // set this tommorow 11/19/24
    public Material[] biomes = new Material[5];
    // 5 biomes and ten different rotations;
    public  Quaternion[] rotations = new Quaternion[10];
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
        int pos = UnityEngine.Random.Range(0, Sectors.Length);
        int selection = 0;

        void Up() => selection+=2;
        void Down() => selection -= 2;
        void Right() => selection += 1;
        void Left() => selection -= 1;
        Action[] x = new Action[4] { () => Up(), () => Down(), () => Right(), () => Left() };
        
        
            
        
        for (int i = 0; i < Sectors.Length; i++)
        {

            GameObject[] Neighbors = Sectors[(pos - 2)..(pos + 2)];
            Neighbors[2].GetComponent<ProGen>().ChangeTiles(biomes[0]);
            int index;
            int choice = UnityEngine.Random.Range(0, 4);
            x[choice]();
            index = Array.IndexOf(Neighbors, Neighbors[selection]);
            pos = index;
            
             
            
        }
    }
    void AddToArray(GameObject[] thing, GameObject val)
    {
        for (int i = 0; i < thing.Length; i++)
        {
            if(thing[i] == null)
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
