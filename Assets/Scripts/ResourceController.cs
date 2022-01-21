using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{

    public int totalWoodCount;

    void Update()
    {
        totalResourceCount();
    }
    public void totalResourceCount()
    {
        var totalWoodCountTemp = 0;
        Stockpile[] allStockpiles = GameObject.FindObjectsOfType<Stockpile>();
        foreach (Stockpile stockpile in allStockpiles)
        {
            totalWoodCountTemp += stockpile.storedWood;
            //stone count
            //iron count
            //food count
        }
        totalWoodCount = totalWoodCountTemp;
    }
}
