using NUnit;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    public Player Inventory;
    public static bool IsInventoryExists = false;
    bool jumping = false;
    bool cooldown= false;
    private CharacterController cc;
    private Vector3 move;
    public static double health = 100;
    public double x;
    float speed = 0.001f;
    Transform trans;
    public Transform cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Inventory = ScriptableObject.CreateInstance<Player>();
    }
    void Start()
    {
        
        Deactivate(GameObject.Find("sword"));
        cam = GameObject.Find("FreeLook Camera").transform;
        speed = UnityEngine.Random.Range((float)0.5, 1); 
        trans = gameObject.GetComponent<Transform>();
        cc = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInventoryExists)
        {
            UpdateInventoryLogic();
        }
        x = health;
        Move();
        if(Input.GetKey(KeyCode.L))
        {
            Dash(cooldown);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private IEnumerator Tiring()
    {
        yield return new WaitForSeconds(1);
        cooldown = false;
        yield break;
    }
    void Turn()
    {
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 currentlook = cam.forward;
            currentlook.y = 0;
            Quaternion target = Quaternion.LookRotation(currentlook);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);
            transform.rotation = cam.rotation;
        }
    }
    private void Move()
    {
        var controls = new PlayerControls();

       
            

                move = speed * controls.Main.Move.ReadValue<Vector3>();
            print(move);
                move = transform.TransformDirection(move);
                cc.Move(move);
                Turn();


            
            
        
        if (!cc.isGrounded && !jumping)
        {
            cc.Move(new Vector3(0, -8.18f, 0));
        }
    }
    void Dash(bool tired)
    {
        if (!tired)
        {
            Vector3 dash = new(0, 0, 5^2);
            dash = transform.TransformDirection(dash);
            
            dash *= speed;
            dash *= Time.deltaTime;
            dash.Normalize();
            for (int i = 0; i < 2; i++)
            {
                cc.Move(dash);
            }
            Turn();
            cooldown = true;
           
            StartCoroutine(Tiring());

        }
    }
    Vector3 Thing(Vector3 move)
    {
        move.Normalize();
        return move;
    }
    void Jump()
    {
        jumping = true;
        Vector3 jump = new (0, 2, 0);
        jump *= Time.deltaTime;
        jump.Normalize();
        for (int i = 0; i < 4; i++)
        {
            cc.Move(jump);
        }
        StartCoroutine(GRAVITY());
        
    }
    private IEnumerator GRAVITY()
    {
        yield return new WaitForSeconds(0.5f);
        jumping = false;
        yield break;
    }
    public static void Deactivate(GameObject target)
    {
        
        
        
            target.SetActive(false);
            target.transform.parent = GameObject.FindGameObjectWithTag("Backpack").transform;
            return;
        
        



    }
    private static void UpdateInventoryLogic()
    {
        var Player = GameObject.Find("Player").GetComponent<PlayerScript>();
        List<GameObject> inv = Player.Inventory.INVENTORY;
        int selection = EnemyScript.spawner.hand;
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

        if (NumberPress() < inv.Count)
        {
            selection = NumberPress();
            EnemyScript.spawner.hand = NumberPress();
        }
        
    }
    static int NumberPress()
    {

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            return 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            return 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            return 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            return 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            return 8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            return 9;
        }
        return EnemyScript.spawner.hand;
    }
        public static void Activate(GameObject target)
        {
            var hand = GameObject.Find("Hand");
            var player = GameObject.Find("Player");
            target.SetActive(true);
            if (target.transform.parent == player.transform.GetChild(1).gameObject)
            {
                target.transform.rotation = hand.transform.rotation;
            }
            target.transform.parent = hand.transform;
            target.transform.position = hand.transform.position;
            return;
       }
    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.name == "Ground")
        {
            jumping = false;
        }
    }
}
