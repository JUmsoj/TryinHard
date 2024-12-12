using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class OldManScript : MonoBehaviour
{
    [SerializeField] private Quest<float>[] fQuest = new Quest<float>[5];
    [SerializeField] private Quest<GameObject>[] gQuest = new Quest<GameObject>[5];
    public Quest<float>[] quest { get { return fQuest; } set { fQuest = value; } }
    public Quest<GameObject>[] gquest {  get { return gQuest; } set { gQuest = value; } }
    [SerializeField]private PlayerControls controls;
    [SerializeField] private GameObject player;
    [SerializeField] int length;
    private float distance { get; set; }
    [SerializeField]private bowscript bow;
    bool gavequests = false;
    void Awake()
    {
        controls = new();
        
        player = GameObject.FindGameObjectWithTag("Finish");
        bow = GameObject.FindAnyObjectByType<bowscript>();
        fQuest[0] = new KillQuest(3f, GameObject.FindAnyObjectByType<SwordScript>().kill, 5f);
        fQuest[1] = new KillQuest(3f, GameObject.FindAnyObjectByType<SwordScript>().kill, 5f);
        fQuest[2] = new KillQuest(3f, GameObject.FindAnyObjectByType<SwordScript>().kill, 5f);
        fQuest[3] = new KillQuest(3f, GameObject.FindAnyObjectByType<SwordScript>().kill, 5f);
        fQuest[4] = new KillQuest(3f, GameObject.FindAnyObjectByType<SwordScript>().kill, 5f);
        
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
        if (distance < 10 && !gavequests)
        {
            gavequests = true;
            bow.StartingQuests();
            Debug.LogWarning("HEY");
        }
        else if (distance < 10 && gavequests)
        {
            for(int i = 0; i < fQuest.Count(); i++)
            {
                AddToArray(bow.quest.Quests, i, fQuest);
                
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
        for (int i = 0; i < array.Count(); i++)
        {
            if (array[i] == null)
            {
                array[i] = thing_taken_out_of[obj];
                thing_taken_out_of[obj] = default;
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
