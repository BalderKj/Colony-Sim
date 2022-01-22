using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCanvas : MonoBehaviour
{
    public GameObject LumberyardBluePrintPlacement;
    public bool isPlacing = false;

    public void SpawnLumberyard()
    {
        if(GameObject.Find("LumberyardBluePrintPlacement(Clone)") == null)
        {
            Instantiate(LumberyardBluePrintPlacement);
            isPlacing = true;
            
        }
        
        
    }




}
