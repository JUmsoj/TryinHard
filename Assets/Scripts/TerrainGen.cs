using UnityEngine;
using System;
using Microsoft.VisualBasic;
using System.Reflection;
public class TerrainGen : MonoBehaviour
{
    [SerializeField] GameObject[] Sectors;
    [SerializeField] int[] nums;
    [SerializeField] char[] s = new char[3];
    [SerializeField] string l;
    // set this tommorow 11/19/24
    static Material[] biomes;
    // 5 biomes and ten different rotations;
    static Quaternion[] rotations;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(biomes == null) { 
            biomes = new Material[5]; 
        }
        if (rotations == null)
        {

            rotations = new Quaternion[10];

        }
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
        for(int i = 0; i < nums.Length; i++)
        {
            Sectors[i] = gameObject.transform.GetChild(i).gameObject;
        }
        // free the memory
        





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
    }
    void WorldGen()
    {
       
    }
        // Update is called once per frame
   void Update()
   {

   }
    
}
