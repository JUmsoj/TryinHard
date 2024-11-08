using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    // private string[] attacks;
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
        /*attacks[0] = "Sword";
        attacks[1] = "Sharp" + attacks[0];
        print(attacks[0]);*/
        gameObject.transform.parent = GameObject.Find("Player").transform;
        Collider = gameObject.AddComponent<MeshCollider>();
        Collider.convex = true;
        Collider.sharedMesh = gameObject.GetOrAddComponent<MeshFilter>().mesh;
        rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.isKinematic = true;
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
    private void Turn(string direction)
    {
        if(direction == "h")
        {
            rotate = new (45 * Input.GetAxis("switch") * speed, Quaternion.identity.y, Quaternion.identity.z, Quaternion.identity.w);
            directionx = 5;
            gameObject.transform.rotation = rotate;
            
        }
        else if (direction == "v")
        {
            rotate = new(Quaternion.identity.x, speed * 45 * Input.GetAxis("rotation") , Quaternion.identity.z*speed * 45 * Input.GetAxis("rotation"), Quaternion.identity.w);
            directionz = 5;
            gameObject.transform.rotation = rotate;
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
   
}
