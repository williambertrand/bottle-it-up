using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ItemLocation
{
    public string item;
    public int shelf; //index of this items location down the aisle
}


[System.Serializable]
public struct AisleData
{
    public string name;  
    public int shelfCount;
    public ItemLocation[] itemLocations;
}

