using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_Submit : MonoBehaviour
{
    private GameObject Player;
    List<collectible> inv;
    private TextMeshProUGUI itemList1;
    private TextMeshProUGUI itemList2;

    // Start is called before the first frame update
    void Start()
    {
        Player = Camera.main.gameObject;
        itemList1 = GameObject.Find("FoundItems1").GetComponent<TextMeshProUGUI>();
        itemList2 = GameObject.Find("FoundItems2").GetComponent<TextMeshProUGUI>();
        itemList1.text = "";
        itemList2.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        inv = Player.GetComponent<PlayerManager>().inventory;
        for (int x = 0;  x < inv.Count; x++)
            {
                if(inv[x].collected)
                {
                    Player.GetComponent<PlayerManager>().money += inv[x].price;
                    Debug.Log("Thank you for the " + inv[x].name);
                }
                else
                {
                    Player.GetComponent<Collectibles>().collectible_collected(inv[x]);
                    Debug.Log("Thank for your donation of " + inv[x].name);
                    itemList1.text += inv[x].name + "\n";
                    itemList2.text += inv[x].name + "\n";
                }
            inv.RemoveAt(x);
            }
    }
}
