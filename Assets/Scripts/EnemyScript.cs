using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool active = false;
    private bool touchingground = false;
    public GameObject player;
    private Rigidbody rb;
    public static Spawner spawner;
    private bool iscooldown = false;
    public bool touchingplayer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.Find("Player");
        if (gameObject.name != "Enemy")
        {
            gameObject.transform.position = new Vector3(0, 2, 0);
        }
    }
    void Start()
    {
        if (spawner == null)
        {
            spawner = ScriptableObject.CreateInstance<Spawner>();
          
        }
        if (gameObject.name != "Enemy")
        {
            spawner.count++;
        }
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(touchingplayer)
        {
            if (gameObject.name != "Enemy")
            {
                gameObject.name = "TrueEnemy";
            }
            /*if(PlayerScript.health < 0 )
            {
                Debug.LogError("Kill");
                Destroy(player);
            }*/
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
    private IEnumerator Fight()
    {
        active = true;
        yield return new WaitForSeconds(1);
        PlayerScript.health--;
        active = false;
        Debug.Log("took damage");
        yield break;
    }
    void Move()
    {
        if (touchingground && gameObject.name != "Enemy")
        {
            rb.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, Time.deltaTime * 5);
        }
        if(touchingplayer && !active)
        {
            StartCoroutine(Fight());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Finish"))
        {
            
            touchingplayer = true;
            print("touched player");
            PlayerScript.health--;
        }
        else if (collision.gameObject.name == "Ground")
        {
            touchingground = true;
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Finish"))
        {
            touchingplayer = false;
            PlayerScript.health--;
        }
        else if(collision.gameObject.name == "Ground")
        {
            touchingground = false;
        }
       
    }

    internal int NumberPress()
    {
        throw new NotImplementedException();
    }
}
