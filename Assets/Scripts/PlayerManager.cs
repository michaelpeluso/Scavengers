using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<collectible> inventory = new List<collectible>();
    public Dictionary<GameObject, GameObject> pockets = new Dictionary<GameObject, GameObject>();
    public GameObject backpack;
    public int money;

    public List<GameObject> pocketKey = new List<GameObject>();
    public List<GameObject> pocketValue = new List<GameObject>();

    void Start()
    {
        money = 0;
        foreach (Transform child in transform) {
            pockets.Add(child.gameObject, null);
            pocketKey.Add(child.gameObject);
        }
    }

    void Update()
    {
    }

    public void UpdateInspectorPocket(GameObject pocket, GameObject item) 
    {
        if (pocketKey.Contains(pocket)) {
            int index = 0;
            foreach (GameObject poc in pocketKey) {
                index++;
                if (GameObject.ReferenceEquals(poc, pocket)) {
                    pocketValue[index] = item;
                }
            }
        }
    }
}
