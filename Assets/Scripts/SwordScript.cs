using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public bool threw = false;
    private Vector3 move = new Vector3(-5, 0, 0);
    private Rigidbody rb;
    private MeshCollider Collider;
    // Start is called before the first frame update
    void Start()
    {
        
        Collider = gameObject.AddComponent<MeshCollider>();
        Collider.convex = true;
        Collider.sharedMesh = gameObject.GetOrAddComponent<MeshFilter>().mesh;
        rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        print($"{gameObject.name}Object Created");
    }

    // Update is called once per frame
    void Update()
    {
        if(threw)
        {
            gameObject.transform.Translate(move);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        { 
            move.x*=Input.GetAxis("switch");
            transform.Translate(move);
        }
        if (Input.GetKeyDown(KeyCode.A) && !threw)
        {
            print("ht");
            Throw();
            threw = true;
        }
    }
    private void Throw()
    {
       
        move = transform.forward;
        move.y = 0;

        move.x *= 500000000000/400000000*-1;
        move.z *= 5000000000000/40000000*-1;
        print($"X:{move.x}, Y:{move.y}, Z:{move.z}");
        gameObject.transform.rotation = new Quaternion(180, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w);
        gameObject.transform.Translate(move);
        
        
        Debug.Log("threw");
        return;
    }
   
}
