using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum coltype
{
    UNASSIGNED,
    MINERAL,
    NATURE,

}

public struct collectible
{
    public bool inuse;
    public bool collected; //a bool to check if it's collected or not
    public coltype type; //the type that is associated the the collectible
    public GameObject gObject;  //the gameobject associated with the collectible
    public string name;  //what the collectible will be called
    public string desc;  //the description for the collectible
    public int price; //the price of the collectible
}

public class Collectibles : MonoBehaviour
{

    collectible[] collection_array = new collectible[1000]; //an array of collectibles

    /*
     * Adds to an array of collectibles
     * This is used for organizing and limiting amount of collectibles in game.
     */
    collectible new_collectible(coltype type, GameObject gObject, string name, string desc, int price)
    {
         for (int x = 0; x < collection_array.Length; x++)
            {
            if (!collection_array[x].inuse)
            {
                Debug.Log("Assigning stuff");
                collection_array[x].inuse = true;
                collection_array[x].collected = false;
                collection_array[x].type = type;
                collection_array[x].gObject = gObject;
                collection_array[x].name = name;
                collection_array[x].desc = desc;
                collection_array[x].price = price;

                Debug.Log("Added Array to collection");
                return collection_array[x];
              }
              continue;
            }
         Debug.Log("Collection Array is full");
         return collection_array[999];
    }

    public void collectible_collected(collectible col)
    {
        for (int x = 0; x < collection_array.Length; x++)
        {
            if(collection_array[x].name == col.name)
            {
                collection_array[x].collected = true;
            }
        }
    }

    public bool check_collectible_collected(collectible col)
    {
        for (int x = 0; x < collection_array.Length; x++)
        {
                if (collection_array[x].collected && col.name == collection_array[x].name)
                {
                    return true;
                }
        }
        return false;

    }
    public bool check_collectible_collected_by_string(string cname)
    {
        for (int x = 0; x < collection_array.Length; x++)
        {
            if (collection_array[x].collected && cname == collection_array[x].name)
            {
                return true;
            }
        }
        return false;

    }
    /*
    * Takes the name of gameObject and assigns the collectible accordingly.
    */
    public collectible assign_collectible(string objectName, GameObject obj)
    {
        collectible col = new collectible();
        switch (objectName)
        {
            case "Andesite":
                col = new_collectible(coltype.MINERAL, obj, "Andesite", "A grey rock found mostly near volcanic activity", 100);
                Debug.Log(col.name + " was successfully assigned");
                return col;
            case "Red_Mushroom":
                col = new_collectible(coltype.NATURE, obj, "Red Mushroom", "A mushroom commonly found in the forest", 100);
                Debug.Log(col.name + " was successfully assigned");
                return col;
            case "Brown_Mushroom":
                col = new_collectible(coltype.NATURE, obj, "Brown Mushroom", "A mushroom commonly found in the forest", 100);
                Debug.Log(col.name + " was successfully assigned");
                return col;
            case "Red_Flower":
                col = new_collectible(coltype.NATURE, obj, "Red Flower", "A flower commonly found in the forest", 100);
                Debug.Log(col.name + " was successfully assigned");
                return col;
            case "Coconut":
                col = new_collectible(coltype.NATURE, obj, "Coconut", "A nut that contains liquids, commonly found in the beach", 100);
                Debug.Log(col.name + " was successfully assigned");
                return col;
            case "Gold":
                col = new_collectible(coltype.NATURE, obj, "Gold", "A rare treasure", 5000);
                Debug.Log(col.name + " was successfully assigned");
                return col;
            case "Starfish":
                col = new_collectible(coltype.NATURE, obj, "Starfish", "It's suggested that starfish lack a brain", 100);
                Debug.Log(col.name + " was successfully assigned");
                return col;
            case "Shell":
                col = new_collectible(coltype.NATURE, obj, "Shell", "Marine life", 100);
                Debug.Log(col.name + " was successfully assigned");
                return col;
            default:
                Debug.Log("assignment of collectible failed");
                return col; //returns an empty collectible
        }
    }

}

