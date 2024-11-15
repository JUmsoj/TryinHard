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
        

    }

    // Update is called once per frame
    void Update()
    {



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
    public void Hit()
    {
        GameObject Hand = GameObject.Find("Hand");
        gameObject.transform.rotation = Hand.transform.rotation;
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.Translate(new Vector3( 0, 0, Hand.transform.position.z + 5));
        }
       
        StartCoroutine(wait());
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.Translate(0, 0, Hand.transform.position.z -5);
        }


    } 
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(2);

        yield break;
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
        var Player = GameObject.Find("Player").GetComponent<PlayerScript>();
        if(collision.gameObject.CompareTag("Enemy") && Player.Inventory.INVENTORY[0] == gameObject)
        {
            EnemyScript.spawner.count--; 
            Destroy(thing);
        }
        Debug.Log($"Destroyed {thing.name}");
        return;
    }
   
    
    

}
