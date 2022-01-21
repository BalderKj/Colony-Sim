using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrintPlacement : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    
    bool canPlace = true;
    
    private int numberOfCollisions;
    public GameObject prefab;
    public BuildCanvas BuildCanvas;
    
    
    Renderer rend;

    void Start() 
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = LayerMask.GetMask("Ground");
        if(Physics.Raycast(ray, out hit, 50000.0f, mask))
        {
            transform.position = hit.point;
        }

        if(Input.GetMouseButton(0) && canPlace)
        {
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
            BuildCanvas.isPlacing = false;
            
        }
        if(canPlace){rend.material.color = Color.green;}else{rend.material.color = Color.red;}

        if(Input.GetMouseButton(1))
        {
            Destroy(gameObject);
            BuildCanvas.isPlacing = false;
            /*refund resource cost */
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

}
