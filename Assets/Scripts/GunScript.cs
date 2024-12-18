using System;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private PlayerControls controls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        controls = new();
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
        // all other ones will check if it is active
       
    
}
public class BossQuest : Quest<GameObject>
{
    GameObject boss;
    GameObject[] enemies;
    bool defeated_minions;
    int enemycount;
    public BossQuest(GameObject boss, float exp, GameObject start = null, params GameObject[] enemies) : base(boss, start: start, exp_val: exp)
    {
        if (!player.GetComponent<PlayerScript>().IsInBossFight) player.GetComponent<PlayerScript>().IsInBossFight = true;
      
        else
        {
            active = false;
            return;
        }
        Debug.LogWarning("Made it");
        this.boss = boss;
        this.enemies = enemies;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyScript>().Event = new();
            enemies[i]?.GetComponent<EnemyScript>().Event.AddListener(OnAttack);
            enemycount++;
        }
        Spawn();
    }
    void Spawn()
    {
        foreach(var thing in enemies)
        {
            thing.transform.position = new Vector3(0, 3, 0);
            thing.name = "Enemy(NotClone)";
        }
    }
    void OnAttack(GameObject obj)
    {
        if (active)
        {
            var enemy = Array.IndexOf(enemies, obj);
            enemies[enemy] = null;
            enemycount--;
            if (enemycount == 0)
            {
                defeated_minions = true;
            }
            Debug.LogWarning("yipeeeee");
        }
    }
}
