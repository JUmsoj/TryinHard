using UnityEngine;

public class PlayerScript : MonoBehaviour
{
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
        if (Input.GetAxisRaw("Vertical") != 0) 
        {

            move = speed * Input.GetAxisRaw("Vertical");
            trans.Translate((float)move, 0, (float)move);
            print("moving vertically");
        }
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            move = speed * Input.GetAxisRaw("Horizontal");
            trans.Translate((float)move, 0, (float)move);
            print("moving horizontally");
        }
    }
}
