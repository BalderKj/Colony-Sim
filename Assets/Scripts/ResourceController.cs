using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceController : MonoBehaviour
{
    
    public TextMeshProUGUI WoodCountText;
    public int totalWoodCount;


    void Update()
    {
        totalResourceCount();
        UpdateText();
        Debug.Log("resourceController currently has wood: " + totalWoodCount);

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

    void UpdateText()
    {
        WoodCountText.text =  "wood: " +totalWoodCount.ToString();
    }
}
