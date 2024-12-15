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
        public Type type { get; set; }
        private GameObject item;
        private GameObject npc;

        public quest_option(Type[] Types)
        {
            types = Types;
            quests = new T[5];
            item = GameObject.Find("Item");
            npc = GameObject.Find("Item");
            type=typeof(T);
            sword = GameObject.FindAnyObjectByType<SwordScript>() ?? GameObject.FindAnyObjectByType<OldManScript>().sword;
            Fill();
        }
        public Type[] types;
        public T[] quests;
        SwordScript sword;
        void Fill() 
        {
            int random = 0;
            
            if (typeof(T) == typeof(Quest<GameObject>))
            {
                for (int i = 0; i < quests.Length; i++)
                {
                    Debug.LogError("thigh");
                    quests[i] = (T)Activator.CreateInstance(types[random], npc, item, 10f);
                }
            }
            else if(typeof(T) == typeof(Quest<float>))
            {
                for (int i = 0; i < quests.Length; i++)
                {
                    Debug.LogError(i); 
                    quests[i] = (T)Activator.CreateInstance(types[random], 3f, sword.kill, 10f);
                }
            }
        } 
        
    }





    private SwordScript sword;
    private quest_option<Quest<float>> fquest_option;
    private quest_option<Quest<GameObject>> fquest_option_object;
    
    private const int questslength = 5;
       
    
    [SerializeField]private PlayerControls controls;
    [SerializeField] private GameObject player;
    [SerializeField] int length;
    private float distance { get; set; }
    [SerializeField]private bowscript bow;
    bool gavequests = false;
    private bool tags;
    
    void Awake()
    {
        
        sword = GameObject.FindAnyObjectByType<SwordScript>();
        tags = gameObject.CompareTag("Grandpa");
        controls = new();
        fquest_option = new(new Type[] { typeof(KillQuest) });
        fquest_option_object = new(new Type[] {typeof(FetchQuest) });
        player = GameObject.FindGameObjectWithTag("Finish");
        bow = GameObject.FindAnyObjectByType<bowscript>();
        if(!!tags)
        {
            
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
            for(int i = 0; i < questslength; i++)
            {
                AddToArray(bow.quest.Quests, i, fquest_option.quests);
                AddToArray(bow.quest.NPCBasedQuests, i, fquest_option_object.quests);
                Debug.LogError("Hey");
            }
        }
    }
    void Special()
    {
        throw new NotImplementedException();
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
