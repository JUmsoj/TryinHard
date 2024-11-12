using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    bool cooldown= false;
    private CharacterController cc;
    private Vector3 move;
    public static double health = 100;
    public double x;
    float speed = 0.001f;
    Transform trans;
    public Transform cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.Find("FreeLook Camera").transform;
        speed = UnityEngine.Random.Range((float)0.5, 1); 
        trans = gameObject.GetComponent<Transform>();
        cc = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 currentlook = cam.forward;
            currentlook.y = 0;
            Quaternion target = Quaternion.LookRotation(currentlook);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);
            transform.rotation = cam.rotation;
        }
    }
    private void Move()
    {
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {

            move = Time.deltaTime * speed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            move = transform.TransformDirection(move);
            cc.Move(Thing(move));
            Turn();


        }
        if (!cc.isGrounded)
        {
            cc.Move(new Vector3(0, (float)-9.18, 0));
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
    Vector3 Thing(Vector3 move)
    {
        move.Normalize();
        return move;
    }
    private void OnCollisionEnter(Collision collision)
    {
       
    }
}
