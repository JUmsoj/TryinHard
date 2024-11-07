using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
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
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Turn("h");
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Turn("v");
        }
        if (Input.GetKeyDown(KeyCode.A) && !threw)
        {
            print("ht");
            Throw();
            threw = true;
        }
    }
    void Turn(string direction)
    {
        if(direction == "h")
        {
            rotate = new (45 * Input.GetAxis("switch") * speed, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w);
            directionx = 5;
            gameObject.transform.rotation = rotate;
            
        }
        else if (direction == "v")
        {
            rotate = new(Quaternion.identity.x, Quaternion.identity.y, speed * 45 * Input.GetAxis("rotation"), Quaternion.identity.w);
            directionz = 5;
            gameObject.transform.rotation = rotate;
        }
    }
    private void Throw()
    {

        move = new(directionx, 0, directionz);
        move.y = 0;

        print($"X:{move.x}, Y:{move.y}, Z:{move.z}");
        gameObject.transform.Translate(move);
        
        
        Debug.Log("threw");
        return;
    }
   
}
