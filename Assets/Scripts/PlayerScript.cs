using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private CharacterController cc;
    private Vector3 move;
    public static double health = 100;
    public double x;
    float speed = 30;
    Transform trans;
    public Transform cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = UnityEngine.Random.Range((float)0.5, 1); 
        trans = gameObject.GetComponent<Transform>();
        cc = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        x = health;
        if(health < 0)
        {
            Destroy(gameObject);
        }
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) 
        {
            
            move =  Time.deltaTime * speed * new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, Input.GetAxisRaw("Vertical") * speed);
            move = transform.TransformDirection(move);
            cc.Move(Thing(move));
            Turn();
            
            
        }
        if (!cc.isGrounded)
        {
            cc.Move(new Vector3(0, (float)-9.18, 0));
        }
    }
    void Turn()
    {
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 currentlook = cam.forward;
            currentlook.y = 0;
            Quaternion target = Quaternion.LookRotation(currentlook);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5);
        }
    }
    Vector3 Thing(Vector3 move)
    {
        move.Normalize();
        return move;
    }
}
