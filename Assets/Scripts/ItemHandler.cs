using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public string itemDescription;

    public bool inHand = false;
    public bool inHandPreviousFrame = false;
    private float inHandDelay = 1f;
    private float colliderDelay = 0.1f;
    public bool inBackpack = false;
    public bool inHolster = false;
    private GameObject currentPocket;


    public Canvas infoPopUpPrefab;
    private Canvas infoPopUp;
    private GameObject Player;
    private Dictionary<GameObject, GameObject> pockets;
    private Rigidbody rb;
    private Collider[] colliders;
    private collectible collect;

    public RemoteInteractorManager remoteScript;
    public PlayerManager playerManager;
    public Collectibles cManager;

    private void Start()
    {
        Player = Camera.main.gameObject;
        playerManager = Player.GetComponent<PlayerManager>();
        cManager = Player.GetComponent<Collectibles>();
        pockets = playerManager.pockets;
        gameObject.GetComponent<Outline>().OutlineWidth = 4;
        gameObject.GetComponent<Outline>().enabled = false;
        collect = cManager.assign_collectible(this.gameObject.name, this.gameObject);
        Debug.Log(collect.name);
        PopUpSetUp();
        
        colliders = GetComponentsInChildren<Collider>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        //remoteScript = Camera.main.transform.GetChild(0).GetComponent<RemoteInteractorManager>();
    }

    void PopUpSetUp() 
    {
        infoPopUp = Instantiate(infoPopUpPrefab, gameObject.transform);
        if(collect.collected)
        {
            infoPopUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = collect.name;
            infoPopUp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = collect.desc;
        }
        infoPopUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text ="???";
        infoPopUp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        infoPopUp.transform.localScale = new Vector3(infoPopUp.transform.localScale.x / gameObject.transform.localScale.x, infoPopUp.transform.localScale.y / gameObject.transform.localScale.y, infoPopUp.transform.localScale.z / gameObject.transform.localScale.z);
        infoPopUp.enabled = false;
        if (gameObject.name == "Backpack") {
            infoPopUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text ="Backpack";
            infoPopUp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "A useful tool to store items.";
        }
    }

    void Update()
    {
        if (infoPopUp.enabled) {
            infoPopUp.transform.position = gameObject.transform.position + new Vector3(0f, 0.2f, 0f);
            infoPopUp.transform.LookAt(transform.position + Player.transform.rotation * Vector3.forward);
        }
        if (inHolster && !inHand && inHandPreviousFrame) {
            //HolsterItem();
        }
        if (gameObject.activeSelf) { checkIfCollected(); }
        
        UpdateDelays();
    }

    public void HoverEnter() 
    {
        gameObject.GetComponent<Outline>().enabled = true;
        infoPopUp.enabled = true;
    }

    public void HoverExit() 
    {
        gameObject.GetComponent<Outline>().enabled = false;
        infoPopUp.enabled = false;
    }

    public void SelectEnter() {
        foreach (Collider col in colliders) {
            col.isTrigger = true;
        }
        rb.constraints = RigidbodyConstraints.None;
        inHand = true;
    }

    public void SelectExit() {
        inHand = false;
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.name == "Backpack Trigger" && gameObject.name != "Backpack" && !inHand && inHandPreviousFrame) {
            Player.GetComponent<PlayerManager>().inventory.Add(collect);
            gameObject.SetActive(false);
            inBackpack = true;
        }
        if (gameObject.tag == "Tool" && pockets.ContainsKey(other.gameObject) && !inHand && inHandPreviousFrame) {
            Debug.Log("holster item");
            if (playerManager.pockets.ContainsKey(other.gameObject)) {
                playerManager.pockets[other.gameObject] = gameObject;
                playerManager.UpdateInspectorPocket(other.gameObject, gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    private void UpdateDelays() {
        inHandDelay -= Time.deltaTime;
        if (inHandDelay <= 0f) {
            inHandPreviousFrame = inHand;
            inHandDelay += 1f;
        }
        colliderDelay -= Time.deltaTime;
        if (colliderDelay < 0f) {
            if (!inHand) {
                foreach (Collider col in colliders) {
                    col.isTrigger = false;
                }
            }
            colliderDelay += 0.1f;
        }
    }

    private void HolsterItem(GameObject pocket) {
        playerManager.pockets[pocket] = gameObject;
        playerManager.UpdateInspectorPocket(pocket, gameObject);
        gameObject.SetActive(false);
    }
    public void checkIfCollected()
    {
        //GameObject tempObject;
        if(cManager.check_collectible_collected(collect))
        {
            collect.collected = true;
            infoPopUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = collect.name;
            infoPopUp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = collect.desc;
            //Debug.Log(collect.name + "_Display");
            //tempObject = GameObject.Find(collect.name + "_Display");
            //Debug.Log(tempObject.ToString());
            //tempObject.SetActive(true);
            //gameObject.SetActive(false);
        }
    }
}
