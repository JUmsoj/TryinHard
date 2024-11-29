using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class bowscript : MonoBehaviour
{
    private GameObject parent;
    private PlayerControls controls;
    private Rigidbody rb;
    private void Awake()
    {
        controls = new();
        
    }
    private void OnEnable()
    {
        
        controls.Main.Bow.Enable();
        rb = GetComponent<Rigidbody>();
        controls.Main.Bow.performed += Shoot;
        controls.Main.Bow.started += context =>
        {
            Debug.LogWarning("shooting");
        };
        
    }
    private void OnDisable()
    {
        controls.Main.Bow.Disable();
        controls.Main.Bow.performed -= Shoot;
        controls.Main.Bow.started -= context =>
        {
            Debug.LogWarning("shooting");
        };
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.useGravity = false; 
    }
    void Shoot(InputAction.CallbackContext context)
    {
             ref var inventory = ref gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<PlayerScript>().Inventory.INVENTORY;
            inventory.Remove(gameObject);
            Debug.LogError("stuff");
            gameObject.transform.parent = null;
            rb.useGravity = true;
            
            rb.linearVelocity = (new Vector3(5, 0, 5));
       
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
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
