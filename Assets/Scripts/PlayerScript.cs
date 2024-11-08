using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static double health = 100;
    public double x;
    double move;
    float speed;
    Transform trans;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = UnityEngine.Random.Range((float)0.5, 1); 
        trans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        print(health);
        if (Input.GetAxisRaw("Vertical") != 0) 
        {
            move = speed * Input.GetAxisRaw("Vertical");
            trans.Translate(0, 0, (float)move/2);
            print("moving vertically");
        }
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            move = speed * Input.GetAxisRaw("Horizontal");
            trans.Translate((float)move/2, 0,0);
            print("moving horizontally");
        }
    }
}
