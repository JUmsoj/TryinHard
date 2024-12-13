using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class OldManScript : MonoBehaviour
{
    
    private const int questslength = 5;
    Quest<float>[] fQuest = new Quest<float>[questslength];
     Quest<GameObject>[] gQuest = new Quest<GameObject>[questslength];   
    public Quest<float>[] quest { get { return fQuest; } set { fQuest = value; } }
    public Quest<GameObject>[] gquest {  get { return gQuest; } set { gQuest = value; } }
    [SerializeField]private PlayerControls controls;
    [SerializeField] private GameObject player;
    [SerializeField] int length;
    private float distance { get; set; }
    [SerializeField]private bowscript bow;
    bool gavequests = false;
    private bool tags;
    private SwordScript sword;
    void Awake()
    {
        sword = GameObject.FindAnyObjectByType<SwordScript>();
        tags = gameObject.CompareTag("Grandpa");
        controls = new();
       
        player = GameObject.FindGameObjectWithTag("Finish");
        bow = GameObject.FindAnyObjectByType<bowscript>();
        if(tags)
        {
            Normal();
        }
        length = fQuest.Count();
    }
    private void OnEnable()
    {
        controls.Main.Talk.Enable();
        controls.Main.Talk.performed += Talk_performed;
    }
    private void OnDisable()
    {
        controls.Main.Talk.Disable();
        controls.Main.Talk.performed -= Talk_performed;
    }
    private void Talk_performed(InputAction.CallbackContext obj)
    {
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        Debug.LogWarning("Pressed");
        if (distance < 10 && !gavequests && tags)
        {
            gavequests = true;
            bow.StartingQuests();
            Debug.LogWarning("HEY");
        }
        else if (distance < 10 && gavequests)
        {
            for(int i = 0; i < questslength; i++)
            {
                AddToArray(bow.quest.Quests, i, fQuest);
                AddToArray(bow.quest.NPCBasedQuests, i, gQuest);
            }
        }
    }
    void Special()
    {
        throw new NotImplementedException();
    }
    void Normal()
    {
        for(int i = 0; i < 5; i++)
        {
            var item = GameObject.Find("Item");
            var npc = GameObject.Find("NPC");
            fQuest[i] = new KillQuest(5f, sword.kill, 5f);
            gQuest[i] = new FetchQuest(item, npc, 10f);
            
        }

    }
    void AddToArray<T>(T[] array, T obj)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null) { 
                array[i] = obj; 
                return; 
            }
        }
        array[array.Length-1] = obj;
    }
    void AddToArray<T>(T[] array, int obj, T[] thing_taken_out_of)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
            {
                T objects = thing_taken_out_of[obj];
                thing_taken_out_of[obj] = default;
                array[i] = objects;
                
                length--;
                Debug.Log(length);
                return;
            }
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
