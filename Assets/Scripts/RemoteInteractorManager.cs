using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteInteractorManager : MonoBehaviour
{
    private Collider thisCollider;
    public Dictionary<GameObject, GameObject> pockets;

    public GameObject backpack;
    private GameObject itemBeingHeld;
    private ItemHandler heldItemScript;
    
    void Start()
    {
        UpdatePockets();
        thisCollider = gameObject.GetComponent<MeshCollider>();
        itemBeingHeld = null;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<ItemHandler>() != null) {
            if (other.gameObject.GetComponent<ItemHandler>().inHand) {
                itemBeingHeld = other.gameObject;
                heldItemScript = itemBeingHeld.GetComponent<ItemHandler>();
            }
        }
        
        //Debug.Log("equal: " + GameObject.ReferenceEquals(itemBeingHeld, backpack));
        //Debug.Log("contains backpack: " + !pockets.ContainsValue(backpack));

        if (GameObject.ReferenceEquals(itemBeingHeld, backpack) && !pockets.ContainsValue(backpack) && pockets.ContainsKey(other.gameObject)) {
            //Debug.Log("Put backpack in pocket " + other.gameObject.name);
            Camera.main.gameObject.GetComponent<PlayerManager>().UpdateInspectorPocket(other.gameObject, backpack);
        }
    }
    
    public void UpdatePockets() {
        pockets = Camera.main.GetComponent<PlayerManager>().pockets;
    }
}
