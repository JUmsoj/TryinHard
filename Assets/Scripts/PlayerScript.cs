using NUnit;
using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{

    [SerializeField] private float exp_backdoor;
    public float exp { get { return exp_backdoor; } set { exp_backdoor = value; } }
    public float thing;
    public PlayerControls controls;
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
    public float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        controls = new();
        Inventory = ScriptableObject.CreateInstance<Player>();
    }
    private void OnEnable()
    {
        controls.Main.Enable();
        controls.Main.Move.performed += Check;
        controls.Main.Jump.performed += Jump;
        controls.Main.Inv.performed += NumberPress;
        
       
    }
    private void OnDisable()
    {
        controls.Main.Disable();
        
        
        
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
        time += Time.deltaTime;
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
        
    }
    private IEnumerator Tiring()
    {
        yield return new WaitForSeconds(1);
        cooldown = false;
        yield break;
    }
    void Turn()
    {

        
            Vector3 currentlook = cam.forward;
            currentlook.y = 0;
            Quaternion target = Quaternion.LookRotation(currentlook);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);
            transform.rotation = cam.rotation;
        
    }
    private void Move()
    {




                var temp = 5f/2f;
                move = temp * controls.Main.Move.ReadValue<Vector3>();
               
                
                move = transform.TransformDirection(move);
                cc.Move(Thing(move));
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
    void Check(InputAction.CallbackContext ctx)
    {
        print("Hi World");
        Debug.LogWarning("Hi Fellows");
    }
    Vector3 Thing(Vector3 move)
    {
        move.Normalize();
        return move;
    }
    void Jump(InputAction.CallbackContext ctx)
    {
        float x = 0.54238237823782f;
        jumping = true;
        Vector3 jump;
        if (ctx.ReadValueAsButton())
            jump = new Vector3(0, 2, 0) * x;
        else jump = Vector3.zero;
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
        ref int selection = ref EnemyScript.spawner.hand;
        if (inv.Count > 0)
        {

            selection = Mathf.Clamp(selection, 0, inv.Count - 1);
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
        
        
            
        
        
    }
    void  NumberPress(InputAction.CallbackContext ctx)
    {

        ref int selection = ref EnemyScript.spawner.hand;
        selection = Mathf.Min((int)ctx.ReadValue<float>(), Inventory.INVENTORY.Count-1);
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
}
