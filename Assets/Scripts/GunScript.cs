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
    public class BossQuest : Quest<GameObject>
    {
        GameObject boss;
        GameObject[] enemies;
        bool defeated_minions;
        public BossQuest(GameObject boss,  float exp, GameObject start = null, params KillQuest[] stuff) : base(boss, start: start, exp_val: exp)
        {
            this.boss = boss;
            for(int i = 0; i < enemies.Length; i++) 
            {
                stuff[i].goal = 1;
                enemies[0].GetComponent<EnemyScript>();
            }

        }
    }
}
