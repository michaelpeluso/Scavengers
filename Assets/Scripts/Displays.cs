using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displays : MonoBehaviour
{
    public string cname; //name of the collectible
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.gameObject;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
