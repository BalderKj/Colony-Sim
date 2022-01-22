using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrintPlacement : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    
    bool canPlace = true;
    bool reqResources = false;
    public GameObject prefab;
    public BuildCanvas BuildCanvas;

    public ResourceController resourceController;

    //resource costs
    public int woodCost;

    
    
    Renderer rend;

    void Start() 
    {
        rend = GetComponent<Renderer>();
        resourceController = FindObjectOfType<ResourceController>();
        
    }

    void Update()
    { 
        //check for required resources
        ResourceCheck();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = LayerMask.GetMask("Ground");
        if(Physics.Raycast(ray, out hit, 50000.0f, mask))
        {
            transform.position = hit.point;
        }

        if(reqResources == false)
        {
            canPlace = false;
        }

        if(Input.GetMouseButton(0) && canPlace)
        {
            Instantiate(prefab, transform.position, transform.rotation);
            BuildCanvas.isPlacing = false;
            Destroy(gameObject);

            
        }
        if(canPlace){rend.material.color = Color.green;}else{rend.material.color = Color.red;}

        if(Input.GetMouseButton(1))
        {
            Destroy(gameObject);
            BuildCanvas.isPlacing = false;
        }

    }

    void OnTriggerStay(Collider other)
    {
        canPlace = false;
    }

    void OnTriggerExit(Collider other) 
    {
        canPlace = true;
    }

    void ResourceCheck()
    {
        if (woodCost <= resourceController.totalWoodCount)
        { 
            reqResources = true; 
        }
        else
        {
            reqResources = false;
        }

    }

}
