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
        if (target != GameObject.Find("sword"))
        {
            target.SetActive(false);
            return;
        }
        target.transform.parent = null;
        try
        {
            target.GetComponent<MeshCollider>().enabled = false;
            target.GetComponent<Rigidbody>().detectCollisions = false;
            target.GetComponent<MeshRenderer>().enabled = false;
        }
        catch (Exception)
        {
            target.AddComponent<MeshCollider>().enabled = false;
            target.AddComponent<Rigidbody>().detectCollisions = false;
            target.AddComponent<MeshRenderer>().enabled = false;
        }
    }
    public static void Activate(GameObject target)
    {
        var player = GameObject.Find("Player");
        if (target != GameObject.Find("sword"))
        {
            target.SetActive(false);
            return;
        }
        target.transform.parent = player.transform;
        target.transform.position = player.transform.position;
        // add code to make it near;
        try
        {
            target.GetComponent<MeshCollider>().enabled = true;
            target.GetComponent<Rigidbody>().detectCollisions = true;
            target.GetComponent<MeshRenderer>().enabled = true;
        }
        catch (Exception)
        {
            target.AddComponent<MeshCollider>().enabled = true;
            target.AddComponent<Rigidbody>().detectCollisions = true;
            target.AddComponent<MeshRenderer>().enabled = true;
        }
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
            
           Activate(EnemyScript.spawner.inventory[EnemyScript.spawner.hand]);

                // deactivates everything else;
                if (EnemyScript.spawner.inventory.Count > 1)
                {
                    for (int i = 0; i < EnemyScript.spawner.inventory.Count; i++)
                    {
                        if (EnemyScript.spawner.hand != i)
                        {
                            Deactivate(EnemyScript.spawner.inventory[i]);
                        }
                    }
                }
                // special cases
                if (EnemyScript.spawner.inventory[EnemyScript.spawner.hand] == GameObject.Find("sword"))
                {
                    
                    GameObject.Find("sword").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                }
            
        }
        catch (Exception)
        {
            print("Does Not Exist Yet");
        }
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
