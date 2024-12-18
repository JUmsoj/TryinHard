using System;
using System.Linq;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class OldManScript : MonoBehaviour
{
    struct quest_option<T>
    {

        
        public quest_option(Type[] Types)
        {
            types = Types;
            quests = new T[5];
        }
        public Type[] types;
        public T[] quests;
    }
    private quest_option<Quest<float>> fquest_option = new(new Type[] {typeof(KillQuest)});
    private quest_option<Quest<GameObject>> fquest_option_object = new(new Type[] { typeof(FetchQuest) });
    private Type[] gtypes = new Type[] {typeof(FetchQuest), typeof(FollowQuest)};
    private Type[] ftypes;
    private const int questslength = 5;
    Quest<float>[] fQuest = new Quest<float>[questslength];
     Quest<GameObject>[] gQuest = new Quest<GameObject>[questslength];   
    
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
        if(!!tags)
        {
            Normal();
        }
        else
        {
            Special();
        }
        
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
            Debug.LogError("Stuff");
            for(int i = 0; i < questslength; i++)
            {
                AddToArray(bow.quest.Quests, i, fquest_option.quests);
                AddToArray(bow.quest.NPCBasedQuests, i, fquest_option_object.quests);
            }
        }
    }
    void Special()
    {
        throw new NotImplementedException();
    }
    void Normal()
    {
        const int random_num = 0;
        for(int i = 0; i < 5; i++)
        {
            
            
            var item = GameObject.Find("Item");
            var npc = GameObject.Find("NPC");
            fquest_option.quests[i] = (Quest<float>)Activator.CreateInstance(fquest_option.types[random_num], 3f, sword.kill, 5f, SwordScript.onkill);
            fquest_option_object.quests[i] = (Quest<GameObject>)Activator.CreateInstance(fquest_option_object.types[random_num], item, npc, 10f);
            if (fquest_option.quests[i] != null)
            {
                Debug.LogWarning("Hey");
            }
            if (fquest_option_object.quests[i] != null)
            {
                Debug.LogWarning("Hey Twice");
            }
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
