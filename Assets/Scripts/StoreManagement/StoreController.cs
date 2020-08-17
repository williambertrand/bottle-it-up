using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[System.Serializable]
public class StoreData
{
    public AisleData[] aisles;
}


public struct Aisle
{
    public Dictionary<string, int> itemShelfIndex;
    public Vector3 startPosition;
    public int aisleNum;

    public Aisle(AisleData d, int shelf)
    {
        itemShelfIndex = new Dictionary<string, int>();
        for (int i = 0; i < d.itemLocations.Length; i++)
        {
            itemShelfIndex.Add(
                d.itemLocations[i].item,
                d.itemLocations[i].shelf
            );
        }
        startPosition = new Vector3(
            StoreConstants.AisleStartX + StoreConstants.AisleWidth * shelf,
            0.5f,//some const y value
            StoreConstants.AisleStartZ
        );
        aisleNum = shelf;
    }

}


public class Store
{
    public Dictionary<string, Aisle> aisles;
    const float NAV_Y = 0.5f;
    int[] shelfPerAisle;

    public string[] inventory;
    public Dictionary<string, Vector3> itemPositions;


    public Store(StoreData storeData)
    {
        aisles = new Dictionary<string, Aisle>();
        itemPositions = new Dictionary<string, Vector3>();
        shelfPerAisle = new int[storeData.aisles.Length];
        int count = 0;
        //Fill out the aisles for the store from the serialized aisle data
        for (int i = 0; i < storeData.aisles.Length; i++)
        {
            Aisle aisle = new Aisle(storeData.aisles[i], i);
            aisles.Add(storeData.aisles[i].name, aisle);
            shelfPerAisle[i] = storeData.aisles[i].shelfCount;
            count += storeData.aisles[i].itemLocations.Length;
        }

        //Fill in inventory after everything has been loaded
        inventory = new string[count];
        int index = 0;
        for (int i = 0; i < storeData.aisles.Length; i++)
        {
            string category = storeData.aisles[i].name;
            ItemLocation[] itemLocations = storeData.aisles[i].itemLocations;

            for(int j = 0; j < itemLocations.Length; j++)
            {
                inventory[index] = itemLocations[j].item;
                itemPositions.Add(itemLocations[j].item, _GetItemLocation(itemLocations[j].item, category));
                index++;
            }
        }

    }

    //Get location shelf units down aise #a
    private Vector3 ShelfLocation(int a, int shelf)
    {
        float shelfLen = (StoreConstants.AisleEndZ - StoreConstants.AisleStartZ) / shelfPerAisle[a];

        return new Vector3(
            StoreConstants.AisleStartX + StoreConstants.AisleWidth * a,
            NAV_Y, //some const y value
            StoreConstants.AisleStartZ + shelfLen * shelf
        );
    }

    private Vector3 _GetItemLocation(string item, string category)
    {
        if (!aisles.ContainsKey(category))
        {
            return Vector3.zero;
        }

        Aisle aisle = aisles[category];
        return ShelfLocation(aisle.aisleNum, aisle.itemShelfIndex[item]);
    }


    //Return Zero Vector on item or aisle not found
    public Vector3 GetItemLocation(string item)
    {
        if (!itemPositions.ContainsKey(item))
        {
            return Vector3.zero;
        }

        return itemPositions[item];
    }

    public string GetRandomItem()
    {
        int randInt = (int)Random.Range(0, inventory.Length);
        return inventory[randInt];
    }

}

public class StoreController : MonoBehaviour
{

    public Store store;
    public string StoreName;

    //Navigation points to be set
    public Transform exitPoint;
    public Transform checkoutPoint;

    public static StoreController Instance;

    private void Awake()
    {
        //Lazy boy singleton
        if(Instance == null) { Instance = this; }

        LoadStoreDataWithName(StoreName);

        Debug.Log("-- Store Data Loaded --");
        Debug.Log("Aisles: " + store.aisles.Count);

        if (exitPoint == null)
        {
            Debug.Log("WARNING: No exit point set for store");
        }
    }

    public void LoadStoreDataWithName(string storeName)
    {
        string filePath = "Text/" + storeName;

        TextAsset storeFile = Resources.Load<TextAsset>(filePath);
        
        Debug.Log("LOADING Store Info FROM:" + filePath);
        if (storeFile)
        {
            Debug.Log("Read in file, creating store data....");
            StoreData storedata = JsonUtility.FromJson<StoreData>(storeFile.text);
            store = new Store(storedata);
        }
        else
        {
            Debug.LogError("File does not exist: " + filePath);
        }
    }

    void OnDrawGizmos()
    {
        if (store == null) return;
        var categories = store.aisles.Keys;
        foreach (var key in categories)
        {
            var items = store.aisles[key].itemShelfIndex.Keys;

            foreach(var item in items)
            {
                Handles.Label(store.GetItemLocation(item), item);
            }

        }
    }

}
