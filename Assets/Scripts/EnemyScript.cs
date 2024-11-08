using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private bool touchingground = false;
    public GameObject player;
    private Rigidbody rb;
    private Spawner spawner;
    private bool iscooldown = false;
    int x;
    int y;
    int z;
    public bool touchingplayer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameObject.transform.position = new Vector3(0, 2, 0);
        x = UnityEngine.Random.Range(-1, 1);
        y = 0;
        z = UnityEngine.Random.Range(-1, 1);
    }
    void Start()
    {

        spawner = ScriptableObject.CreateInstance<Spawner>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(touchingplayer)
        {
            if (gameObject.name != "Enemy")
            {
                gameObject.name = "MINOR";
            }
            if(PlayerScript.health <= 0 )
            {
                Destroy(player);
            }
        }
        if (!iscooldown && gameObject.name == "Enemy")
        {
            StartCoroutine(cooldown());
        }
        Move();
        
    }
    private IEnumerator cooldown()
    {
        iscooldown = true;
        yield return new WaitForSeconds(5);
        iscooldown = false;
        spawner.spawn();
        yield break;
    }
    void Move()
    {
        if (touchingground)
        {
            gameObject.transform.Translate(x, y, z);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Finish"))
        {
            player = collision.gameObject;
            touchingplayer = true;
        }
        else if (collision.gameObject.name == "Ground")
        {
            touchingground = true;
        }
        print("touched player");
        PlayerScript.health--;
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Finish"))
        {
            touchingplayer = false;
        }
        else if(collision.gameObject.name == "Ground")
        {
            touchingground = false;
        }
        PlayerScript.health--;
    }

}
