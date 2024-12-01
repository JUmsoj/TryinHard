using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System;
using System.Net;
using System.Collections.Generic;
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
                    var temp = GameObject.Find("Bow").GetComponent<bowscript>().stuff + 1;
                    yield return new WaitForSeconds(intervals);
                    GameObject.Find("Bow").GetComponent<bowscript>().stuff = temp;
                    continue;

            }
            yield break;
    }

    public void Reset()
    {
        
    }
}