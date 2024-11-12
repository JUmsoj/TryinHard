using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class SwordScript : MonoBehaviour
{
    // private string[] attacks;
    
    private static List<GameObject> inv;
    private static int selection;
    private Animator anim;
    readonly int speed = 5;
    Quaternion rotate;
    private int directionx;
    private int directionz;
    public bool threw = false;
    private Vector3 move = new Vector3(-5, 0, 0);
    private Rigidbody rb;
    private MeshCollider Collider;

    // Start is called before the first frame update
   
    void Start()
    {
        anim = GetComponent<Animator>();
        // set this at every Weapon script


        /*
         * 
         * attacks[0] = "Sword";
        attacks[1] = "Sharp" + attacks[0];
        print(attacks[0]);*/
        
        gameObject.transform.parent = GameObject.Find("Player").transform;
        Collider = gameObject.AddComponent<MeshCollider>();
        Collider.convex = true;
        Collider.sharedMesh = gameObject.GetOrAddComponent<MeshFilter>().mesh;
        rb = gameObject.AddComponent<Rigidbody>();
        
        rb.isKinematic = true;
        print($"{gameObject.name}Object Created");
        Deactivate(gameObject);

    }

    // Update is called once per frame
    void Update()
    {

        UpdateInventoryLogic();
        
        if (threw)
        {
            gameObject.transform.Translate(move);
        }
     
        
        if (Input.GetKeyDown(KeyCode.T) && !threw)
        {
            print("ht");
            Throw();
            threw = true;
        }
        if (Input.GetMouseButton(0))
        {
            anim.SetTrigger("Hit");
        }
       
        if(Input.GetAxis("turn") != 0)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 90 * Input.GetAxis("turn")));
            print($"Turned {90 * Input.GetAxis("turn")} Degrees");
        }
        
       
    }
    public static void Deactivate(GameObject target)
    {
        GameObject hand = GameObject.Find("Hand");
        var sword = GameObject.Find("sword");
        if (target != sword)
        {
            target.SetActive(false);
            target.transform.parent = null;
            return;
        }
        else
        {
            sword.transform.parent = null;
            sword.GetComponent<MeshCollider>().enabled = false;
            sword.GetComponent<Rigidbody>().detectCollisions = false;
            sword.GetComponent<MeshRenderer>().enabled = false;
        }
        
        
        
    }
    public static void Activate(GameObject target)
    {
        var hand = GameObject.Find("Hand");
        var player = GameObject.Find("Player");
        var sword = GameObject.Find("sword");

        if (target != sword)
        {
            sword = null;
            target.SetActive(true);
            target.transform.parent = hand.transform;
            target.transform.position = hand.transform.position;
            return;
        }
        else
        {
            
            if (sword.transform.parent != hand.transform) // if sword is newly being activated then rotation will be reset
            {
                sword.transform.rotation = hand.transform.rotation;
            }
            sword.transform.parent = hand.transform;
            sword.transform.position = hand.transform.position;

            // add code to make it near;

            sword.GetComponent<MeshCollider>().enabled = true;
            sword.GetComponent<Rigidbody>().detectCollisions = true;
            sword.GetComponent<MeshRenderer>().enabled = true;
        }
        // sword exception
        
        
        
        
        
    }
   
    private int Throw()
    {
        gameObject.transform.parent = null;
        move = new(directionx^2, 0, directionz^2);

        print($"X:{move.x}, Y:{move.y}, Z:{move.z}");
        gameObject.transform.Translate(move);
        
        Debug.Log("threw");
        return 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        GameObject thing = collision.gameObject;
        
        if(collision.gameObject.CompareTag("Enemy") && EnemyScript.spawner.inventory[0] == gameObject)
        {
            EnemyScript.spawner.count--; 
            Destroy(thing);
        }
        Debug.Log($"Destroyed {thing.name}");
        return;
    }
    private static void UpdateInventoryLogic()
    {
        try
        {
            inv = EnemyScript.spawner.inventory;
            selection = EnemyScript.spawner.hand;
            GameObject player = GameObject.Find("Player");
            Activate(inv[selection]);

                // deactivates everything else;
                if (inv.Count > 1)
                {
                    for (int i = 0; i < inv.Count; i++)
                    {
                        if (selection != i)
                        {
                            Deactivate(inv[i]);
                        }
                    }
                }
                // special cases
                if (inv[selection] == player)
                {
                    
                    player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                }
            
        }
        catch (Exception)
        {
            print("Does Not Exist Yet");
        }
        selection = NumberPress();
        EnemyScript.spawner.hand = NumberPress();
    }
    static int NumberPress()
    {
      
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            return 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 3;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 4;
        }
        if(Input.GetKeyDown (KeyCode.Alpha5))
        {
            return 5;
        }
        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            return 6;
        }
        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            return 7;
        }
        if(Input.GetKeyDown(KeyCode.Alpha8)) {
            return 8;
        }
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            return 9;
        }
        return EnemyScript.spawner.hand;
    }

}
