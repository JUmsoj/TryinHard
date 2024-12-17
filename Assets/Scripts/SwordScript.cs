using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using UnityEngine.WSA;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;
public class SwordScript : MonoBehaviour
{
    // private string[] attacks;

    Quest<float> startquest;
    public QUESTS quest; 
    private int kills { get; set; }
    public int kill
    {
        get
        {
            return kills;
        }
    }

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
    public PlayerControls controls;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = FindFirstObjectByType<Animator>();
        controls = new();
        quest = GameObject.FindAnyObjectByType<bowscript>().quest;
    }
    void OnLeftClick(InputAction.CallbackContext ctx)
    {
        Debug.LogWarning("thing of ");
        if (ctx.interaction is HoldInteraction)
        {
            Throw();
            Debug.LogWarning("Hi");
        }
        else
        {
            anim.SetTrigger("Hit");
            Hit();
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(97.246f, -171.506f, 8.348999f));
        }
    } 
    private void OnEnable()
    {
        controls.Main.Sword.Enable();
        controls.Main.Sword.performed += OnLeftClick;
    }
    private void OnDisable()
    {
        controls.Main.Sword.Disable();  
        controls.Main.Sword.performed -= OnLeftClick;
    }
    void Start()
    {
        
        
        
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
     
        
        
        
        
        
       
    }
    public void Hit()
    {
        
        
       
       


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
        bool isquestthere = startquest != null && quest.Quests[0] != null;
        if (isquestthere )
        {
            startquest = quest.Quests[quest.Quests[0].Firstactivequest];
        }
        ref var enemy_count = ref EnemyScript.spawner.count;
        GameObject thing = collision.gameObject;
        var Player = GameObject.Find("Player").GetComponent<PlayerScript>();
        if(collision.gameObject.CompareTag("Enemy") && Player.Inventory.INVENTORY[0] == gameObject)
        {
            var obj = collision.gameObject.GetComponent<EnemyScript>();
            for (int k = 0; k < 5; k++)
            {
                obj.health -= ((int)Mathf.Ceil(Time.deltaTime) + 5);
            }
            if (obj.health <= 0)
            {
                enemy_count--;
                Destroy(thing);
            }
            if (isquestthere)
            {
                int startquesty = startquest.Firstactivequest;
                if (startquesty != -1)
                {
                    quest.Quests[startquesty].Progress();
                    quest.Quests[startquesty] = null;
                }
            }
            

                    


        }
        Debug.Log($"Destroyed {thing.name}");
        return;
    }
   
    
    

}
