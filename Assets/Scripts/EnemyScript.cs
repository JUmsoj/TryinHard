using JetBrains.Annotations;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private MeshRenderer mr;
    public bool hascrate = false;
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
        mr = GetComponent<MeshRenderer>();
        GetComponent<Rigidbody>().mass *= 7f;
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
        if (collision.gameObject.CompareTag("Tile"))
        { 
            touchingground = true;
            print("Yes");
        }
        if (collision.gameObject.name == "Player")
        {
            if(hascrate)
            {
                ReCapture();
                Debug.LogError("Player recaptured crate");
            }

        }
        if (collision.gameObject.name == "Cube")
        {
            if (!hascrate && collision.gameObject.transform.parent == null)
            {
                EnemyCapture(gameObject, collision.gameObject);
                Debug.LogError("Enemy captured crate");
            }
        }
    }
    void EnemyCapture(GameObject Captor, GameObject crate)
    {
        crate.transform.parent = Captor.transform;
        hascrate = true;
        crate.transform.position = gameObject.transform.position;
        crate.transform.Translate(0, 5, 0);
        mr.material = Resources.Load<Material>("whon");
        return;

    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Finish"))
        {
            touchingplayer = false;
            PlayerScript.health--;
        }
        if(collision.gameObject.CompareTag("Floor"))
        {
            touchingground = false;
        }
       
    }
    void ReCapture()
    {
        mr.material = Resources.Load<Material>("Enemy"); 
        var crate = gameObject.transform.GetChild(0).gameObject;
        crate.transform.parent = null;
        crate.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(5, -5), 0, UnityEngine.Random.Range(5, -5)));
        hascrate = false;
        Vector3 Throw = new(0, 0, 0);
        Throw.Set(-5, 0, 5);
        Throw *= Time.deltaTime;
        Throw.Normalize();
        rb.AddForce(Throw);    
    }
    internal int NumberPress()
    {
        throw new NotImplementedException();
    }
    
}
