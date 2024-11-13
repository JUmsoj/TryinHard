using System.Collections.Generic;
using UnityEngine;
public class Player : ScriptableObject
{
    public List<GameObject> INVENTORY;

    private void Awake()
    {
        INVENTORY = new List<GameObject>();
    }
}
