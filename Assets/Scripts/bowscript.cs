using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System;
using System.Net;
using System.Collections.Generic;

using System.Threading.Tasks.Sources;
using JetBrains.Annotations;
using System.Linq;
public class QUESTS : ScriptableObject
{
    public Quest<float>[] Quests { get; set; } = new Quest<float>[5];
    public Quest<GameObject>[] NPCBasedQuests { get; set; } = new Quest<GameObject>[5];

}

public class bowscript : MonoBehaviour
{
    public QUESTS quest;
    public float stuff;
    private GameObject parent;
    private PlayerControls controls;
    private Rigidbody rb;
    private void Awake()
    {
        controls = new();

        quest = ScriptableObject.CreateInstance<QUESTS>();
        StartingQuests();

    }
    void StartingQuests()
    {
        quest.Quests[0] = new KillQuest(start: GameObject.Find("sword").GetComponent<SwordScript>().kill, goal: 3f, exp: 5f);
        quest.NPCBasedQuests[0] = new FetchQuest(GameObject.Find("Item"), GameObject.Find("NPC"), exp: 10f);
    }
    private void OnEnable()
    {
        
        controls.Main.Bow.Enable();
        rb = GetComponent<Rigidbody>();
        controls.Main.Bow.performed += Shoot;
        
        
    }
    private void OnDisable()
    {
        controls.Main.Bow.Disable();
        controls.Main.Bow.performed -= Shoot;
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.useGravity = false; 
    }
    public void Shoot(InputAction.CallbackContext ctx)
    {
            gameObject.transform.parent = null;
            ref List<GameObject> inv = ref GameObject.Find("Player").GetComponent<PlayerScript>().Inventory.INVENTORY;
            print(inv);
            inv.Remove(gameObject);

            Debug.LogError("stuff");
            
            rb.useGravity = true;

            rb.AddForce(stuff * new Vector3(5, 0, 5));

        
       
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Quest<T>
{
    public QUESTS quests = GameObject.FindFirstObjectByType<bowscript>().quest;
    int FindInstanceOfThing<s>(Type type, Quest<s>[] array)
    {
        var quest = array;
        for (int i = 0; i < quest.Length; i++)
        {
            if (quest[i] != null && quest[i].GetType() == type)
            {
                return i;
            }
        }
        return -1;
    }
    public virtual int Firstactivequest {
        
        get
        {
            quests = GameObject.FindAnyObjectByType<SwordScript>().quest; 
            var type = GetType();
            var instance = FindInstanceOfThing(type, quests.Quests);
            Debug.LogError(instance);
            return instance;
        }
    }
    public bool completed = false;
    public float exp;
    public GameObject player = GameObject.FindGameObjectWithTag("Finish");
     public T start { get; set; }
    public T goal { get; set; }
    List<InputAction> Actions { get; set; } = new List<InputAction>();

    T num; // This field is declared but not used. You might want to consider its purpose.

    public Quest(T goal, T start, float exp_val)
    {
        this.goal = goal;
        this.start = start;
        exp = exp_val;  
    }
    public void RemoveInput()
    {
        foreach (var i in Actions)
        {
            
            i.Disable();

        }
        Actions.Clear();

    }
    public virtual void Progress()
    {
        // Implementation for progress could go here
    }
    public virtual void Complete()
    {
        Debug.Log("completed task");
        player.GetComponent<PlayerScript>().exp += exp;
        completed = true;
               
    }
    public void AddInput(Dictionary<string/*name*/, string[]> inputs, InputActionMap map,  Action<InputAction.CallbackContext>[] performed)
    {

        InputAction action;
        foreach(var i in inputs.Keys)
        {
           
            if (!map.actions.Contains(map.FindAction(i)))
            {
                map.Disable();
                action = map.AddAction(i);
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    action.AddBinding(new InputBinding(inputs[i][j]));
                }
                map.Enable();
                Actions.Add(action);
                Debug.LogWarning($"action name: {Actions.Last().name},  Bindings : {Actions.Last().bindings[0]}");

                Debug.LogWarning(action.name);
                Actions.Last().Enable();
            }
            
            
           
               
            
            
        }
      
       
       
       for(int i = 0; i < Actions.Count;i++)
        {
            Actions[i].performed += performed[i];
        }
    }
}

public class KillQuest : Quest<float>
{
    float kills;
    public KillQuest(float goal,  float start, float exp) : base(goal, start, exp)
    {
        this.goal = goal;
        kills = 0;
        this.start = start;
    }
    public override void Progress()
    {
        kills = GameObject.Find("sword").GetComponent<SwordScript>().kill;
        if(start + goal >= kills && !completed)
        {
            Complete();
        }
      
    }
}

[InitializeOnLoad]
public class AddTwoVectors : InputProcessor<Vector3>
{

    static AddTwoVectors()
    {
        InputSystem.RegisterProcessor<AddTwoVectors>(); 
    }
    public float thing;
    public override Vector3 Process(Vector3 val, InputControl control)
    {
        return thing * val;
    }
}

[InitializeOnLoad]
public class HoldAndRelease : IInputInteraction<float>
{
    public float duration;
    bool stuff;
    public float max;
    bool notcreatedroutine = true;
    public float intervals;
    static HoldAndRelease()
    {
        InputSystem.RegisterInteraction<HoldAndRelease>();
    }
    //while it is started then add to power variable
    // then when it is released(performed) then return and throw
    public void Process(ref InputInteractionContext ctx) 
    {
       
       
       
        bowscript script = GameObject.Find("Bow").GetComponent<bowscript>();
        stuff = ctx.ControlIsActuated(1);
        
        if (notcreatedroutine && stuff)
        {
            script.StartCoroutine(Get(max));
            notcreatedroutine = false;
        }
        
        

        if (!stuff)
        {
            ref float power = ref GameObject.Find("Bow").GetComponent<bowscript>().stuff;
            notcreatedroutine = true;
            float x = GameObject.Find("Bow").GetComponent<bowscript>().stuff;
            if (x >= duration)
            {
                ctx.action.performed -= script.Shoot;
                ctx.action.performed += script.Shoot;
                ctx.Performed();
                Debug.LogWarning("He");

                



            }
            else
            {
                
                ctx.Canceled();
                




            }
            power = 0;
            return;
        }
        // subroutine
        
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    public IEnumerator Get( float dur)
    {
        
            while (GameObject.Find("Bow").GetComponent<bowscript>().stuff < dur && stuff)
            {
                    decimal temp = (decimal)GameObject.Find("Bow").GetComponent<bowscript>().stuff + 1;
                    yield return new WaitForSeconds(intervals);
            
                    GameObject.Find("Bow").GetComponent<bowscript>().stuff = (float)temp;
                    Debug.Log(temp);
                    continue;

            }
            yield break;
    }

    public void Reset()
    {
        
    }

    
}
public class FetchQuest : Quest<GameObject>
{

    bool fetched = false;
    float distance;
    InputActionMap quests_inputs;
    InputAction give;
    
    public FetchQuest(GameObject goal, GameObject start, float exp) : base(goal, start, exp)
    {
        this.goal = goal;
        this.start = start;
        quests_inputs = new("Quests");
        PlayerControls controls = new();
        AddInput(new Dictionary<string, string[]> { ["Give"] = new string[] { "<Keyboard>/f" }, ["Take"] = new string[] { "<Keyboard>/m" } }, controls.Main,  new Action<InputAction.CallbackContext>[] {Give_performed, Take_performed});
    }
    
    void Take_performed(InputAction.CallbackContext ctx)
    {
        
        distance = Vector3.Distance(goal.transform.position, player.transform.position);

        if (distance < 10 && !fetched)
        {
            fetched = true;
            Debug.LogWarning("takes");
            ref var inv = ref player.GetComponent<PlayerScript>().Inventory.INVENTORY;
            inv.Add(goal);
            PlayerScript.IsInventoryExists = true;

        }
             

        
    }

    private void Give_performed(InputAction.CallbackContext obj)
    {
        ref var inv = ref player.GetComponent<PlayerScript>().Inventory.INVENTORY;
        inv.Remove(goal);
        distance = Vector3.Distance(goal.transform.position, start.transform.position);
        if(fetched && distance <= 10 && obj.performed && !completed)
        {
            Debug.LogWarning("Gives");
            fetched = false;
            Debug.Log("Hi");
            
            PlayerScript.Deactivate(goal);
            Complete();
        }
       
        
    }

   
  
}