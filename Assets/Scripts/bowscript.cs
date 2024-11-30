using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System;
using System.Net;
public class bowscript : MonoBehaviour
{
    public float stuff;
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
        if (context.performed && context.interaction is HoldAndRelease )
        {
           
           gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<PlayerScript>().Inventory.INVENTORY

            .Remove(gameObject);
            Debug.LogError("stuff");
            gameObject.transform.parent = null;
            rb.useGravity = true;

            rb.AddForce(stuff * new Vector3(5, 0, 5));

        }
       
        
        
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
    
    [InitializeOnLoad]
public class HoldAndRelease : IInputInteraction<float>
{
    public float duration;
    
    static HoldAndRelease()
    {
        InputSystem.RegisterInteraction<HoldAndRelease>();
    }
    //while it is started then add to power variable
    // then when it is released(performed) then return and throw
    public void Process(ref InputInteractionContext ctx)
    {
        
        bool stuff = ctx.ControlIsActuated(0.75f);
        ref float power = ref GameObject.Find("Bow").GetComponent<bowscript>().stuff;
        bowscript script =GameObject.Find("Bow").GetComponent<bowscript>();
        if(stuff && power <= duration)
        {
            
            
            script.StartCoroutine(Get(stuff, duration));
            
        }
         if(!stuff) 
        {
            script.StopCoroutine(Get(stuff, duration));
            
            if(power >= duration + 8)
            {
                ctx.Performed();
            }
            else
            {
                power = 0;
                ctx.Canceled();
            }

        }
    }
    // subroutine
    public IEnumerator Get(bool nen, float dur)
    {
        while (GameObject.Find("Bow").GetComponent<bowscript>().stuff < dur)
        {
            yield return new WaitForSeconds(2);
            if (nen)
            {
                try
                {
                    GameObject.Find("Bow").GetComponent<bowscript>().stuff++;

                }
                catch (Exception e)
                {
                    MonoBehaviour.print($"exception {e}");
                }
            }
        }
        
    }
   
    public void Reset()
    {

    }
}