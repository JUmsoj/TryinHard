using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
public class BackpackScript : MonoBehaviour
{
    public PlayerControls controls;
    [SerializeField] private GameObject player;
    public static List<GameObject> container = new(10);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        controls = new();
    }
    void Start()
    {
        
        container.Add(GameObject.Find("sword"));
        container.Add(GameObject.Find("Bow"));
        
    }
    private void OnEnable()
    {
        
            controls.Main.Collect.Enable();
            controls.Main.Collect.performed += PlayerCapture;
           
        
        
    }
    private void OnDisable()
    {
        controls.Main.Collect.Disable();
        controls.Main.Collect.performed -= PlayerCapture;
    }
    void PlayerCapture(InputAction.CallbackContext ctx)
    {
        Debug.LogWarning("thing");
        if (Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position) < 15)
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
    }
    
    // Update is called once per frame
    

}

