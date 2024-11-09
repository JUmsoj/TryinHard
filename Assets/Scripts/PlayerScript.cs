using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    public static double health = 100;
    public double x;
    double move;
    float speed;
    Transform trans;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetOrAddComponent<Rigidbody>();
        speed = UnityEngine.Random.Range((float)0.5, 1); 
        trans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        x = health;
        if(health < 0)
        {
            Destroy(gameObject);
        }
        if (Input.GetAxisRaw("Vertical") != 0) 
        {
            move = speed * Input.GetAxisRaw("Vertical");
            rb.linearVelocity = new Vector3(0, 0, (float)move * 1);
            
        }
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            move = speed * Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector3((float)move * 10, 0,0);
            
        }
    }
}
