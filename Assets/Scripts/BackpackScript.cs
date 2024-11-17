using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
public class BackpackScript : MonoBehaviour
{
    public static List<GameObject> container = new(10);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        container.Add(GameObject.Find("sword"));
        container.Add(new GameObject("bow"));
    }
    private void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position) < 15)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                PlayerCapture();
            }
        }
    }
    void PlayerCapture()
    {
        Debug.Log("touched");
        foreach (GameObject child in new List<GameObject>(container))
        {

            var player = GameObject.Find("Player").GetComponent<PlayerScript>();

            player.Inventory.INVENTORY.Add(child);

            PlayerScript.IsInventoryExists = true;

            container.Remove(child);
        }
        Destroy(gameObject);
    }
    
    // Update is called once per frame
    

}
